using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados.Pagos;
using Newtonsoft.Json;

namespace JJSS.Presentacion.Pagos
{
    public partial class PagosMultiple : System.Web.UI.Page
    {
        private GestorTorneos gestorTorneo;
        private GestorMercadoPago gestorMP;
        private participante participanteElegido;
        private GestorInscripciones gestorInscripciones;
        private static PagoMultiple PagoMultiple;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneo = new GestorTorneos();
            gestorInscripciones = new GestorInscripciones();

            if (!IsPostBack)
            {
                
                PagoMultiple = JsonConvert.DeserializeObject<PagoMultiple>(Session["PagoMultiple"].ToString());
                

                lbl_fecha1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                lbl_participante.Text = PagoMultiple.NombreCompleto;
                lbl_descripcion.Text  = PagoMultiple.Descripcion;

                double monto =(double) PagoMultiple.MontoTotal;
                lbl_monto.Text = "$ " + PagoMultiple.MontoTotal;



                var sInit_Point = "";
                gestorMP = new GestorMercadoPago();
                sInit_Point = gestorMP.NuevoPagoMultiple(PagoMultiple.ObjetosPagables);      
                mp_checkout.Attributes.Add("href", sInit_Point);
                mp_checkout.Attributes["href"] = sInit_Point;
                mp_checkout.DataBind();
                //mp_checkout.HRef = sInit_Point;
            }
        }


        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("MenuTorneo.aspx");
        }

        protected void limpiar()
        {

            lbl_participante.Text = "No hay participante seleccionado";
            Session["PagoMultiple"] = "";

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
    }
}