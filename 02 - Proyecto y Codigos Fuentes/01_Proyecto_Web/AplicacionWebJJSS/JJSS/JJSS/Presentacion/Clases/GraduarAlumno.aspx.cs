using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class GraduarAlumno : System.Web.UI.Page
    {
        private GestorGraduacion gestorGraduacion;
        private GestorTipoClase gestorTipo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_GRADUAR'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o no tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                }
                gestorTipo = new GestorTipoClase();
                gestorGraduacion = new GestorGraduacion();

                cargarGrilla();
                MultiView1.SetActiveView(view_elegir_graduacion);
            }

        }

        protected void cargarGrilla()
        {
            int tipoClase = 0;
            int.TryParse(ddl_tipo_clase.SelectedValue, out tipoClase);

            List<AlumnoFaja> listaAlumnos;
            List<AlumnoFaja> listaAlumnosFiltrada;

            
            if (tipoClase == 0)
            {
                gestorGraduacion = new GestorGraduacion();
                listaAlumnos = gestorGraduacion.buscarFajasAlumnos();
            }
            else
            {
                gestorGraduacion = new GestorGraduacion();
                listaAlumnos = gestorGraduacion.buscarFajasAlumnosConFiltro(tipoClase);
            }

            string filtroApellido = txt_filtro_apellido.Text.ToUpper().Trim();
            listaAlumnosFiltrada = listaAlumnos.FindAll(x => x.apellido.ToUpper().StartsWith(filtroApellido));

            foreach (AlumnoFaja t in listaAlumnosFiltrada)
            {
                t.fechaParaMostrar = t.fecha?.ToString("dd/MM/yyyy") ?? " - ";
            }

            gv_graduacion.DataSource = listaAlumnosFiltrada;

            gv_graduacion.DataBind();


        }

        protected void cargarRadioButton()
        {
            List<tipo_clase> lista = gestorTipo.ObtenerTipoClase();
            tipo_clase tc = new tipo_clase();
            tc.id_tipo_clase = 0;
            tc.nombre = "Todos";
            lista.Insert(0,tc);

            ddl_tipo_clase.DataSource = lista;
            ddl_tipo_clase.DataValueField = "id_tipo_clase";
            ddl_tipo_clase.DataTextField = "nombre";

            ddl_tipo_clase.DataBind();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/Inicio.aspx#section_alumnos");
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tipoClase");
            dt.Columns.Add("idAlumno");
            dt.Columns.Add("grados");
            
            for (int i = 0; i < gv_graduacion.Rows.Count; i++)
            {
                TextBox tb = (TextBox)gv_graduacion.Rows[i].Cells[4].FindControl("txt_grados");
                int grados = int.Parse(tb.Text);
                if (grados >= 1)
                {
                    int idAlu = Convert.ToInt32(gv_graduacion.DataKeys[i].Value);
                    DataRow dr = dt.NewRow();
                    dr["idAlumno"] = idAlu;
                    dr["grados"] = grados;
                    dr["tipoClase"] = gv_graduacion.Rows[i].Cells[2].Text;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();


                }
            }
            gestorGraduacion = new GestorGraduacion();
            string result = "-";
            result = gestorGraduacion.graduar(dt);
            if (result == "")
            {
                mensaje("Graduado exitosamente", true);
                cargarGrilla();
            }
            else if (result != "-") mensaje(result, false);
        }

       

        private void mensaje(string pMensaje, Boolean pEstado)
        {
            // Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>");
            if (pEstado == true)
            {
                pnl_mensaje_exito.Visible = true;
                pnl_mensaje_error.Visible = false;
                lbl_exito.Text = pMensaje;
            }
            else
            {
                pnl_mensaje_exito.Visible = false;
                pnl_mensaje_error.Visible = true;
                lbl_error.Text = pMensaje;
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        protected void gv_graduacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_graduacion.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }
    }
}