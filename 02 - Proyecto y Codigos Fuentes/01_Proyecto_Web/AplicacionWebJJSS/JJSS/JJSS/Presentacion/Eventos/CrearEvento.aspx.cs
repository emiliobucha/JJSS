using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Globalization;
using System.IO;

namespace JJSS.Presentacion
{
    public partial class CrearEvento : System.Web.UI.Page
    {
        private static GestorEventos gestorEvento;
        private static evento_especial eventoAEditar;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorEvento = new GestorEventos();
            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/Eventos/Menu_Evento.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_CREACION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o no tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {

                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                }
                CargarComboSedes();
                CargarComboTipos();

                if (Session["mensaje"] != null && Session["mensaje"].ToString().Trim() != "")
                {
                    mensaje(Session["mensaje"].ToString(), Convert.ToBoolean(Session["exito"]));
                    Session["mensaje"] = null;
                }

                eventoAEditar = null;
                if (Session["eventoSeleccionado"] != null)
                {
                    int idEvento = int.Parse(Session["eventoSeleccionado"].ToString());
                    eventoAEditar= gestorEvento.BuscarEventoPorID(idEvento);
                    var eventoResultado = gestorEvento.ObtenerEventoResultado(idEvento);

                    txt_nombre.Text = eventoResultado.nombre;
                    txt_precio.Text = eventoResultado.precio.ToString().Replace(",", ".");
                    txt_hora.Text = eventoResultado.hora;
                    txt_hora_cierre.Text = eventoResultado.hora_cierre;
                    dp_fecha.Text = ((DateTime)eventoResultado.fecha).ToString("dd/MM/yyyy");
                    dp_fecha_cierre.Text = ((DateTime)eventoResultado.dtFechaCierre).ToString("dd/MM/yyyy");
                    Avatar.ImageUrl = eventoResultado.imagen;
                    
                    ddl_sedes.SelectedItem.Value = eventoResultado.idSede.ToString();
                    Session["eventoSeleccionado"] = null;
                }
                else
                {
                    limpiar();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                string nombre = txt_nombre.Text;
                DateTime fecha = new DateTime(99, 01, 01);
                DateTime fecha_cierre = fecha;
                fecha = DateTime.Parse(dp_fecha.Text);
                fecha_cierre = DateTime.Parse(dp_fecha_cierre.Text);

                decimal precio = decimal.Parse(txt_precio.Text.Replace(".", ","));
                string hora = txt_hora.Text;
                string hora_cierre = txt_hora_cierre.Text;
                int sede = 0;
                int.TryParse(ddl_sedes.SelectedValue, out sede);
                int tipo = 0;
                int.TryParse(dll_tipo_evento.SelectedValue, out tipo);
                System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
                byte[] imagenByte;
                using (MemoryStream ms = new MemoryStream())
                {
                    imagen.CopyTo(ms);
                    imagenByte = ms.ToArray();
                }

                if (eventoAEditar == null)
                {
                    string sReturn = gestorEvento.GenerarNuevoEvento(fecha, nombre, precio, hora, sede, fecha_cierre, hora_cierre, imagenByte, tipo);

                    if (sReturn.CompareTo("") == 0)
                    {
                        sReturn = "El evento se ha creado exitosamente";
                        limpiar();
                        Session["mensaje"] = sReturn;
                        Session["exito"] = true;
                        Response.Redirect("Menu_Evento.aspx");

                    }
                    else mensaje(sReturn, false);
                }
                else
                {
                    string sReturn = "";
                    eventoAEditar.nombre = nombre;
                    eventoAEditar.precio= precio;
                    eventoAEditar.hora_cierre = hora_cierre;
                    eventoAEditar.hora = hora;
                    eventoAEditar.id_sede = sede;
                    eventoAEditar.fecha = fecha;
                    eventoAEditar.fecha_cierre = fecha_cierre;
                    sReturn = gestorEvento.modificarEvento(eventoAEditar, imagenByte);

                    if (sReturn.CompareTo("") == 0)
                    {
                        sReturn = "El evento se ha modificado exitosamente";
                        limpiar();
                        Session["mensaje"] = sReturn;
                        Session["exito"] = true;
                        Response.Redirect("Menu_Evento.aspx");
                    }
                    else mensaje(sReturn, false);
                }
            }
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

        private void limpiar()
        {
            txt_hora.Text = "";
            txt_hora_cierre.Text = "";
            txt_nombre.Text = "";
            txt_precio.Text = "";
            dll_tipo_evento.SelectedIndex = 0;
            ddl_sedes.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void CargarComboSedes()
        {
            List<sede> sedes = gestorEvento.ObtenerSedes();
            ddl_sedes.DataSource = sedes;
            ddl_sedes.DataTextField = "nombre";
            ddl_sedes.DataValueField = "id_sede";
            ddl_sedes.DataBind();
        }

        protected void CargarComboTipos()
        {
            JJSS_Negocio.Administracion.GestorTipoEvento gte = new JJSS_Negocio.Administracion.GestorTipoEvento();
            List<tipo_evento_especial> tipos = gte.ObtenerTodosTipoEventosConFiltro("");
            dll_tipo_evento.DataSource = tipos;
            dll_tipo_evento.DataTextField = "nombre";
            dll_tipo_evento.DataValueField = "id_tipo_evento";
            dll_tipo_evento.DataBind();
        }

        protected void val_fecha_actual_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                /*FECHA SOMEE
                string[] formats = { "MM/dd/yyyy" };
                DateTime fecha = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                DateTime fecha_cierre = DateTime.ParseExact(dp_fecha_cierre.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                string fecha_act = DateTime.Now.ToString("MM/dd/yyyy");
                DateTime fecha_actual = DateTime.ParseExact(fecha_act, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                */
                //LOCAL
                DateTime fecha = DateTime.Parse(dp_fecha.Text);
                DateTime fecha_cierre = DateTime.Parse(dp_fecha_cierre.Text);
                DateTime fecha_actual = DateTime.Today;

                if (fecha >= fecha_actual && fecha_cierre >= fecha_actual)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void val_fechas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                /*FECHA SOMEE
                string[] formats = { "MM/dd/yyyy" };
                DateTime fecha = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                DateTime fecha_cierre = DateTime.ParseExact(dp_fecha_cierre.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                */
                //LOCAL
                DateTime fecha = DateTime.Parse(dp_fecha.Text);
                DateTime fecha_cierre = DateTime.Parse(dp_fecha_cierre.Text);

                if (fecha_cierre < fecha) args.IsValid = true;
                else if (fecha_cierre > fecha) args.IsValid = false;
                else //son iguales
                {
                    TimeSpan hora =TimeSpan.Parse( txt_hora.Text);
                    TimeSpan horaCierre = TimeSpan.Parse( txt_hora_cierre.Text);
                    if (horaCierre <= hora) args.IsValid = true;
                    else args.IsValid = false;
                }
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect(ViewState["RefUrl"].ToString());
        }
    }
}