using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
using System.Data;

namespace JJSS.Presentacion.Clases
{
    public partial class RegistrarAsistenciasAnteriores : System.Web.UI.Page
    {
        private GestorAsistencia ga;
        private static List<AlumnoAsistenciaInscripcion> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            ga = new GestorAsistencia();
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_ASISTENCIA'");
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


                dp_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarComboClases();
            }
        }

        private void CargarComboClases()
        {
            GestorClases gc = new GestorClases();
            List<ClasesDisponibles> clases = gc.ObtenerClasesDisponibles("", 0, 0, 0);

            ClasesDisponibles primerElemento = new ClasesDisponibles()
            {
                id_clase = -1,
                nombre = "Seleccione una clase",
            };
            clases.Insert(0, primerElemento);
            ddl_clases.DataSource = clases;
            ddl_clases.DataValueField = "id_clase";
            ddl_clases.DataTextField = "nombre";
            ddl_clases.DataBind();
        }

        private void CargarGrilla()
        {

            int idClase = Convert.ToInt32(ddl_clases.SelectedValue);
            DateTime fecha = DateTime.Parse(dp_fecha.Text);
            try
            {
                list = ga.BuscarAlumnosConAsistenciaInscripcion(idClase, fecha);
                gv_inscriptos.DataSource = list;
                gv_inscriptos.DataBind();
                if (gv_inscriptos.Rows.Count > 0)
                {
                    chkSelectAll.Visible = true;
                    lbl_select_all.Visible = true;
                    btn_aceptar.Visible = true;
                }
                else
                {
                    chkSelectAll.Visible = false;
                    lbl_select_all.Visible = false;
                    btn_aceptar.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                mensaje(ex.Message, false);
                chkSelectAll.Visible = false;
                lbl_select_all.Visible = false;
                btn_aceptar.Visible = false;
            }
        }

        private void CargarDatosClase()
        {

        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            chkSelectAll.Visible = false;
            lbl_select_all.Visible = false;
            gv_inscriptos.DataSource = null;
            gv_inscriptos.DataBind();

            int idClase = int.Parse(ddl_clases.SelectedValue);
            if (idClase == -1)
            {
                mensaje("Debe seleccionar una clase", false);
                return;
            }
            LimpiarMensaje();

            CargarDatosClase();
            CargarGrilla();
            
        }

        protected void gv_inscriptos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_inscriptos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gv_inscriptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = ((CheckBox)e.Row.FindControl("chk_asistio"));
                Boolean asistio = Convert.ToBoolean( DataBinder.Eval(e.Row.DataItem, "asistio"));
                chk.Checked = asistio;
            }
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
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


        private void LimpiarMensaje()
        {
         
                pnl_mensaje_exito.Visible = false;
                pnl_mensaje_error.Visible = false;
                lbl_exito.Text = "";
          
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            List<AlumnoAsistenciaInscripcion> alumnos = new List<AlumnoAsistenciaInscripcion>();
            for (int i = 0; i < gv_inscriptos.Rows.Count; i++)
            {
                int idAlu = Convert.ToInt32(gv_inscriptos.DataKeys[i].Value);
              
                CheckBox chk = (CheckBox)gv_inscriptos.Rows[i].Cells[4].FindControl("chk_asistio");
                int idInscr = list[i].inscr_id;
                AlumnoAsistenciaInscripcion alu_agregar = new AlumnoAsistenciaInscripcion()
                {
                    alu_id = idAlu,
                    asistio = chk.Checked,
                    inscr_id = idInscr
                };
                alumnos.Add(alu_agregar);
            }

            try
            {
                ga.RegistrarMuchasAsistencias(alumnos, Convert.ToInt32(ddl_clases.SelectedValue), DateTime.Parse(dp_fecha.Text));
                mensaje("Se registraron las asistencias correctamente", true);
            }
            catch(Exception ex)
            {
                mensaje(ex.Message, false);
            }
        }
    }
}