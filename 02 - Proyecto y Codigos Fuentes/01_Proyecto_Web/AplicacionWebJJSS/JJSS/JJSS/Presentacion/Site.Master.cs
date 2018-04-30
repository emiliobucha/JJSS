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
                    //pnl_iniciar_sesion.Visible = true;
                   // pnl_sesion_activa.Visible = false;
                    ocultarInvitado();

                }
                else
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva == null)
                    {
                        //pnl_iniciar_sesion.Visible = true;
                        //pnl_sesion_activa.Visible = false;
                    }
                    else
                    {

                        //    pnl_iniciar_sesion.Visible = false;
                        //    pnl_sesion_activa.Visible = true;
                        //    lbl_sesion_nombre.Text = sesionActiva.usuario.nombre.ToString();
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
                               "../Presentacion/Login.aspx" + "', 2000);</script>");

            }

        }

        protected void cerrar_sesion_Click(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            gestorSesion.CerrarSesion();
        }

        private void ocultarInvitado()
        {


            nav_crear_torneo.Style["display"] = "none";



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

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            }
        }
    }
}