﻿using System;
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
            gestorTorneos = new GestorTorneos();
            if (!IsPostBack) CargarComboSedes();




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
                string hora = ddl_hora.SelectedValue;
                string hora_cierre = ddl_hora_cierre.SelectedValue;
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

                if (sReturn.CompareTo("") == 0) sReturn = "El torneo se ha creado exitosamente";
                mensaje(sReturn, "CrearTorneo.aspx");
            }


        }

        /*Resumen:
         * Muestra un cuadro de texto en la pantalla
         * 
         * Paramétros: 
         *              pMensaje: el mensaje que se va a mostrar
         *              pRef: la pagina .aspx que va a redireccionar
         **/
        private void mensaje(string pMensaje, string pRef)
        {
            Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + pRef + "', 2000);</script>");
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
                if (fecha_cierre <= fecha)
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

        protected void btn_mas_Click(object sender, EventArgs e)
        {

        }
    }


}


