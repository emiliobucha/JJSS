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
        

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            var current = HttpContext.Current.Session["SEGURIDAD_SESION"];
            if (current != null)
            {
                if (current.ToString() == "INVITADO")
                {
                    pnl_iniciar_sesion.Visible = true;
                    pnl_sesion_activa.Visible = false;
                }
                else
                {
                    Sesion sesionActiva = (Sesion) current;
                    if (sesionActiva != null)
                    {
                        pnl_iniciar_sesion.Visible = false;
                        pnl_sesion_activa.Visible = true;
                        lbl_sesion_nombre.Text = sesionActiva.usuario.nombre.ToString();

                    }else if (sesionActiva == null)
                    {
                        pnl_iniciar_sesion.Visible = true;
                        pnl_sesion_activa.Visible = false;
                    }


                }
              

            }
            else
            {
                pnl_iniciar_sesion.Visible = true;
                pnl_sesion_activa.Visible = false;
            }


        }

        protected void cerrar_sesion_Click(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            gestorSesion.CerrarSesion();
        }
        
    }
}