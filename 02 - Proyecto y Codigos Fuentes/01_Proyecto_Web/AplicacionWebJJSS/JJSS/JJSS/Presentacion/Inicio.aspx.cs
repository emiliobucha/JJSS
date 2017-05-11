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
            }
            gestorDeTorneos = new GestorTorneos();

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

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {

            //+Acá tendria que entrar como parametro el id del torneo, cuando generemos dinamicamente las imagenes y las cosas de los torneos, el mismo boton ya tiene que tener el id del torneo
            int idTorneo = 1;
            //int.TryParse(ddl_torneos.SelectedValue, out idTorneo);

            //solo para invitados
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            float peso = float.Parse(txt_peso.Text);
            int edad = int.Parse(txt_edad.Text);
            int dni = int.Parse(txt_dni.Text);
            

            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue, out idFaja);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

            //para todos
            gestorInscripciones.InscribirATorneo(idTorneo, nombre, apellido, peso, edad, idFaja, sexo, dni, idAlumno);

        }

        protected void btn_confirmarDni_Click(object sender, EventArgs e)
        {

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
                int edad =DateTime.Today.Year - fechaNac.Year;
                txt_edad.Text = edad.ToString();

                ddl_fajas.SelectedValue = alumnoEncontrado.id_faja.ToString();

                if (alumnoEncontrado.sexo == 0) rbSexo.SelectedIndex = 0;
                if (alumnoEncontrado.sexo == 1) rbSexo.SelectedIndex= 1;

                idAlumno = alumnoEncontrado.id_alumno;
            }

        }
    }
}