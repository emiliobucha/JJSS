using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Administracion;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarAlumnos : System.Web.UI.Page
    {
        private GestorAlumnos gestorAlumnos;
        private GestorEstados gestorEstados;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorAlumnos = new GestorAlumnos();
            gestorEstados = new GestorEstados();

            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_ADMINISTRACION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
                }

                if (Session["mensaje"] != null && Session["mensaje"].ToString().Trim() != "")
                {
                    mensaje(Session["mensaje"].ToString(), Convert.ToBoolean(Session["exito"]));
                    Session["mensaje"] = null;
                }

                CargarCheckboxEstados();
                CargarComboTipoDocumentos();
                CargarGrilla();
            }
        }

        protected void btn_buscar_alumno_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            CargarGrilla();
        }


        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumnos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var dni = gvAlumnos.DataKeys[index].Value.ToString();

                Session["alumnoEditar"] = dni;
                Response.Redirect("../Administracion/RegistrarAlumno.aspx");
            }else if (e.CommandName.CompareTo("activar") == 0)
            {
                int idAlu = Convert.ToInt32(e.CommandArgument);

                try
                {
                    gestorAlumnos.activarAlumno(idAlu);
                    CargarGrilla();
                    mensaje("Se activó el alumno correctamente", true);
                }catch(Exception ex)
                {
                    mensaje("Hubo un error en la activación", false);
                }
                
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

        protected void CargarCheckboxEstados()
        {
            List<estado> estados = gestorEstados.obtenerEstados("ALUMNOS");
            ddl_filtro_estado.DataSource = estados;
            ddl_filtro_estado.DataTextField = "nombre";
            ddl_filtro_estado.DataValueField = "id_estado";
            ddl_filtro_estado.DataBind();
        }

        protected void CargarGrilla()
        {
            var filtroDni = "";
            if (!string.IsNullOrEmpty(txt_filtro_dni.Text)) filtroDni = txt_filtro_dni.Text;
           

            int filtroEstados = 8;
            int.TryParse(ddl_filtro_estado.SelectedValue, out filtroEstados);

            int filtroTipoDoc = 0;
            int.TryParse(ddl_tipo.SelectedValue, out filtroTipoDoc);

            List<PersonaResultado.AlumnoResultado> listaCompleta = gestorAlumnos.BuscarAlumnoConEstado(filtroEstados, txt_filtro_apellido.Text, filtroDni, filtroTipoDoc);

            gvAlumnos.DataSource = listaCompleta;
            gvAlumnos.DataBind();
        }
        
        protected void btn_si_Click1(object sender, EventArgs e)
        {
            int  id  = int.Parse(txtIDSeleccionado.Text) ;
            string sReturn = gestorAlumnos.EliminarAlumnoID(id);
            Boolean estado = true;
            if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el alumno correctamente";
            else estado = false;
            mensaje(sReturn, estado);
            CargarGrilla();
            txtIDSeleccionado.Text = "";
        }

        protected void CargarComboTipoDocumentos()
        {

            List<tipo_documento> tiposdoc = gestorAlumnos.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }

        protected void gvAlumnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkActivar = (LinkButton)e.Row.FindControl("lnk_activar");
                LinkButton lnkEliminar = (LinkButton)e.Row.FindControl("aa");

                int estado = Convert.ToInt32( DataBinder.Eval(e.Row.DataItem, "id_estado"));

                if (estado == JJSS_Negocio.Constantes.ConstantesEstado.ALUMNOS_DE_BAJA)
                {
                    lnkActivar.Visible = true;
                    lnkEliminar.Visible = false;
                }
                else
                {
                    lnkActivar.Visible = false;
                    lnkEliminar.Visible = true;
                }
            }
        }
    }
}