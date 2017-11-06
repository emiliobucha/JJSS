using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class Reservas : System.Web.UI.Page
    {
        private static GestorReservas gestorReservas;
        private const int Retirado = 7;
        private const int Cancelado = 6;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                gestorReservas = new GestorReservas();
                CargarGrillaReservas();
                MultiView1.SetActiveView(view_grilla);
                
            }
        }

        private void CargarGrillaItems(int pIDReserva)
        {
            List<DetalleReservaResultado> lista = gestorReservas.ObtenerDetalles(pIDReserva);
            gv_items.DataSource = lista;
            gv_items.DataBind();

            
            decimal total = 0;
            foreach(DetalleReservaResultado i in lista){
                total += (decimal)i.total;
            }
            lbl_total.Text = total+"";
        }

        private void CargarGrillaReservas()
        {
            DataTable dtCompleta = new DataTable();
            DataTable dtConFiltro = new DataTable();

            List<Object> lista = new List<Object>();
            dtCompleta= gestorReservas.BuscarReservas();
            dtConFiltro = dtCompleta.Clone();

            string filtroApellido = txt_filtro_apellido.Text.ToUpper().Trim();

            for (int i = 0; i < dtCompleta.Rows.Count; i++)
            {
                DataRow dr = dtCompleta.Rows[i];
                if (dr["apellido"].ToString().ToUpper().StartsWith(filtroApellido))
                {
                    dtConFiltro.ImportRow(dr);
                };
            }


            gv_reservas.DataSource = dtConFiltro;
            gv_reservas.DataBind();
        }

        protected void gv_reservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_reservas.PageIndex = e.NewPageIndex;
            CargarGrillaReservas();
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {

        }

        protected void gv_reservas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id_reserva = Convert.ToInt32(gv_reservas.DataKeys[index].Value);

            if (e.CommandName.CompareTo("retirado") == 0)
            {
                string sReturn = gestorReservas.cambiarEstadoReserva(id_reserva, Retirado);
                if (sReturn.CompareTo("") == 0) mensaje("Se registró la venta correctamente", true);
                else mensaje(sReturn, false);
            }
            if (e.CommandName.CompareTo("cancelado") == 0)
            {
                string sReturn = gestorReservas.cambiarEstadoReserva(id_reserva, Cancelado);
                if (sReturn.CompareTo("") == 0) mensaje("Se canceló la reserva correctamente", true);
                else mensaje(sReturn, false);
            }
            if (e.CommandName.CompareTo("detalle") == 0)
            {
                MultiView1.SetActiveView(view_detalle_reserva);
                CargarGrillaItems(id_reserva);
            }
            CargarGrillaReservas();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            CargarGrillaReservas();
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
            // Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>");
            if (pEstado == true)
            {
                pnl_mensaje_exito.Visible = true;
                pnl_mensaje_error.Visible = false;
                lbl_exito.Text = pMensaje;
            }
            else
            {
                pnl_mensaje_exito.Visible = false;
                pnl_mensaje_error.Visible = true;
                lbl_error.Text = pMensaje;
            }
        }

        protected void btn_grilla_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_grilla);
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            CargarGrillaReservas();
        }

        protected void btn_sin_reserva_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_sin_reserva);
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }
    }
}