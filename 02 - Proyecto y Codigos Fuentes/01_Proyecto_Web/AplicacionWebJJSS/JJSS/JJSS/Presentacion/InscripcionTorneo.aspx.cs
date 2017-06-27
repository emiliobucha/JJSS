using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;


namespace JJSS
{
    public partial class InscripcionTorneo : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorTorneos gestorDeTorneos;
        private torneo torneoSeleccionado;
        private int? idAlumno = null;

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado != "INGRESO ACEPTADO")
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logeado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logeado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            }

            gestorInscripciones = new GestorInscripciones();
            gestorDeTorneos = new GestorTorneos();
            if (!IsPostBack)
            {
               
                CargarComboFajas();
                if (Session["torneoSeleccionado"] != null)
                {
                    int id = (int)Session["torneoSeleccionado"];
                    cargarInfoTorneo(id);
                    pnl_elegirTorneo.Visible = false;
                    pnl_InfoTorneo.Visible = true;
                    pnl_Inscripcion.Visible = false;
                    pnl_dni.Visible = true;
                }
                else
                {
                    pnl_elegirTorneo.Visible = true;
                    pnl_InfoTorneo.Visible = false;
                    pnl_Inscripcion.Visible = false;
                    pnl_dni.Visible = false;
                    CargarComboTorneos();
                }
               
                limpiar(true);
            }
            
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void limpiar(Boolean limpiaTodo)
        {
            txt_apellido.Text = "";
            txt_edad.Text = "";
            txt_nombre.Text = "";
            txt_peso.Text = "";
            //ya se que no usamos el index pero solo tiene que setearlo en el primer valor que haya en el combo
            ddl_fajas.SelectedIndex = 0;

            if (limpiaTodo == true)
            {
                txtDni.Text = "";
               // ddl_torneos.SelectedIndex = 1;
            }

        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {


            //+Ver Bien el SelectedValue del combo

            int idTorneo = 0;
            int.TryParse(ddl_torneos.SelectedValue, out idTorneo);

            //solo para invitados
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            double peso = float.Parse(txt_peso.Text);
            int edad = int.Parse(txt_edad.Text);
            int dni = int.Parse(txtDni.Text);


            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue, out idFaja);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

            //para todos
            string sReturn = gestorInscripciones.InscribirATorneo(idTorneo, nombre, apellido, peso, edad, idFaja, sexo, dni, idAlumno);
            limpiar(true);

            if (sReturn.CompareTo("") == 0) sReturn = "La inscripción se ha realizado exitosamente";
            mensaje(sReturn, "InscripcionTorneo.aspx");


        }

        protected void CargarComboTorneos()
        {
            var x = ddl_torneos.SelectedValue;
            if (ddl_torneos.DataSource == null)
            {

                List<torneo> torneos = gestorInscripciones.ObtenerTorneos();
                ddl_torneos.DataSource = torneos;
                ddl_torneos.DataTextField = "nombre";
                ddl_torneos.DataValueField = "id_torneo";
                ddl_torneos.DataBind();
            }
        }

        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajas();
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "color";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        protected void btnAceptarTorneo_Click(object sender, EventArgs e)
        {
            int idTorneo = 0;
            int.TryParse(ddl_torneos.SelectedValue, out idTorneo);
            cargarInfoTorneo(idTorneo);

        }

        protected void cargarInfoTorneo(int idTorneo)
        {
           
            if (idTorneo.CompareTo(0) > 0)
            {
                torneoSeleccionado = gestorDeTorneos.BuscarTorneoPorID(idTorneo);
                pnl_InfoTorneo.Visible = true;
                pnl_Inscripcion.Visible = true;
                btn_aceptar.Visible = true;
                lbl_NombreTorneo.Text = torneoSeleccionado.nombre;
                lbl_FechaCierreInscripcion.Text = torneoSeleccionado.fecha_cierre.Value.ToLongDateString();
                lbl_FechaDeTorneo.Text = torneoSeleccionado.fecha.Value.ToLongDateString();
                lbl_HoraTorneo.Text = torneoSeleccionado.hora.ToString();
                lbl_CostoInscripcion.Text = torneoSeleccionado.precio_categoria.ToString();
                lbl_CostoInscripcionAbsoluto.Text = torneoSeleccionado.precio_absoluto.ToString();
            }
            limpiar(true);
            pnl_dni.Visible = true;
            pnl_Inscripcion.Visible = false;
        }

        protected void btnGenerarListado_Click(object sender, EventArgs e)
        {
            int idTorneo = 0;
            int.TryParse(ddl_torneos.SelectedValue, out idTorneo);
            String sFile = gestorDeTorneos.GenerarListado(idTorneo);

            Response.Clear();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
            Response.WriteFile(sFile);


        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                limpiar(false);
                pnl_Inscripcion.Visible = true;
                participante participanteEncontrado = gestorInscripciones.ObtenerParticipanteporDNI(int.Parse(txtDni.Text));

                //Partipante ya estaba inscripto con ese dni
                if (participanteEncontrado != null)
                {
                    mensaje("Este participante ya está inscripto", "InscripcionTorneo.aspx");
                    return;
                }

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
            }
        }

        private string calcularEdad(DateTime? pFechaNacimiento)
        {
            //+habria que ver como calcularla mejor porque si estuviera calculando mi edad daria 22 pero todavia tengo 21
            DateTime fechaNac = Convert.ToDateTime(pFechaNacimiento);
            int edad = DateTime.Today.Year - fechaNac.Year;
            return edad.ToString();

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
    }
}