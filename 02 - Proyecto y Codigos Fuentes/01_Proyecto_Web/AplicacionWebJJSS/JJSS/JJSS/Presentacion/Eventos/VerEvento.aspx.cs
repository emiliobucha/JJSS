using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Administracion;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Constantes;

namespace JJSS.Presentacion.Eventos
{
    public partial class VerEvento : System.Web.UI.Page
    {
        private static GestorEventos gestorEventos;

        private static evento_especial eventoSeleccionado;
        private static estado estadoEvento;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEventos = new GestorEventos();
            if (!IsPostBack)
            {
                int idEvento = 0;
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "Menu_Evento.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                if (Session["eventoSeleccionado"] != null)
                {
                    idEvento = (int)Session["eventoSeleccionado"];
                    Session["eventoSeleccionado"] = null;
                    eventoSeleccionado = gestorEventos.BuscarEventoPorID(idEvento);
                    estadoEvento = gestorEventos.buscarEstadoEvento(idEvento);
                    cargarInformacion();
                    verBotones();
                }
                else volverPaginaAnterior();
            }
        }
        
        private void cargarInformacion()
        {
            GestorSedes gestorSede = new GestorSedes();
            SedeDireccion sede = gestorSede.ObtenerDireccionSede((int)eventoSeleccionado.id_sede);

            lbl_nombre_torneo.Text = eventoSeleccionado.nombre;
            lbl_FechaCierreInscripcion.Text = eventoSeleccionado.fecha_cierre.Value.ToLongDateString();
            lbl_FechaDeTorneo.Text = eventoSeleccionado.fecha.Value.ToLongDateString();
            lbl_HoraTorneo.Text = eventoSeleccionado.hora.ToString();
            lbl_CostoInscripcion.Text = eventoSeleccionado.precio.ToString();
            lbl_HoraCierreTorneo.Text = eventoSeleccionado.hora_cierre.ToString();
            lbl_sede.Text = sede.sede;
            lbl_direccion_sede.Text = sede.calle + " " + sede.numero + " - B° " + sede.barrio + " - " + sede.ciudad + " - " + sede.provincia + " - " + sede.pais;
            int idTipo = 0;
            if (eventoSeleccionado.id_tipo_evento != null)
            {
                idTipo = (int)eventoSeleccionado.id_tipo_evento;
            }
            lbl_tipo_evento.Text = gestorEventos.buscarTipoEvento(idTipo).nombre;

        }

        private void verBotones()
        {
            //TODO debe verificar primero si es admin o no

            try
            {



                if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                {
                    int idEstado = estadoEvento.id_estado;
                    if (idEstado == ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA)
                    {
                        btn_inscribir.Visible = true;
                    }



                }
                else
                {

                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado != "INGRESO ACEPTADO")
                    {

                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                    }

                    int idEstado = estadoEvento.id_estado;


                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_INSCRIPCION_LISTA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso == 1)
                    {
                        //btn_imprimir_listado.Visible = true;
                        btn_ver_listado.Visible = true;
                    }

                
                    if (idEstado != ConstantesEstado.TORNEO_FINALIZADO && idEstado != ConstantesEstado.TORNEO_CANCELADO)
                    {
                        permiso = 0;
                        drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_CANCELAR'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        }
                        if (permiso == 1)
                        {
                            btn_cancelar.Visible = true;
                        }

                        permiso = 0;
                        drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_EDITAR'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        }
                        if (permiso == 1)
                        {
                            btn_editar.Visible = true;
                        }


                        if (idEstado != ConstantesEstado.TORNEO_SUSPENDIDO)
                        {

                            permiso = 0;
                            drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_SUSPENDER'");
                            if (drsAux.Length > 0)
                            {
                                int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                            }
                            if (permiso == 1)
                            {
                                btn_suspender.Visible = true;
                            }

                        }
                        else
                        {

                            permiso = 0;
                            drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_HABILITAR'");
                            if (drsAux.Length > 0)
                            {
                                int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                            }
                            if (permiso == 1)
                            {
                                btn_habilitar.Visible = true;
                            }

                        }
                    }


                    if (idEstado == ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA)
                    {
                        permiso = 0;
                        drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_INSCRIPCION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        }
                        if (permiso == 1)
                        {
                            btn_inscribir.Visible = true;
                        }


                    }



                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

            }



        }

        protected void btn_inscribir_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Session["eventoSeleccionado"] = eventoSeleccionado.id_evento;
            Response.Redirect("InscripcionEvento.aspx");
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            string res = gestorEventos.cancelarEvento(eventoSeleccionado.id_evento, ConstantesEstado.TORNEO_CANCELADO);
            verBotones();
            if (res.CompareTo("") == 0)
            {
                Session["mensaje"] = "Se ha cancelado el evento exitosamente";
                Session["exito"] = true;
                Response.Redirect("Menu_Evento.aspx");
            }
            else
            {
                mensaje(res, false);
            }
        }

        protected void btn_suspender_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            string res = gestorEventos.cancelarEvento(eventoSeleccionado.id_evento, ConstantesEstado.TORNEO_SUSPENDIDO);
            verBotones();
            if (res.CompareTo("") == 0)
            {
                verBotones();
                mensaje("El evento se suspendió", true);
            }
            else
            {
                mensaje(res, false);
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Session["eventoSeleccionado"] = eventoSeleccionado.id_evento;
            Response.Redirect("CrearEvento.aspx");
        }

        private void volverPaginaAnterior()
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }

        protected void btn_habilitar_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            gestorEventos.cancelarEvento(eventoSeleccionado.id_evento, ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA);
            gestorEventos.cambiarEstadoEventos();
            verBotones();
            volverPaginaAnterior();
        }

        protected void btn_imprimir_listado_Click(object sender, EventArgs e)
        {
            try
            {
                String sFile = gestorEventos.GenerarListado(eventoSeleccionado.id_evento);

                Response.Clear();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                Response.WriteFile(sFile);
            }
            catch (Exception ex)
            {
                mensaje("No se encuentran alumnos inscriptos a ese evento", false);
            }
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            volverPaginaAnterior();
        }

        private void limpiarMensaje()
        {
            pnl_mensaje_exito.Visible = false;
            pnl_mensaje_error.Visible = false;
            lbl_exito.Text = "";
            lbl_error.Text = "";
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


        protected void btn_si_Click1(object sender, EventArgs e)
        {
            limpiarMensaje();
            string res = gestorEventos.cancelarEvento(eventoSeleccionado.id_evento, ConstantesEstado.TORNEO_CANCELADO);
            verBotones();
            if (res.CompareTo("") == 0)
            {
                Session["mensaje"] = "Se ha cancelado el torneo exitosamente";
                Session["exito"] = true;
                Response.Redirect("/Presentacion/Eventos/Menu_Evento.aspx");
            }
            else
            {
                mensaje(res, false);
            }

        }

        protected void btn_ver_listado_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Session["idEvento"] = eventoSeleccionado.id_evento;
            Response.Redirect("/Presentacion/Eventos/VerListadoParticipantesEvento.aspx");
        }
    }
}