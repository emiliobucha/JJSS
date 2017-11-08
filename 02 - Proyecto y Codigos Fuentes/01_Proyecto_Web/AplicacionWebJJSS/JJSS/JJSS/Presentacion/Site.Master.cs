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
                    pnl_iniciar_sesion.Visible = true;
                    pnl_sesion_activa.Visible = false;

                }
                else
                {
                    Sesion sesionActiva = (Sesion) HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva == null)
                    {
                        pnl_iniciar_sesion.Visible = true;
                        pnl_sesion_activa.Visible = false;
                    }
                    else
                    {

                        pnl_iniciar_sesion.Visible = false;
                        pnl_sesion_activa.Visible = true;
                        lbl_sesion_nombre.Text = sesionActiva.usuario.nombre.ToString();
                        lbl_roles.Text =
                            gestorUsuarios.obtenerTablaUsuarios().Select("login = '" + sesionActiva.usuario.login + "'")[0][
                                "grupo_nombre"].ToString();
                    }

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



    }
}