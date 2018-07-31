using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Presentacion.Pagos
{
    public partial class PagosMes : System.Web.UI.Page
    {


        private static GestorPagos gestorPagos;
        private static DateTime fechaDesdeG;
        private static DateTime fechaHastaG;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorPagos = new GestorPagos();
            if (!IsPostBack)
            {

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'PAGO_MES'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" +
                                           "No se encuentra logueado correctamente o no tiene los permisos para estar aquí"
                                               .Trim() + "');</script>" + "<script>window.setTimeout(location.href='" +
                                           "../Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() +
                                   "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" +
                                   "', 2000);</script>");
                }

                DateTime fechaDesde = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime fechaHasta = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                    DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

                dp_fecha_desde.Text = fechaDesde.ToString("dd/MM/yyyy");
                dp_fecha_hasta.Text = fechaHasta.ToString("dd/MM/yyyy");

                fechaDesdeG = fechaDesde;
                fechaHastaG = fechaHasta;
                CargarGrillaPendientes(fechaDesde, fechaHasta);
                CargarGrillaPagados(fechaDesde, fechaHasta);

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
            DateTime.TryParse(dp_fecha_desde.Text, out fechaDesde);

            DateTime fechaHasta = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            DateTime.TryParse(dp_fecha_hasta.Text, out fechaHasta);

            fechaDesdeG = fechaDesde;
            fechaHastaG = fechaHasta;

            CargarGrillaPendientes(fechaDesde,fechaHasta);
            CargarGrillaPagados(fechaDesde, fechaHasta);

        }


        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvPagos.PageIndex = e.NewPageIndex;
            CargarGrillaPagados(fechaDesdeG, fechaHastaG);
        }

        protected void gvPendientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendientes.PageIndex = e.NewPageIndex;
            CargarGrillaPendientes(fechaDesdeG, fechaHastaG);
        }

        private void CargarGrillaPagados(DateTime fechaDesde, DateTime fechaHasta)
        {
            var pagos = gestorPagos.ObtenerObjetosPagablesPagados(fechaDesde, fechaHasta);
            gvPagos.DataSource = pagos;
            gvPagos.DataBind();
            lbl_total.Text = pagos.Sum(x => x.Monto).ToString("N2");

        }

        private void CargarGrillaPendientes(DateTime fechaDesde, DateTime fechaHasta)
        {
            var pendientes = gestorPagos.ObtenerObjetosPagablesIntervaloPendientes(fechaDesde, fechaHasta);
            gvPendientes.DataSource = pendientes;
            gvPendientes.DataBind();
            lbl_total_pendiente.Text = pendientes.Sum(x => x.Monto).ToString("N2");

        }

        protected void gvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // To check condition on integer value
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoPago.Tipo")) == "Torneo")
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoPago.Tipo")) == "Evento")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            }
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoPago.Tipo")) == "Clase")
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;
            }
        }
    }
}