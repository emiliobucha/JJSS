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
    public partial class HistoricoTorneos : System.Web.UI.Page
    {
        private static GestorTorneos gestorDeTorneos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorDeTorneos = new GestorTorneos();
                dp_filtro_fecha_desde.Text = DateTime.Today.AddYears(-1).ToString("dd/MM/yyyy");
                dp_filtro_fecha_hasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
                cargarTorneosAbiertosView();
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarTorneosAbiertosView();
        }

        protected void cargarTorneosAbiertosView()
        {
            String filtroNombre = txt_filtro_nombre.Text;

            DateTime filtroFecha = new DateTime();
            if (dp_filtro_fecha_desde.Text.CompareTo("") != 0)
            {
                /*FECHA SOMEE
                string[] formats = { "MM/dd/yyyy" };
                DateTime filtroFecha = DateTime.ParseExact(dp_filtro_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                */
                //LOCAL
                filtroFecha = DateTime.Parse(dp_filtro_fecha_desde.Text);
            }
            DateTime filtroFechaHasta = new DateTime();
            if (dp_filtro_fecha_hasta.Text.CompareTo("") != 0)
            {
                filtroFechaHasta = DateTime.Parse(dp_filtro_fecha_hasta.Text);
            }
            List<JJSS_Negocio.Resultados.TorneoResultado> tr = gestorDeTorneos.BuscarTorneosConFiltrosEImagen(filtroNombre, filtroFecha, filtroFechaHasta);
            lv_torneos.DataSource = tr;
            lv_torneos.DataBind();
        }

        protected void lv_torneos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["idTorneo"] = id;
            Response.Redirect("~/Presentacion/verTorneo.aspx");

        }
    }
}