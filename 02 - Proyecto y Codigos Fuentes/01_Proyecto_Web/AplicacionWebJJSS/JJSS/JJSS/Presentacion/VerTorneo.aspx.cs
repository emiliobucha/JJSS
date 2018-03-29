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
            gvResultados.DataSource = gestorTorneos.buscarResultados(torneoSeleccionado.id_torneo);
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
            if (idEstado == ConstantesEstado.TORNEO_FINALIZADO)
            {
                if (resultadosTorneo != null && resultadosTorneo.Count > 0)
                {
                    btn_editar_resultados.Visible = true;
                }
                else
                {
                    btn_cargar_resultados.Visible = true;
                }
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

        }

        protected void btn_editar_resultados_Click(object sender, EventArgs e)
        {

        }

        protected void btn_inscribir_Click(object sender, EventArgs e)
        {

        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_suspender_Click(object sender, EventArgs e)
        {

        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_habilitar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {

        }

        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}