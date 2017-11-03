using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

using System.Collections;

namespace JJSS.Presentacion
{
    public partial class AlumnoClases : System.Web.UI.Page
    {
        private GestorClases gestorClase;
        private GestorAlumnos gestorAlumnos;
        private GestorSesiones gestorSesiones = new GestorSesiones();

        private alumno alumnoElegido;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorClase = new GestorClases();
            gestorAlumnos = new GestorAlumnos();
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_INSCRIPCION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ver"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }

                alumnoElegido = gestorAlumnos.ObtenerAlumnoPorIdUsuario(gestorSesiones.getActual().usuario.id_usuario);

                if (alumnoElegido != null)
                {
                    int dni = alumnoElegido.dni;
                    Session["AlumnoDNI"] = dni;
                    cargarClases();
                }else
                {
                    Response.Write("<script>window.alert('" + "No es un alumno correctamente registrado".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Inicio.aspx" + "', 2000);</script>");

                }

            }
        }


        protected void cargarClases()
        {
            gvClases.DataSource = gestorClase.ObtenerClaseSegunAlumnoGrilla(alumnoElegido.id_alumno);
            gvClases.DataBind();
        }


        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Presentacion/Inicio.aspx");
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


        protected void gvClases_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("pago") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idClase = Convert.ToInt32(gvClases.DataKeys[index].Value);
                Session["Clase"] = idClase.ToString();
                Response.Redirect("../Presentacion/AlumnoPagoClase");
            }
        }

        protected void gvClases_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClases.PageIndex = e.NewPageIndex;
            cargarClases();
        }
    }
}