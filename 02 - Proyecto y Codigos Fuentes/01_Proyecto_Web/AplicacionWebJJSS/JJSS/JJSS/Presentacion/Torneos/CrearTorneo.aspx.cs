using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;
using System.IO;
using System.Globalization;



namespace JJSS.Presentacion

{
    public partial class CrearTorneo : System.Web.UI.Page
    {
        private GestorTorneos gestorTorneos;
        private static torneo torneoAEditar;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneos = new GestorTorneos();
            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/Torneos/MenuTorneo.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                //try
                //{
                //    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                //    if (sesionActiva.estado == "INGRESO ACEPTADO")
                //    {
                //        int permiso = 0;
                //        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_CREACION'");
                //        if (drsAux.Length > 0)
                //        {
                //            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

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
                CargarComboSedes();
                torneoAEditar = null;
                if (Session["idTorneo_editar"] != null)
                {
                    int idTorneo = int.Parse(Session["idTorneo_editar"].ToString());
                    torneoAEditar = gestorTorneos.BuscarTorneoPorID(idTorneo);

                    txt_nombre.Text = torneoAEditar.nombre;
                    txt_precio_abs.Text = torneoAEditar.precio_absoluto.ToString().Replace(",", ".");
                    txt_precio_cat.Text = torneoAEditar.precio_categoria.ToString().Replace(",", ".");
                    txt_hora.Text = torneoAEditar.hora;
                    txt_hora_cierre.Text = torneoAEditar.hora_cierre;
                    DateTime dt = (DateTime)torneoAEditar.fecha;
                    dp_fecha.Text = dt.ToString("dd/MM/yyyy");
                    dt = (DateTime)torneoAEditar.fecha_cierre;
                    dp_fecha_cierre.Text = dt.ToString("dd/MM/yyyy");

                    ddl_sedes.SelectedItem.Value = torneoAEditar.id_sede.ToString();
                    Session["idTorneo_editar"] = null;
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

        protected void btn_aceptar_Click1(object sender, EventArgs e)
        {
            Page.Validate();
            //string sReturn;
            if (Page.IsValid)
            {

                string nombre = txt_nombre.Text;
                DateTime fecha = new DateTime(99, 01, 01);
                DateTime fecha_cierre = fecha;

                /*FECHA SOMEE
                string[] formats = { "MM/dd/yyyy" };
                if (dp_fecha.Text != "")
                {
                    fecha = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                }
                if (dp_fecha_cierre.Text != "")
                {
                    fecha_cierre = DateTime.ParseExact(dp_fecha_cierre.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                }
                */
                //LOCAL
                fecha = DateTime.Parse(dp_fecha.Text);
                fecha_cierre = DateTime.Parse(dp_fecha_cierre.Text);



                decimal precio_abs = decimal.Parse(txt_precio_abs.Text.Replace(".", ","));
                decimal precio_cat = decimal.Parse(txt_precio_cat.Text.Replace(".", ","));
                string hora = txt_hora.Text;
                string hora_cierre = txt_hora_cierre.Text;
                int sede = 0;
                int.TryParse(ddl_sedes.SelectedValue, out sede);
                System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
                byte[] imagenByte;
                using (MemoryStream ms = new MemoryStream())
                {
                    imagen.CopyTo(ms);
                    imagenByte = ms.ToArray();
                }
                string sReturn = "";
                if (torneoAEditar == null)
                {//nuevo torneo
                    sReturn = gestorTorneos.GenerarNuevoTorneo(fecha, nombre, precio_cat, precio_abs, hora, sede, fecha_cierre, hora_cierre, imagenByte);

                    if (sReturn.CompareTo("") == 0)
                    {
                        sReturn = "El torneo se ha creado exitosamente";
                        limpiar();
                        mensaje(sReturn, true);
                        Session["mensaje"] = sReturn;
                        Session["exito"] = true;
                        Response.Redirect("/Presentacion/Torneos/MenuTorneo.aspx");
                    }
                    else mensaje(sReturn, false);

                }
                else
                {//editar torneo
                    torneoAEditar.nombre = nombre;
                    torneoAEditar.precio_absoluto = precio_abs;
                    torneoAEditar.precio_categoria = precio_cat;
                    torneoAEditar.hora_cierre = hora_cierre;
                    torneoAEditar.hora = hora;
                    torneoAEditar.id_sede = sede;
                    torneoAEditar.fecha = fecha;
                    torneoAEditar.fecha_cierre = fecha_cierre;
                    sReturn = gestorTorneos.modificarTorneo(torneoAEditar, imagenByte);

                    if (sReturn.CompareTo("") == 0)
                    {
                        sReturn = "El torneo se ha modificado exitosamente";
                        limpiar();
                        Session["mensaje"] = sReturn;
                        Session["exito"] = true;
                        Response.Redirect("/Presentacion/Torneos/MenuTorneo.aspx");
                    }
                    else mensaje(sReturn, false);
                }
            }
        }


        /*Resumen:
         * Muestra un cuadro de texto en la pantalla
         * 
         * Paramétros: 
         *              pMensaje: el mensaje que se va a mostrar
         *              pEstado: true si es exito - false si es error
         **/
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


        protected void CargarComboSedes()
        {
            List<sede> sedes = gestorTorneos.ObtenerSedes();
            ddl_sedes.DataSource = sedes;
            ddl_sedes.DataTextField = "nombre";
            ddl_sedes.DataValueField = "id_sede";
            ddl_sedes.DataBind();
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
                    TimeSpan hora = TimeSpan.Parse(txt_hora.Text);
                    TimeSpan horaCierre = TimeSpan.Parse(txt_hora_cierre.Text);
                    if (horaCierre <= hora) args.IsValid = true;
                    else args.IsValid = false;
                }
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void btn_mas_Click(object sender, EventArgs e)
        {

        }

        private void limpiar()
        {
            txt_hora.Text = "";
            txt_hora_cierre.Text = "";
            txt_nombre.Text = "";
            txt_precio_abs.Text = "";
            txt_precio_cat.Text = "";
            dp_fecha.Text = "";
            dp_fecha_cierre.Text = "";
            if (ddl_sedes.Items.Count > 0) ddl_sedes.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            Session["idTorneo_editar"] = null;
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
    }


}


