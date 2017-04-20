using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;




namespace JJSS.Presentacion

{
    public partial class CrearTorneo : System.Web.UI.Page
    {
        GestorTorneos gestorTorneos;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorTorneos = new GestorTorneos();

            CargarComboSedes();



        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }




        protected void btn_aceptar_Click1(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                string nombre = txt_nombre.Text;
                DateTime fecha = new DateTime(99, 01, 01);
                DateTime fecha_cierre = fecha;
                if (dp_fecha.Text != "")
                {
                    fecha = DateTime.Parse(dp_fecha.Text);
                }
                if (dp_fecha_cierre.Text != "")
                {
                    fecha_cierre = DateTime.Parse(dp_fecha_cierre.Text);
                }
                decimal precio_abs = decimal.Parse(txt_precio_abs.Text.Replace(".", ","));
                decimal precio_cat = decimal.Parse(txt_precio_cat.Text.Replace(".", ","));
                string hora = ddl_hora.SelectedValue;
                string hora_cierre = ddl_hora_cierre.SelectedValue;
                int sede = 0;
                int.TryParse(ddl_sedes.SelectedValue, out sede);
                string sReturn = gestorTorneos.GenerarNuevoTorneo(fecha, nombre, precio_cat, precio_abs, hora, sede, fecha_cierre, hora_cierre);
            }

            
        }
    

    protected void CargarComboSedes() {
        List<sede> sedes = gestorTorneos.ObtenerSedes();
        ddl_sedes.DataSource = sedes;
        ddl_sedes.DataTextField = "nombre";
        ddl_sedes.DataValueField = "id_sede";
        ddl_sedes.DataBind();
     }
    }

 }


