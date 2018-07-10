using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Administracion;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class VerTorneo : System.Web.UI.Page
    {
        private static GestorTorneos gestorTorneos;

        private static torneo torneoSeleccionado;
        private static estado estadoTorneo;
        private static List<ResultadoDeTorneo> resultadosTorneo;
        private static SedeDireccion sede;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneos = new GestorTorneos();
            if (!IsPostBack)
            {
                int idTorneo = 0;
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/Torneos/MenuTorneo.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                if (Session["idTorneo"] != null)
                {
                    idTorneo = (int)Session["idTorneo"];
                    torneoSeleccionado = gestorTorneos.BuscarTorneoPorID(idTorneo);
                    estadoTorneo = gestorTorneos.buscarEstadoTorneo(idTorneo);
                    resultadosTorneo = gestorTorneos.buscarResultados(idTorneo);
                    cargarTabla();
                    cargarInformacion();
                    verBotones();
                }
                else volverPaginaAnterior();
            }
        }

        private void cargarTabla()
        {
            gvResultados.Visible = true;
            gvResultados.DataSource = resultadosTorneo;
            gvResultados.DataBind();
            if (estadoTorneo.id_estado == ConstantesEstado.TORNEO_CANCELADO) gvResultados.Visible = false;
        }

        private void cargarInformacion()
        {
            GestorSedes gestorSede = new GestorSedes();
            sede = gestorSede.ObtenerDireccionSede((int)torneoSeleccionado.id_sede);

            lbl_nombre_torneo.Text = torneoSeleccionado.nombre;
            lbl_FechaCierreInscripcion.Text = torneoSeleccionado.fecha_cierre.Value.ToLongDateString();
            lbl_FechaDeTorneo.Text = torneoSeleccionado.fecha.Value.ToLongDateString();
            lbl_HoraTorneo.Text = torneoSeleccionado.hora.ToString();
            lbl_CostoInscripcion.Text = torneoSeleccionado.precio_categoria.ToString();
            lbl_CostoInscripcionAbsoluto.Text = torneoSeleccionado.precio_absoluto.ToString();
            lbl_HoraCierreTorneo.Text = torneoSeleccionado.hora_cierre.ToString();
            lbl_sede.Text = sede.sede;
            lbl_direccion_sede.Text = sede.calle + " " + sede.numero + " - B° " + sede.barrio + " - " + sede.ciudad + " - " + sede.provincia + " - " + sede.pais;

        }

        private void verBotones()
        {
            //TODO debe verificar primero si es admin o no

            try
            {



                if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                {
                    int idEstado = estadoTorneo.id_estado;
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


                   int idEstado = estadoTorneo.id_estado;
      

                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION_LISTA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso == 1)
                    {
                        btn_imprimir_listado.Visible = true;
                    }



                    if (idEstado == ConstantesEstado.TORNEO_FINALIZADO || idEstado == ConstantesEstado.TORNEO_EN_CURSO)
                    {

                        permiso = 0;
                        drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_RESULTADOS'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        }
                        if (permiso == 1)
                        {
                            btn_cargar_resultados.Visible = true;


                            btn_imprimir_resultados.Visible = true;
                        }


                    }
                    if (idEstado != ConstantesEstado.TORNEO_FINALIZADO && idEstado != ConstantesEstado.TORNEO_CANCELADO)
                    {

                        
                        permiso = 0;
                        drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_CANCELAR'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        }
                        if (permiso == 1)
                        {
                            btn_cancelar.Visible = true;
                        }

                        permiso = 0;
                        drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_EDITAR'");
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
                            drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_SUSPENDER'");
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
                            drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_HABILITAR'");
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
                        drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION'");
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

        protected void btn_cargar_resultados_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Response.Redirect("CargarResultadosTorneo.aspx");
        }

        protected void btn_editar_resultados_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
        }

        protected void btn_inscribir_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Session["idTorneo_inscribirTorneo"] = torneoSeleccionado.id_torneo;
            Response.Redirect("InscripcionTorneo.aspx");
        }

        protected void btn_si_Click1(object sender, EventArgs e)
        {
            limpiarMensaje();
            string res = gestorTorneos.cancelarTorneo(torneoSeleccionado.id_torneo, ConstantesEstado.TORNEO_CANCELADO);
            verBotones();
            if (res.CompareTo("") == 0)
            {
                Session["mensaje"] = "Se ha cancelado el torneo exitosamente";
                Session["exito"] = true;
                Response.Redirect("/Presentacion/Torneos/MenuTorneo.aspx");
            }
            else
            {
                mensaje(res, false);
            }

        }

        protected void btn_suspender_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            string res = gestorTorneos.cancelarTorneo(torneoSeleccionado.id_torneo, ConstantesEstado.TORNEO_SUSPENDIDO);
            verBotones();
            if (res.CompareTo("") == 0)
            {
                verBotones();
                mensaje("El torneo se suspendió", true);
            }
            else
            {
                mensaje(res, false);
            }
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Session["idTorneo_editar"] = torneoSeleccionado.id_torneo;
            Response.Redirect("CrearTorneo.aspx");
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
            gestorTorneos.cancelarTorneo(torneoSeleccionado.id_torneo, ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA);
            gestorTorneos.cambiarEstadoTorneos();
            verBotones();
            volverPaginaAnterior();
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Response.Redirect("/Presentacion/Torneos/MenuTorneo.aspx");
        }

        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultados.PageIndex = e.NewPageIndex;
            cargarTabla();
        }

        protected void btn_imprimir_listado_Click(object sender, EventArgs e)
        {
            try
            {
                String sFile = gestorTorneos.GenerarListado(torneoSeleccionado.id_torneo);

                Response.Clear();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                Response.WriteFile(sFile);
            }
            catch (Exception ex)
            {
                mensaje("No se encuentran alumnos inscriptos a ese torneo", false);
            }
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

        protected void btn_volver_Click1(object sender, EventArgs e)
        {
            limpiarMensaje();
            volverPaginaAnterior();
        }

        protected void btn_imprimir_resultados_Click(object sender, EventArgs e)
        {
            try
            {
                String sFile = gestorTorneos.GenerarListadoResultados(torneoSeleccionado,resultadosTorneo,sede);

                Response.Clear();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                Response.WriteFile(sFile);
            }
            catch (Exception ex)
            {
                mensaje("No se encuentran resultados cargados a ese torneo", false);
            }
        }
    }
}