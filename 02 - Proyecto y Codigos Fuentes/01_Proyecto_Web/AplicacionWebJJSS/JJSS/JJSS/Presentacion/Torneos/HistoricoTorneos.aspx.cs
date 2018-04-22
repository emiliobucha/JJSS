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
                cargarComboEstados();
                dp_filtro_fecha_desde.Text = DateTime.Today.AddYears(-1).ToString("dd/MM/yyyy");
                dp_filtro_fecha_hasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
                cargarTorneosAbiertosView(true);
                cargarTorneosAbiertosGrid(true);
            }
        }

        private void cargarComboEstados()
        {
            ddl_estados.DataSource = gestorDeTorneos.buscarEstadosTorneo();
            ddl_estados.DataValueField = "id_estado";
            ddl_estados.DataTextField = "nombre";
            ddl_estados.DataBind();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarTorneosAbiertosView(false);
            cargarTorneosAbiertosGrid(false);
        }

        protected void cargarTorneosAbiertosGrid(Boolean inicio)
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
            int idEstado;
            if (inicio)
            {
                idEstado = 0;
            }
            else
            {
                idEstado = int.Parse(ddl_estados.SelectedValue);
            }

            List<JJSS_Negocio.Resultados.TorneoResultado> tr = gestorDeTorneos.BuscarTorneosConFiltrosEImagen(filtroNombre, filtroFecha, filtroFechaHasta, idEstado);
            gv_torneos.DataSource = tr;
            gv_torneos.DataBind();
        }


        protected void gv_torneo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["idTorneo"] = id;
            Response.Redirect("VerTorneo.aspx");
        }

        protected void gv_torneo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_torneos.PageIndex = e.NewPageIndex;
            cargarTorneosAbiertosGrid(true);
        }



        protected void cargarTorneosAbiertosView(Boolean inicio)
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
            int idEstado;
            if (inicio)
            {
                idEstado = 0;
            }
            else
            {
                idEstado = int.Parse(ddl_estados.SelectedValue);
            }

            List<JJSS_Negocio.Resultados.TorneoResultado> tr = gestorDeTorneos.BuscarTorneosConFiltrosEImagen(filtroNombre, filtroFecha, filtroFechaHasta, idEstado);
            lv_torneos.DataSource = tr;
            lv_torneos.DataBind();
        }

        protected void lv_torneos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["idTorneo"] = id;
            Response.Redirect("VerTorneo.aspx");

        }

    }
}