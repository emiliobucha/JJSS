using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JJSS.Presentacion
{
    public partial class CrearClase : System.Web.UI.Page
    {
        DataTable tablaLunes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             

            }
        }

        protected void chk_lunes_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_lunes.Checked == true)
            {
                //Tabla visible
                dt_lunes.Visible = true;
                dt_lunes.DataSource = tablaLunes;
            }
        }

        protected void btnLunesMas_Click(object sender, EventArgs e)
        {
            //agregar fila
        }
    }
}