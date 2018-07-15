using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

namespace JJSS
{


    public partial class SiteMaster : MasterPage
    {
        GestorSesiones gestorSesion;
        GestorUsuarios gestorUsuarios;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            gestorUsuarios = new GestorUsuarios();
            try
            {



                if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                {
                    nav_usuario.Visible = false;
                    nav_iniciar_sesion.Visible = true;
                    ocultarInvitado();

                }
                else
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva == null)
                    {
                        nav_usuario.Visible = false;
                        nav_iniciar_sesion.Visible = true;
                    }
                    else
                    {

                        nav_usuario.Visible = true;
                        nav_iniciar_sesion.Visible = false;
                        lbl_sesion_nombre.Text = sesionActiva.usuario.nombre.ToString() + "&nbsp;" ;
                        //    lbl_roles.Text =
                        //        gestorUsuarios.obtenerTablaUsuarios().Select("login = '" + sesionActiva.usuario.login + "'")[0][
                        //            "grupo_nombre"].ToString();
                        //
                    }
                    ocultarPermiso();

                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() +
                               "');</script>" + "<script>window.setTimeout(location.href='" +
                               "Login.aspx" + "', 2000);</script>");

            }

        }

        protected void cerrar_sesion_Click(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            gestorSesion.CerrarSesion();
            Response.Redirect("../Presentacion/Login.aspx");
        }

        private void ocultarInvitado()
        {


            nav_crear_torneo.Style["display"] = "none";
            nav_historico_torneos.Style["display"] = "none";

            navbarAdministracion.Style["display"] = "none";
            nav_crear_evento.Style["display"] = "none";

            nav_hist_evento.Style["display"] = "none";
            nav_registrar_asistencia.Style["display"] = "none";
            nav_listado_asistencia.Style["display"] = "none";

            nav_graduar_alumno.Style["display"] = "none";

            nav_crear_clase.Style["display"] = "none";
            navbarPagos.Style["display"] = "none";
            nav_asistencias_anteriores.Style["display"] = "none";

        }

        protected void ocultarPermiso()
        {
            try
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {

                    //Administración de torneos

                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_crear_torneo.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_HISTORIAL'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_historico_torneos.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_inscripcion_torneo.Style["display"] = "none";
                    }



                    //Administración
                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'ADMINISTRACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        navbarAdministracion.Style["display"] = "none";
                    }

                    //Administración de Eventos
                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_crear_evento.Style["display"] = "none";
                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_HISTORIAL'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_hist_evento.Style["display"] = "none";
                    }

                    //Administración de Clases


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_GRADUAR'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_graduar_alumno.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_ASISTENCIA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_registrar_asistencia.Style["display"] = "none";
                        nav_listado_asistencia.Style["display"] = "none";
                        nav_asistencias_anteriores.Style["display"] = "none";
                    }

                   permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        nav_crear_clase.Style["display"] = "none";
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            }

        }
    }
}