using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;


namespace JJSS.Presentacion
{
    public partial class Site2 : System.Web.UI.MasterPage
    {
        GestorSesiones gestorSesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
            {
                pnl_iniciar_sesion.Visible = true;
                pnl_sesion_activa.Visible = false;
            }
            else
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
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
                }
            }

        }
        protected void cerrar_sesion_Click(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            gestorSesion.CerrarSesion();
        }
    }      
    
}