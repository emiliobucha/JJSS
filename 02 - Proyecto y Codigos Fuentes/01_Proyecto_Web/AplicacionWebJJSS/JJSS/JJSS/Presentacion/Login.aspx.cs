using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JJSS.Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
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
    }
}