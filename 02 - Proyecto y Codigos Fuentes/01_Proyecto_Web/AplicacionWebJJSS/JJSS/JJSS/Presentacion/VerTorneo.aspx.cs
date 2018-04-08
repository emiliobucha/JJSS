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

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneos = new GestorTorneos();
            int idTorneo = 0;
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
            else Response.Redirect("../Presentacion/HistoricoTorneos.aspx");

        }

        private void cargarTabla()
        {
            gvResultados.DataSource = resultadosTorneo;
            gvResultados.DataBind();
        }

        private void cargarInformacion()
        {
            GestorSedes gestorSede = new GestorSedes();
            SedeDireccion sede = gestorSede.ObtenerDireccionSede((int)torneoSeleccionado.id_sede);

            lbl_nombre_torneo.Text = torneoSeleccionado.nombre;
            lbl_FechaCierreInscripcion.Text = torneoSeleccionado.fecha_cierre.Value.ToLongDateString();
            lbl_FechaDeTorneo.Text = torneoSeleccionado.fecha.Value.ToLongDateString();
            lbl_HoraTorneo.Text = torneoSeleccionado.hora.ToString();
            lbl_CostoInscripcion.Text = torneoSeleccionado.precio_categoria.ToString();
            lbl_CostoInscripcionAbsoluto.Text = torneoSeleccionado.precio_absoluto.ToString();
            lbl_HoraCierreTorneo.Text = torneoSeleccionado.hora_cierre.ToString();
            lbl_sede.Text = sede.sede;
            lbl_direccion_sede.Text = sede.calle + " " + sede.numero + " - " + sede.ciudad + " - " + sede.provincia + " - " + sede.pais;

        }

        private void verBotones()
        {
            //TODO debe verificar primero si es admin o no
            int idEstado = estadoTorneo.id_estado;
            Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
            //si es ADMIN
            btn_imprimir_listado.Visible = true;
            if (idEstado == ConstantesEstado.TORNEO_FINALIZADO)
            {
                btn_cargar_resultados.Visible = true;
            }
            if (idEstado != ConstantesEstado.TORNEO_FINALIZADO && idEstado != ConstantesEstado.TORNEO_CANCELADO)
            {
                btn_cancelar.Visible = true;
                btn_editar.Visible = true;
                if (idEstado != ConstantesEstado.TORNEO_SUSPENDIDO)
                {
                    btn_suspender.Visible = true;
                }
                else
                {
                    btn_habilitar.Visible = true;
                }
            }
            //si no es ADMIN
            if (idEstado == ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA)
            {
                btn_inscribir.Visible = true;
            }
        }

        protected void btn_cargar_resultados_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Response.Redirect("../Presentacion/CargarResultadosTorneo.aspx");
        }

        protected void btn_editar_resultados_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
        }

        protected void btn_inscribir_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Response.Redirect("../Presentacion/InscripcionTorneo.aspx");
            Session["torneoSeleccionado"] = torneoSeleccionado.id_torneo;
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            gestorTorneos.cancelarTorneo(torneoSeleccionado.id_torneo, ConstantesEstado.TORNEO_CANCELADO);
        }

        protected void btn_suspender_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            gestorTorneos.cancelarTorneo(torneoSeleccionado.id_torneo, ConstantesEstado.TORNEO_SUSPENDIDO);
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
        }

        protected void btn_habilitar_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            gestorTorneos.cancelarTorneo(torneoSeleccionado.id_torneo, ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA);
            gestorTorneos.cambiarEstadoTorneos();
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            limpiarMensaje();
            Response.Redirect("../Presentacion/HistoricoTorneos.aspx");
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
    }
}