using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Soporte
{
    public partial class CambioEstadoAlumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            var gestor = new GestorVencimientos();
            gestor.ActualizarEstadosInscripcion();
            gestor.ActualizarEstadosAlumno();

        }
    }
}