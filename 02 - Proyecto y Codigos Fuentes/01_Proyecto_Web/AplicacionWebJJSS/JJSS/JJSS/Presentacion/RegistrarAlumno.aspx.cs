
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

namespace JJSS.Presentacion
{
    public partial class RegistrarAlumno : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorAlumnos gestorAlumnos;
        private GestorCiudades gestorCiudades;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorAlumnos = new GestorAlumnos();
            gestorInscripciones = new GestorInscripciones();
            gestorCiudades = new GestorCiudades();

            if (!IsPostBack)
            {
                CargarComboFajas();
                CargarComboCiudades();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajas();
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "color";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        protected void CargarComboCiudades()
        {
            List<ciudad> ciudades = gestorCiudades.ObtenerCiudades();
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
            DateTime fechaNac = DateTime.Parse(dp_fecha.Text);
            int idFaja = int.Parse(ddl_fajas.SelectedValue);
            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino
            int tel = int.Parse(txt_telefono.Text);
            string mail = txt_email.Text;
            string calle = txt_calle.Text;
            string departamento = txt_nro_dpto.Text;
            int piso = int.Parse(txt_piso.Text);
            int numero = int.Parse(txt_numero.Text);
            System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
            byte[] imagenByte;
            using (MemoryStream ms = new MemoryStream())
            {
                imagen.CopyTo(ms);
                imagenByte = ms.ToArray();
            }
            int telEmergencia = int.Parse(txt_telefono_urgencia.Text);
            //Image imagenPerfil = Avatar;

            string sReturn = gestorAlumnos.RegistrarAlumno(nombre, apellido, fechaNac, idFaja, 1, sexo, dni, tel, mail, 1, telEmergencia, imagenByte, calle, numero, departamento, piso);

            if (sReturn.CompareTo("") == 0) sReturn = "Se ha creado el alumno exitosamente";
            mensaje(sReturn, "RegistrarAlumno.aspx");

        }

        private void mensaje(string pMensaje, string pRef)
        {
            Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + pRef + "', 2000);</script>");
        }




    }
}