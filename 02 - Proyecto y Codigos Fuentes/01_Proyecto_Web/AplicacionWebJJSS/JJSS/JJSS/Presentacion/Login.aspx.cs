using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
        GestorSesiones gestorSesion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnl_cambiar_pass.Visible = false;
                pnlLogin.Visible = true;
            }
            
        }

        protected void lnk_olvido_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnl_cambiar_pass.Visible = true;
        }

        protected void btn_iniciar_sesion_Click(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            string login = txt_usuario.Text;
            string pass = txt_pass.Text;
            try
            {
                Sesion nueva = gestorSesion.IniciarSesion(login, pass);
                if (nueva.estado == "INGRESO ACEPTADO")
                {
                    Response.Redirect("~/Presentacion/Inicio.aspx",false);
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}