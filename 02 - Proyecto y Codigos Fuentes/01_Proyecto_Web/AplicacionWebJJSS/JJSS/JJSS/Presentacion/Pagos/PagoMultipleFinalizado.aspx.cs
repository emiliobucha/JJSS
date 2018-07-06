using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

using System.Collections;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Resultados.Pagos;
using Newtonsoft.Json;

namespace JJSS.Presentacion
{
    public partial class PagoMultipleFinalizado : System.Web.UI.Page
    {
        private GestorTorneos gestorTorneos;
        private GestorFormaPago gestorFPago;
        private GestorPagos gestorPago;
        private GestorParticipantes gestorParticipantes;
        private GestorMercadoPago gestorMP;
        private participante participanteElegido;
        private GestorInscripciones gestorInscripciones;
        private short pagoRecargo = 0; //si es 0 no pago recargo, si es 1 si lo pago
        private static PagoMultiple pagoMultiple;
        private GestorInscripcionesEvento gestorInscripcionesEventos;

        protected void Page_Load(object sender, EventArgs e)
        {


            gestorTorneos = new GestorTorneos();
            gestorFPago = new GestorFormaPago();
            gestorParticipantes = new GestorParticipantes();
            gestorPago = new GestorPagos();
            gestorInscripciones = new GestorInscripciones();
            gestorInscripcionesEventos = new GestorInscripcionesEvento();
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

                if (string.IsNullOrEmpty(Session["PagoMultiple"].ToString())) Response.Redirect("PagosPanel.aspx");

                pagoMultiple = JsonConvert.DeserializeObject<PagoMultiple>(Session["PagoMultiple"].ToString());


                if (pagoMultiple == null)
                {
                    Response.Redirect("PagosPanel.aspx");
                    return;
                }

                if (pagoMultiple.TipoDocumento == null) Response.Redirect("PagosPanel.aspx");

                if (pagoMultiple.Dni == null) Response.Redirect("PagosPanel.aspx");

                if (pagoMultiple.ObjetosPagables == null || pagoMultiple.ObjetosPagables.Count == 0)
                {
                    Response.Redirect("PagosPanel.aspx");
                    return;

                }


                if (Request["MP"] == "N")
                {
                    lbl_fecha1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                    lbl_participante.Text = pagoMultiple.NombreCompleto;
                    lbl_descripcion.Text = pagoMultiple.Descripcion;

                    double monto = (double) pagoMultiple.MontoTotal;
                    lbl_monto.Text = "$ " + pagoMultiple.MontoTotal;

                    string sReturn = gestorPago.RegistrarNuevoPagoMultiple(pagoMultiple);

                    if (string.IsNullOrEmpty(sReturn))
                    {
                        mensaje("Se ha registrado el pago exitosamente", true);
                        CargarGrilla();
                    }
                    else mensaje(sReturn, false);
                }
                else
                {
                    var resultado = Request["collection_status"];
                    if (resultado != "approved" && resultado != "pending") Response.Redirect("PagosPanel.aspx");


                    if (resultado == "approved" || resultado == "pending")
                    {
                        pagoMultiple.EstadoMP = resultado;

                        lbl_fecha1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                        lbl_participante.Text = pagoMultiple.NombreCompleto;
                        lbl_descripcion.Text = pagoMultiple.Descripcion;

                        double monto = (double)pagoMultiple.MontoTotal;
                        lbl_monto.Text = "$ " + pagoMultiple.MontoTotal;

                        string sReturn = gestorPago.RegistrarNuevoPagoMultiple(pagoMultiple);

                        if (string.IsNullOrEmpty(sReturn))
                        {
                            mensaje("Se ha registrado el pago exitosamente", true);
                            CargarGrilla();
                        }
                        else mensaje(sReturn, false);
                    }
                }
            }
        }


        private void CargarGrilla()
        {
            gvPagos.DataSource = pagoMultiple.ObjetosPagables;
            gvPagos.DataBind();
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

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "imprimir")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                var pagoImprimir = pagoMultiple.ObjetosPagables[index];


                //para usuarios
                  Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];

                  try
                  {
                      string mail = sesionActiva.usuario.mail;

                      if (pagoMultiple.TipoDocumento != null)
                      {

                              if (pagoImprimir.TipoPago.Id == ConstantesTipoPago.TORNEO().Id)
                              {
                                  string sFile = gestorInscripciones.ComprobanteInscripcionPago(pagoImprimir.Inscripcion, mail);
                                  Response.Clear();
                                  Response.AddHeader("Content-Type", "Application/octet-stream");
                                  Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                                  Response.WriteFile(sFile);


                              }
                              if (pagoImprimir.TipoPago.Id == ConstantesTipoPago.EVENTO().Id)
                              {
                                  string sFile = gestorInscripcionesEventos.ComprobanteInscripcion(pagoImprimir.Inscripcion, mail);
                                  Response.Clear();
                                  Response.AddHeader("Content-Type", "Application/octet-stream");
                                  Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                                  Response.WriteFile(sFile);
                              }
                    }
                  }
                  catch (Exception ex)
                  {
                      mensaje("Ocurrió un error al tratar de generar el comprobante",false);
                  }

            }
        }
    }
}