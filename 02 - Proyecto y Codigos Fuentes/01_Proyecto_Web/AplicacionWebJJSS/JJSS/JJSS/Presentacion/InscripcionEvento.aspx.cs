using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Globalization;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Administracion;

namespace JJSS.Presentacion
{
    public partial class InscripcionEvento : System.Web.UI.Page
    {
        private GestorInscripcionesEvento gestorInscripciones;
        private GestorEventos gestorEvento;
        private evento_especial eventoSeleccionado;
        private GestorAlumnos gestorAlumnos;
        private int? idAlumno = null;
        private seguridad_usuario usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorInscripciones = new GestorInscripcionesEvento();
            gestorAlumnos = new GestorAlumnos();
            gestorEvento = new GestorEventos();

            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        usuario = sesionActiva.usuario;
                        alumno alumno = gestorAlumnos.ObtenerAlumnoPorIdUsuario(usuario.id_usuario);
                        txtDni.Text = alumno.dni.ToString();
                        limpiar(false);
                    }
                    else
                    {
                        limpiar(true);
                    }
                }
                catch
                {
                    limpiar(true);
                }

                if (Session["eventoSeleccionado"] != null)
                {
                    int id = (int)Session["eventoSeleccionado"];
                    cargarInfoEvento(id);
                    pnl_elegirEvento.Visible = false;
                    pnl_InfoTorneo.Visible = true;
                    pnl_Inscripcion.Visible = false;
                    pnl_dni.Visible = true;
                }
                else
                {
                    pnl_elegirEvento.Visible = true;
                    pnl_InfoTorneo.Visible = false;
                    pnl_Inscripcion.Visible = false;
                    pnl_dni.Visible = false;
                    CargarComboEventos();
                }


            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void limpiar(Boolean limpiaTodo)
        {
            txt_apellido.Text = "";
            dp_fecha.Text = "";
            txt_nombre.Text = "";
           
            txt_apellido.ReadOnly = false;
            txt_nombre.ReadOnly = false;
            dp_fecha.ReadOnly = false;
            dp_fecha.Enabled = true;
            rbSexo.Enabled = true;

            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            if (limpiaTodo == true)
            {
                txtDni.Text = "";
                // ddl_Eventos.SelectedIndex = 1;
            }

        }

        protected void btnAceptarEvento_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
          

            int idEvento = 0;
            int.TryParse(ddl_evento.SelectedValue, out idEvento);
            cargarInfoEvento(idEvento);
        }

        protected void cargarInfoEvento(int idEvento)
        {

            if (idEvento > 0)
            {

                eventoSeleccionado = gestorEvento.BuscarEventoPorID(idEvento);
                pnl_InfoTorneo.Visible = true;
                pnl_Inscripcion.Visible = true;
                btn_aceptar.Visible = true;
                GestorSedes gestorSede = new GestorSedes();
                SedeDireccion sede = gestorSede.ObtenerDireccionSede((int)eventoSeleccionado.id_sede);


                lbl_nombreEvento.Text = eventoSeleccionado.nombre;
                lbl_FechaCierreInscripcion.Text = eventoSeleccionado.fecha_cierre.Value.ToLongDateString();
                lbl_FechaDeEvento.Text = eventoSeleccionado.fecha.Value.ToLongDateString();
                lbl_HoraEvento.Text = eventoSeleccionado.hora.ToString();
                lbl_CostoInscripcion.Text = eventoSeleccionado.precio.ToString();
                lbl_HoraCierre.Text = eventoSeleccionado.hora_cierre.ToString();
                lbl_sede.Text = sede.sede;
                lbl_direccion_sede.Text = sede.calle + " " + sede.numero + " - " + sede.ciudad + " - " + sede.provincia + " - " + sede.pais;
            }

            pnl_dni.Visible = true;
            pnl_Inscripcion.Visible = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            limpiar(false);
            pnl_Inscripcion.Visible = true;
            int idEvento;
            if (Session["eventoSeleccionado"] != null)
            {
                idEvento = (int)Session["eventoSeleccionado"];

            }
            else
            {
                idEvento = int.Parse(ddl_evento.SelectedValue);

            }



            participante_evento participanteEncontrado = gestorInscripciones.obtenerParticipanteEvento(int.Parse(txtDni.Text), idEvento);

            //Partipante ya estaba inscripto con ese dni
            if (participanteEncontrado != null)
            {
                mensaje("Este participante ya está inscripto a este evento", false);
                return;
            }

            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(txtDni.Text);
            if (alumnoEncontrado != null)
            {
                //Completa los campos con los datos del alumno, asi luego cuando se va a inscribir, al participante ya le manda los datos y no hay que modificar el metodo de carga de participantes
                txt_apellido.ReadOnly = true;
                txt_nombre.ReadOnly = true;
                dp_fecha.ReadOnly = true;
                dp_fecha.Enabled = false;

                rbSexo.Enabled = false;

                txt_apellido.Text = alumnoEncontrado.apellido;
                txt_nombre.Text = alumnoEncontrado.nombre;

                DateTime fecha = (DateTime)alumnoEncontrado.fecha_nacimiento;
                /*FECHA SOMEE
                string format = "MM/dd/yyyy";
                dp_fecha.Text = fecha.ToString(format, new CultureInfo("en-US"));
                */
                //LOCAL
                dp_fecha.Text = fecha.ToShortDateString();
                
                if (alumnoEncontrado.sexo == 0) rbSexo.SelectedIndex = 0;
                if (alumnoEncontrado.sexo == 1) rbSexo.SelectedIndex = 1;

                idAlumno = alumnoEncontrado.id_alumno;
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

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            int idEvento;
            if (Session["eventoSeleccionado"] != null)
            {
                idEvento = (int)Session["eventoSeleccionado"];

            }
            else
            {
                idEvento = int.Parse(ddl_evento.SelectedValue);

            }



            //solo para invitados
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;

            /*FECHA SOMEE
            string[] formats = { "MM/dd/yyyy" };
            DateTime fechaNac = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
            */
            //LOCAL
            DateTime fechaNac = DateTime.Parse(dp_fecha.Text);

            int dni = int.Parse(txtDni.Text);
            
            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = JJSS_Negocio.Constantes.ContantesSexo.FEMENINO;
            if (rbSexo.SelectedIndex == 1) sexo = JJSS_Negocio.Constantes.ContantesSexo.MASCULINO;

            //para alumnos
            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(txtDni.Text);
            if (alumnoEncontrado != null) idAlumno = alumnoEncontrado.id_alumno;

          


            //para todos
            string sReturn = gestorInscripciones.InscribirAEvento(idEvento, nombre, apellido, fechaNac.Date, sexo, dni, idAlumno);


            if (sReturn.CompareTo("") == 0)
            {
                mensaje("La inscripción se ha realizado exitosamente", true);
                pnl_pago.Visible = true;
                Session["EventoPagar"] = idEvento;
                Session["ParticipanteDNI"] = dni;


                //para usuarios
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];

                
                try
                {
                    string mail = sesionActiva.usuario.mail;
                    string sFile = gestorInscripciones.ComprobanteInscripcion(gestorInscripciones.obtenerInscripcionAEventoPorIdParticipantePorDni(dni, idEvento).id_inscripcion, mail);

                    pnl_comprobante.Visible = true;
                    btn_descargar.Attributes.Add("href", "Downloader.ashx?" + "sFile=" + sFile);
                }
                catch (Exception ex)
                {
                    mensaje("Usted se ha inscripto exitosamente pero no se le pudo generar el comprobante.Puede fijarse en sus incripciones si es necesario", true);
                    
                }


            }
            else mensaje(sReturn, false);
        }

        protected void CargarComboEventos()
        {
            var x = ddl_evento.SelectedValue;
            if (ddl_evento.DataSource == null)
            {

                List<evento_especial> eventos = gestorInscripciones.ObtenerEventos();
                ddl_evento.DataSource = eventos;
                ddl_evento.DataTextField = "nombre";
                ddl_evento.DataValueField = "id_evento";
                ddl_evento.DataBind();
            }
        }

        protected void rbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void val_sexo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (rbSexo.SelectedIndex == 0 || rbSexo.SelectedIndex == 1)
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

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar(true);
            Response.Redirect("../Presentacion/Inicio.aspx#section_eventos");
        }
    }
}