using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class Menu_Evento : System.Web.UI.Page
    {
        private GestorEventos gestorEventos;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {



                if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                {
                    ocultarInvitado();

                }
                else
                {

                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado != "INGRESO ACEPTADO")
                    {

                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                    }
                    ocultarPermiso();
                }

            }
            catch (Exception ex)
            {

                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

            }

            gestorEventos = new GestorEventos();
            if (!IsPostBack)
            {
                dp_filtro_fecha_desde.Text = DateTime.Today.ToString("dd/MM/yyyy");
                dp_filtro_fecha_hasta.Text = DateTime.Today.AddYears(2).ToString("dd/MM/yyyy");
                CargarListViewEventos();
                if (Session["mensaje"] != null)
                {
                    mensaje(Session["mensaje"].ToString(), Boolean.Parse(Session["exito"].ToString()));
                    Session["mensaje"] = null;
                }
            }
        }

        private void CargarListViewEventos()
        {
            String filtroNombre = txt_filtro_nombre.Text;

            DateTime filtroFecha = new DateTime();
            if (dp_filtro_fecha_desde.Text.CompareTo("") != 0)
            {
                filtroFecha = DateTime.Parse(dp_filtro_fecha_desde.Text);
            }
            DateTime filtroFechaHasta = new DateTime();
            if (dp_filtro_fecha_hasta.Text.CompareTo("") != 0)
            {
                filtroFechaHasta = DateTime.Parse(dp_filtro_fecha_hasta.Text);
            }

            gestorEventos = new GestorEventos();
            lv_eventos.DataSource = gestorEventos.ObtenerEventosConImagenYFiltro(filtroNombre, filtroFecha, filtroFechaHasta);
            lv_eventos.DataBind();
        }

        protected void ocultarPermiso()
        {
            try
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {




                    //Administración de torneos

                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        crear_evento.Style["display"] = "none";
                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_HISTORIAL'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        historial_evento.Style["display"] = "none";
                    }



                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_INSCRIPCION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ver"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        //item_mis_Torneos.Style["display"] = "none";
                    }



                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

            }
        }

        private void ocultarInvitado()
        {
            crear_evento.Style["display"] = "none";
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
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

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            CargarListViewEventos();
        }

        protected void lv_eventos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("inscribir") == 0)
            {
                Session["eventoSeleccionado"] = id;
                Response.Redirect("InscripcionEvento.aspx");
            } else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["eventoSeleccionado"] = id;
                Response.Redirect("VerEvento.aspx");
            }
            
        }

    }
}