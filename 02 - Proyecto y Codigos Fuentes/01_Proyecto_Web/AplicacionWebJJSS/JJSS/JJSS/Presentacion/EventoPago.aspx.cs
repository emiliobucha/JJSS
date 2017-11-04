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
    public partial class EventoPago : System.Web.UI.Page
    {
        private GestorEventos gestorEvento;
        private GestorFormaPago gestorFPago;
        private GestorPagoClase gestorPago;
        private GestorParticipantesEvento gestorParticipantesEvento;
        private GestorMercadoPago gestorMP;
        private participante_evento participante_eventoElegido;
        private short pagoRecargo = 0; //si es 0 no pago recargo, si es 1 si lo pago


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEvento = new GestorEventos();
            gestorFPago = new GestorFormaPago();
            gestorParticipantesEvento = new GestorParticipantesEvento();
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


                if (Session["EventoPagar"] == null) Response.Redirect("InscripcionEvento.aspx");

                lbl_fecha1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                int id = int.Parse(Session["EventoPagar"].ToString());
                int dni = int.Parse(Session["ParticipanteDNI"].ToString());
                participante_eventoElegido = gestorParticipantesEvento.ObtenerParticipantePorDNI(dni);
                lbl_participante.Text = participante_eventoElegido.apellido + ", " + participante_eventoElegido.nombre;

                evento_especial evento = gestorEvento.BuscarEventoPorID(id);
                lbl_evento.Text = evento.nombre;
                lbl_fechaevento.Text = evento.fecha.ToString();

                double monto = (double)evento.precio ;
                lbl_monto.Text = "$" + monto;

              

                
                String sInit_Point = "";
                gestorMP = new GestorMercadoPago();
                sInit_Point = gestorMP.NuevoPago(monto, "Pago de Evento");
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
            Session["PagoEvento"] = "";
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