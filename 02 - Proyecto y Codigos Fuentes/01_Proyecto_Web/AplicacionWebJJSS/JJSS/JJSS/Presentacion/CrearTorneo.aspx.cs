using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;




namespace JJSS.Presentacion
{
    public partial class CrearTorneo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


 

        protected void btnCrearNuevoTorneo_Click(object sender, EventArgs e)
        {
            GestorTorneos gestorTorneos = new GestorTorneos();
            gestorTorneos.GenerarNuevoTorneo(DateTime.Today, "Hola", (float)1.5, "aho", 1);
        }
    }


}