using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

namespace JJSS.Presentacion.Torneos
{
    public partial class VerListadoParticipantesTorneo : System.Web.UI.Page
    {

        private static GestorTorneos gestorTorneos;
        private static torneo torneoSeleccionado;

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
                        System.Data.DataRow[] drsAux =
                            sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION_LISTA'");
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


            gestorTorneos = new GestorTorneos();
            if (Session["idTorneo"] != null)
            {
                var idTorneo = (int)Session["idTorneo"];
                torneoSeleccionado = gestorTorneos.BuscarTorneoPorID(idTorneo);

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
            var listado = gestorTorneos.ListadoParticipantes(torneoSeleccionado.id_torneo);


            gv_participantes.Visible = true;
            gv_participantes.DataSource = listado;
            gv_participantes.DataBind();

        }

        private void CargarInformacion()
        {
            lbl_nombre_torneo.Text = torneoSeleccionado.nombre;
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
                String sFile = gestorTorneos.GenerarListado(torneoSeleccionado.id_torneo);

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