using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class MenuTorneo : System.Web.UI.Page
    {
        private GestorTorneos gestorDeTorneos;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorDeTorneos = new GestorTorneos();
            if (!IsPostBack)
            {
                cargarTorneosAbiertosView();
            }

        }

        protected void cargarTorneosAbiertosView()
        {
            lv_torneos_abiertos.DataSource = gestorDeTorneos.ObtenerTorneosConImagen();
            lv_torneos_abiertos.DataBind();
        }
        protected void lv_torneos_abiertos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["torneoSeleccionado"] = id;
            Response.Redirect("~/Presentacion/InscripcionTorneo.aspx");
        }
    }
}