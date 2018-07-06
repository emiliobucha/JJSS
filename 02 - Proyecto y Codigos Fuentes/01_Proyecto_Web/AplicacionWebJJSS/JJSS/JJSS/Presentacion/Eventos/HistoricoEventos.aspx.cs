using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion.Eventos
{
    public partial class HistoricoEventos : System.Web.UI.Page
    {
        private GestorEventos gestorEventos;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEventos = new GestorEventos();
            if (!IsPostBack)
            {

                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/Eventos/Menu_Evento.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                cargarComboEstados();
                dp_filtro_fecha_desde.Text = DateTime.Today.AddYears(-1).ToString("dd/MM/yyyy");
                dp_filtro_fecha_hasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
                cargarEventosAbiertosGrid(true);
            }
        }

        private void cargarComboEstados()
        {
            ddl_estados.DataSource = gestorEventos.buscarEstadosEvento();
            ddl_estados.DataValueField = "id_estado";
            ddl_estados.DataTextField = "nombre";
            ddl_estados.DataBind();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarEventosAbiertosGrid(false);
        }

        protected void cargarEventosAbiertosGrid(Boolean inicio)
        {
            String filtroNombre = txt_filtro_nombre.Text;

            DateTime filtroFecha = new DateTime();
            if (!string.IsNullOrEmpty(dp_filtro_fecha_desde.Text))
            {
                filtroFecha = DateTime.Parse(dp_filtro_fecha_desde.Text);
            }
            DateTime filtroFechaHasta = new DateTime();
            if (!string.IsNullOrEmpty(dp_filtro_fecha_hasta.Text))
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
            
            List<TorneoResultado> tr = gestorEventos.BuscarEventosConFiltrosEImagen(filtroNombre, filtroFecha, filtroFechaHasta, idEstado);
            gv_eventos.DataSource = tr;
            gv_eventos.DataBind();
        }

        protected void gv_eventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_eventos.PageIndex = e.NewPageIndex;
            cargarEventosAbiertosGrid(true);
        }

        protected void gv_eventos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gv_eventos.DataKeys[index].Value);
                Session["eventoSeleccionado"] = id;
                Response.Redirect("VerEvento.aspx");
            }
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu_Evento.aspx");
        }
    }
}