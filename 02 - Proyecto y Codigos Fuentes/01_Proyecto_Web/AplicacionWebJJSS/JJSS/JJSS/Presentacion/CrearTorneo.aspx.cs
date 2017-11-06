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
        GestorTorneos gestorTorneos;



        protected void Page_Load(object sender, EventArgs e)
        {


            //try
            //{
            //    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
            //    if (sesionActiva.estado != "INGRESO ACEPTADO")
            //    {
            //        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            //}

            gestorTorneos = new GestorTorneos();
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_CREACION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {

                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }
                CargarComboSedes();




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
                string[] formats = { "MM/dd/yyyy" };
                if (dp_fecha.Text != "")
                {
                    fecha = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                }
                if (dp_fecha_cierre.Text != "")
                {
                    fecha_cierre = DateTime.ParseExact(dp_fecha_cierre.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                }



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
                string sReturn = gestorTorneos.GenerarNuevoTorneo(fecha, nombre, precio_cat, precio_abs, hora, sede, fecha_cierre, hora_cierre, imagenByte);

                if (sReturn.CompareTo("") == 0)
                {
                    sReturn = "El torneo se ha creado exitosamente";
                    limpiar();
                    mensaje(sReturn, true);
                }
                else mensaje(sReturn, false);

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
                string[] formats = { "MM/dd/yyyy" };
                DateTime fecha = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                DateTime fecha_cierre = DateTime.ParseExact(dp_fecha_cierre.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                string fecha_act = DateTime.Now.ToString("MM/dd/yyyy");
                DateTime fecha_actual = DateTime.ParseExact(fecha_act, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
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
                string[] formats = { "MM/dd/yyyy" };
                DateTime fecha = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                DateTime fecha_cierre = DateTime.ParseExact(dp_fecha_cierre.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
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

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx#section_torneos");
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
            if (ddl_sedes.Items.Count>0) ddl_sedes.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

        }
    }


}


