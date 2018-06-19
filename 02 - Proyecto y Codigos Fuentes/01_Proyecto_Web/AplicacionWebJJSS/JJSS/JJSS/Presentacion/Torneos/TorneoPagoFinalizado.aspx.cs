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
    public partial class TorneoPagoFinalizado : System.Web.UI.Page
    {
        private GestorTorneos gestorTorneos;
        private GestorFormaPago gestorFPago;
        private GestorPagoTorneo gestorPago;
        private GestorParticipantes gestorParticipantes;
        private GestorMercadoPago gestorMP;
        private participante participanteElegido;
        private GestorInscripciones gestorInscripciones;
        private short pagoRecargo = 0; //si es 0 no pago recargo, si es 1 si lo pago


        protected void Page_Load(object sender, EventArgs e)
        {


            gestorTorneos = new GestorTorneos();
            gestorFPago = new GestorFormaPago();
            gestorParticipantes = new GestorParticipantes();
            gestorPago = new GestorPagoTorneo();
            if (!IsPostBack)
            {

                //try
                //{
                //    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                //    if (sesionActiva.estado == "INGRESO ACEPTADO")
                //    {
                //        int permiso = 0;
                //        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION'");
                //        if (drsAux.Length > 0)
                //        {
                //            int.TryParse(drsAux[0]["perm_ver"].ToString(), out permiso);

                //        }
                //        if (permiso != 1)
                //        {
                //            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                //}
                if (Session["TorneoPagar"] == null || Session["TorneoPagar"].ToString() == "") Response.Redirect("InscripcionTorneo.aspx");



                var resultado = Request["Estado"];
                if (resultado == "ok")
                {

                    lbl_fecha.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                    int id = int.Parse(Session["TorneoPagar"].ToString());
                    var dni = Session["ParticipanteDNI"].ToString();
                    int tipoDoc = int.Parse(Session["ParticipanteTipoDoc"].ToString());
                    participanteElegido = gestorParticipantes.ObtenerParticipantePorDNI(dni);
                    torneo torneoPagar = gestorTorneos.BuscarTorneoPorID(id);
                    lbl_participante.Text = participanteElegido.apellido + ", " + participanteElegido.nombre;
                    lbl_torneo.Text = torneoPagar.nombre;


                    double monto = (double)torneoPagar.precio_absoluto;
                    lbl_monto.Text = "$" + monto;



                    if (!string.IsNullOrEmpty(Session["ParticipanteDNI"].ToString())  )
                    {
                        participanteElegido = gestorParticipantes.ObtenerParticipantePorDNI(dni);

                        string sReturn = ""; //gestorPago.registrarPago(participanteElegido.id_participante, id, 4);
                        if (sReturn.CompareTo("") == 0)
                        {
                            mensaje("Se ha registrado el pago exitosamente", true);

                            //para usuarios
                            Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];


                            try
                            {
                                string mail = sesionActiva.usuario.mail;
                                string sFile = gestorInscripciones.ComprobanteInscripcionPago(gestorInscripciones.obtenerInscripcionATorneoPorIdParticipantePorDni(tipoDoc,dni, id).id_inscripcion, mail);

                                Response.Clear();
                                Response.AddHeader("Content-Type", "Application/octet-stream");
                                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                                Response.WriteFile(sFile);
                            }
                            catch (Exception ex)
                            {
                                Response.Write("<script>window.alert('" + "Usted se ha inscripto exitosamente pero no se le pudo generar el comprobante. Puede fijarse en sus incripciones si es necesario".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "/Presentacion/MenuInicio.aspx" + "', 2000);</script>");
                            }



                            limpiar();
                        }
                        else mensaje(sReturn, false);
                    }
                    else mensaje("No hay alumno seleccionado", false);
                    Session["TorneoPagar"] = null;
                }
                else
                {
                    Session["TorneoPagar"] = null;
                    Response.Redirect("Inicio.aspx");
                }


            }


        }




        protected void limpiar()
        {

            //lbl_alumno.Text = "No hay alumno seleccionado";
            //Session["Clase"] = "";
            //pagoRecargo = 0;
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

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("Inicio.aspx");
        }
    }
}