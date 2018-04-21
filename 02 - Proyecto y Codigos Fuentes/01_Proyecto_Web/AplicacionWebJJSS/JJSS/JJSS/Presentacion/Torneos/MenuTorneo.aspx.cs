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
                dp_filtro_fecha_desde.Text = DateTime.Today.ToString("dd/MM/yyyy"); 
                dp_filtro_fecha_hasta.Text = DateTime.Today.AddYears(2).ToString("dd/MM/yyyy");
                cargarTorneosAbiertosView();
            }

        }

        protected void cargarTorneosAbiertosView()
        {
            String filtroNombre = txt_filtro_nombre.Text;

            DateTime filtroFecha = new DateTime();
            if (dp_filtro_fecha_desde.Text.CompareTo("") != 0)
            {
                filtroFecha = DateTime.Parse(dp_filtro_fecha_desde.Text);
            }
            DateTime filtroFechaHasta = new DateTime();
            if (dp_filtro_fecha_hasta.Text.CompareTo("") != 0)
            {
                filtroFechaHasta = DateTime.Parse(dp_filtro_fecha_hasta.Text);
            }
            lv_torneos_abiertos.DataSource = gestorDeTorneos.ObtenerTorneosConImagenYFiltro(filtroNombre,filtroFecha,filtroFechaHasta);
            lv_torneos_abiertos.DataBind();
        }
        protected void lv_torneos_abiertos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["idTorneo"] = id;
                Response.Redirect("~/Presentacion/Torneos/VerTorneo.aspx");
            }else if (e.CommandName.CompareTo("inscribir") == 0)
            {
                Session["idTorneo_inscribirTorneo"] = id;
                Response.Redirect("~/Presentacion/Torneos/InscripcionTorneo.aspx");
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarTorneosAbiertosView();
        }
    }
}