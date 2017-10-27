using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

using System.Collections;

namespace JJSS.Presentacion
{
    public partial class TorneoPago : System.Web.UI.Page
    {
        private GestorTorneos gestorTorneo;
        private GestorFormaPago gestorFPago;
        private GestorPagoClase gestorPago;
        private GestorParticipantes gestorParticipantes;
        private GestorMercadoPago gestorMP;
        private participante participanteElegido;
        private short pagoRecargo = 0; //si es 0 no pago recargo, si es 1 si lo pago


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneo = new GestorTorneos();
            gestorFPago = new GestorFormaPago();
            gestorParticipantes = new GestorParticipantes();
            gestorPago = new GestorPagoClase();

            if (!IsPostBack)
            {

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }

                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }


                if (Session["TorneoPagar"] == null) Response.Redirect("InscripcionTorneo.aspx");

                lbl_fecha1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                int id = int.Parse(Session["TorneoPagar"].ToString());
                int dni = int.Parse(Session["ParticipanteDNI"].ToString());
                participanteElegido = gestorParticipantes.ObtenerParticipantePorDNI(dni);
                lbl_participante.Text = participanteElegido.apellido + ", " + participanteElegido.nombre;

                torneo torneo = gestorTorneo.BuscarTorneoPorID(id);
                lbl_torneo.Text = torneo.nombre;
                lbl_fechatorneo.Text = torneo.fecha.ToString();

                double monto = (double)torneo.precio_absoluto ;
                lbl_monto.Text = "$" + monto;

              

                
                String sInit_Point = "";
                gestorMP = new GestorMercadoPago();
                sInit_Point = gestorMP.NuevoPago(monto);
                mp_checkout.Attributes.Add("href", sInit_Point);
            }
        }


        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

        protected void limpiar()
        {

            lbl_participante.Text = "No hay partiipante seleccionado";
            Session["PagoTorneo"] = "";
            pagoRecargo = 0;
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