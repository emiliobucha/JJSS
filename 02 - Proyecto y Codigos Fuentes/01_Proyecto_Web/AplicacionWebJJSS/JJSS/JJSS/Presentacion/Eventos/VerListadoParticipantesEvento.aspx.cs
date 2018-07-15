using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

namespace JJSS.Presentacion.Eventos
{
    public partial class VerListadoParticipantesEvento : System.Web.UI.Page
    {

        private static GestorEventos gestorEventos;
        private static evento_especial eventoSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion) HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_INSCRIPCION_LISTA'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" +
                                           "No se encuentra logueado correctamente o no tiene los permisos para estar aquí"
                                               .Trim() + "');</script>" + "<script>window.setTimeout(location.href='" +
                                           "../Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() +
                                   "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" +
                                   "', 2000);</script>");
                }


            }

            gestorEventos = new GestorEventos();
            if (Session["idEvento"] != null)
            {
                var idEvento = (int)Session["idEvento"];
                eventoSeleccionado = gestorEventos.BuscarEventoPorID(idEvento);

                CargarGrilla();
                CargarInformacion();

            }
            else
            {
                Response.Redirect("MenuTorneo.aspx");
            }

        }

        private void CargarGrilla()
        {
            var listado = gestorEventos.ListadoParticipantes(eventoSeleccionado.id_evento);


            gv_participantes.Visible = true;
            gv_participantes.DataSource = listado;
            gv_participantes.DataBind();

        }

        private void CargarInformacion()
        {
            lbl_nombre_torneo.Text = eventoSeleccionado.nombre;
        }

        protected void gv_participantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_participantes.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }


        protected void btn_imprimir_Click(object sender, EventArgs e)
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
                Mensaje("No se encuentran alumnos inscriptos a ese torneo", false);
            }
        }

        private void Mensaje(string pMensaje, bool pEstado)
        {
            if (pEstado)
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