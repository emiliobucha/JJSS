
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.IO;
using System.Globalization;

namespace JJSS.Presentacion
{
    public partial class RegistrarProfe : System.Web.UI.Page
    {

        private GestorInscripciones gestorInscripciones;
        private GestorProfesores gestorProfes;
        private GestorCiudades gestorCiudades;
        private GestorProvincias gestorProvincias;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorProfes = new GestorProfesores();
            gestorInscripciones = new GestorInscripciones();
            gestorCiudades = new GestorCiudades();
            gestorProvincias = new GestorProvincias();
            


            if (!IsPostBack)
            {
                CargarComboFajas();
                CargarComboCiudades(1);
                CargarComboProvincias();
            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajas();
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "color";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        protected void CargarComboProvincias()
        {
            List<provincia> provincias = gestorProvincias.ObtenerProvincias();
            ddl_provincia.DataSource = provincias;
            ddl_provincia.DataTextField = "nombre";
            ddl_provincia.DataValueField = "id_provincia";
            ddl_provincia.DataBind();
        }

        protected void CargarComboCiudades(int pProvincia)
        {
            List<ciudad> ciudades = gestorCiudades.ObtenerCiudadesPorProvincia(pProvincia);
            ddl_localidad.DataSource = ciudades;
            ddl_localidad.DataTextField = "nombre";
            ddl_localidad.DataValueField = "id_ciudad";
            ddl_localidad.DataBind();
        }

        protected void btn_guardar_click(object sender, EventArgs e)
        {
            //+ver categorias, direccion y foto perfil
            int dni = int.Parse(txtDni.Text);
            string nombre = txt_nombres.Text;
            string apellido = txt_apellido.Text;

            string[] formats = { "MM/dd/yyyy" };
            DateTime fechaNac = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
            int idFaja = int.Parse(ddl_fajas.SelectedValue);
            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

            int tel = 0;
            if (txt_telefono.Text != "")
            {
                tel = int.Parse(txt_telefono.Text);
            }

            string mail = txt_email.Text;
            string calle = txt_calle.Text;
            string departamento = txt_nro_dpto.Text;

            int? piso = null;
            if (txt_piso.Text != "")
            {
                piso = int.Parse(txt_piso.Text);
            }
            int? numero = null;
            if (txt_numero.Text != "")
            {
                numero = int.Parse(txt_numero.Text);
            }
            System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
            byte[] imagenByte;
            using (MemoryStream ms = new MemoryStream())
            {
                imagen.CopyTo(ms);
                imagenByte = ms.ToArray();
            }
            int telEmergencia = int.Parse(txt_telefono_urgencia.Text);
            //Image imagenPerfil = Avatar;

            int ciudad = int.Parse(ddl_localidad.SelectedValue);

            string sReturn = gestorProfes.RegistrarProfesor(nombre, apellido, fechaNac, idFaja, sexo, dni, tel, mail, telEmergencia, imagenByte, calle, numero, departamento, piso, ciudad);
            if (sReturn.CompareTo("") == 0) mensaje("Se ha creado el profesor exitosamente", true);
            else mensaje(sReturn, false);
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

        protected void ddl_provincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboCiudades(int.Parse(ddl_localidad.SelectedValue));
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("Inicio.aspx");
        }

        private void limpiar()
        {
            txtDni.Text = "";
            txt_apellido.Text = "";
            txt_calle.Text = "";
            txt_email.Text = "";
            txt_filtro_dni.Text = "";
            txt_nombres.Text = "";
            txt_nro_dpto.Text = "";
            txt_numero.Text = "";
            txt_piso.Text = "";
            txt_telefono.Text = "";
            txt_telefono_urgencia.Text = "";
            ddl_fajas.SelectedIndex = 0;
            ddl_localidad.SelectedIndex = 0;
            ddl_provincia.SelectedIndex = 0;
        }

        protected void btn_ver_profes_Click(object sender, EventArgs e)
        {

        }

        protected void btn_buscar_profe_Click(object sender, EventArgs e)
        {

        }

        protected void btn_registro_Click(object sender, EventArgs e)
        {

        }

        protected void gvprofes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvprofes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvprofes_PageIndexChanged(object sender, EventArgs e)
        {

        }
    }
}