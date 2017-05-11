using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

namespace JJSS.Presentacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorTorneos gestorDeTorneos;
        private torneo torneoSeleccionado;
        private int? idAlumno=null;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorInscripciones = new GestorInscripciones();
            if (!IsPostBack)
            {
                CargarComboFajas();
                pnl_Inscripcion.Visible = false;
                pnl_dni.Visible = true;
                limpiar(true);
            }
            gestorDeTorneos = new GestorTorneos();




            //   this.btn_confirmar_dni.ServerClick += MetodoClick;


            /*ClientScript.GetPostBackEventReference(this, string.Empty);

            if (Request.Form["__EVENTTARGET"] == "btn_confirmar_dni_Click")
            {
                //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
                btn_confirmar_dni_Click(this, new EventArgs());
            }*/
            btn_aceptar.Visible = false;
        }
        

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string calcularEdad(DateTime? pFechaNacimiento)
        {
            //+habria que ver como calcularla mejor porque si estuviera calculando mi edad daria 22 pero todavia tengo 21
            DateTime fechaNac = Convert.ToDateTime(pFechaNacimiento);
            int edad = DateTime.Today.Year - fechaNac.Year;
            return edad.ToString();
        }

        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajas();
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "color";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        private void limpiar(Boolean limpiaTodo)
        {
            txt_apellido.Text = "";
            txt_edad.Text = "";
            txt_nombre.Text = "";
            txt_peso.Text = "";
            //ya se que no usamos el index pero solo tiene que setearlo en el primer valor que haya en el combo
            ddl_fajas.SelectedIndex = 1;

            if (limpiaTodo == true)
            {
                txtDni.Text = "";
            }
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {

            //+Ver Bien el SelectedValue del combo

            int idTorneo = 1;

            //solo para invitados
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            float peso = float.Parse(txt_peso.Text);
            int edad = int.Parse(txt_edad.Text);
            int dni = int.Parse(txtDni.Text);


            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue, out idFaja);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

            //para todos
            gestorInscripciones.InscribirATorneo(idTorneo, nombre, apellido, peso, edad, idFaja, sexo, dni, idAlumno);
            limpiar(true);

        }       


        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            txt_apellido.ReadOnly = false;
            txt_nombre.ReadOnly = false;
            txt_edad.ReadOnly = false;
            ddl_fajas.Enabled = true;
            rbSexo.Enabled = true;

            limpiar(false);
            pnl_Inscripcion.Visible = true;
            participante participanteEncontrado = gestorInscripciones.ObtenerParticipanteporDNI(int.Parse(txtDni.Text));

            //Partipante ya estaba inscripto con ese dni
            if (participanteEncontrado != null) return;

            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(int.Parse(txtDni.Text));
            if (alumnoEncontrado != null)
            {
                //Completa los campos con los datos del alumno, asi luego cuando se va a inscribir, al participante ya le manda los datos y no hay que modificar el metodo de carga de participantes
                txt_apellido.ReadOnly = true;
                txt_nombre.ReadOnly = true;
                txt_edad.ReadOnly = true;
                ddl_fajas.Enabled = false;
                rbSexo.Enabled = false;

                txt_apellido.Text = alumnoEncontrado.apellido;
                txt_nombre.Text = alumnoEncontrado.nombre;

                txt_edad.Text = calcularEdad(alumnoEncontrado.fecha_nacimiento);

                ddl_fajas.SelectedValue = alumnoEncontrado.id_faja.ToString();

                if (alumnoEncontrado.sexo == 0) rbSexo.SelectedIndex = 0;
                if (alumnoEncontrado.sexo == 1) rbSexo.SelectedIndex = 1;

                idAlumno = alumnoEncontrado.id_alumno;
            }
            btn_aceptar.Visible = true;

        }

        protected void btn_confirmar_dni_Click(object sender, EventArgs e)
        {
            txt_apellido.ReadOnly = false;
            txt_nombre.ReadOnly = false;
            txt_edad.ReadOnly = false;
            ddl_fajas.Enabled = true;
            rbSexo.Enabled = true;

            pnl_Inscripcion.Visible = true;
            participante participanteEncontrado = gestorInscripciones.ObtenerParticipanteporDNI(int.Parse(txt_dni.Text));

            //Partipante ya estaba inscripto con ese dni
            if (participanteEncontrado != null) return;

            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(int.Parse(txt_dni.Text));
            if (alumnoEncontrado != null)
            {
                //Completa los campos con los datos del alumno, asi luego cuando se va a inscribir, al participante ya le manda los datos y no hay que modificar el metodo de carga de participantes
                txt_apellido.ReadOnly = true;
                txt_nombre.ReadOnly = true;
                txt_edad.ReadOnly = true;
                ddl_fajas.Enabled = false;
                rbSexo.Enabled = false;

                txt_apellido.Text = alumnoEncontrado.apellido;
                txt_nombre.Text = alumnoEncontrado.nombre;

                DateTime fechaNac = Convert.ToDateTime(alumnoEncontrado.fecha_nacimiento);
                int edad = DateTime.Today.Year - fechaNac.Year;
                txt_edad.Text = edad.ToString();

                ddl_fajas.SelectedValue = alumnoEncontrado.id_faja.ToString();

                if (alumnoEncontrado.sexo == 0) rbSexo.SelectedIndex = 0;
                if (alumnoEncontrado.sexo == 1) rbSexo.SelectedIndex = 1;

                idAlumno = alumnoEncontrado.id_alumno;
               // ClientScript.RegisterStartupScript(this.GetType(), "myScript", "mostraPanelInscripcion();", false);
            }
        }
    }
}