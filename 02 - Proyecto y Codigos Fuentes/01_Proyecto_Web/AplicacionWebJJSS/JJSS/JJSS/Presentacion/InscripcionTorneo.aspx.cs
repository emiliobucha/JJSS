using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JJSS
{
    public partial class InscripcionTorneo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            int idTorneo = ddl_torneos.SelectedIndex;
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            float peso = float.Parse(txt_peso.Text);
            int edad = int.Parse(txt_edad.Text);
            int idFaja = ddl_fajas.SelectedIndex;
            short sexo=0; 
            if (rbSexo.SelectedIndex == 0) sexo = 0; //femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //masculino


        }
    }
    
}