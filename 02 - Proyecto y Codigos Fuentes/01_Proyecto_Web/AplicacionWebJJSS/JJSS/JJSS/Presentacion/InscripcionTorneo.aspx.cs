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

namespace JJSS
{
    public partial class InscripcionTorneo : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorTorneos gestorDeTorneos;
        private torneo torneoSeleccionado;
        private GestorAlumnos gestorAlumnos;
        private int? idAlumno = null;
        private seguridad_usuario usuario;

        protected void Page_Load(object sender, EventArgs e)
        {

            gestorInscripciones = new GestorInscripciones();
            gestorDeTorneos = new GestorTorneos();
            gestorAlumnos = new GestorAlumnos();


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



                if (Session["torneoSeleccionado"] != null)
                {
                    int id = (int)Session["torneoSeleccionado"];

                    cargarInfoTorneo(id);
                    pnl_elegirTorneo.Visible = false;
                    pnl_InfoTorneo.Visible = true;
                    pnl_Inscripcion.Visible = false;
                    pnl_dni.Visible = true;
                    torneo t = gestorDeTorneos.BuscarTorneoPorID(id);
                    CargarComboFajas((int)t.id_tipo_clase);
                }
                else
                {
                    pnl_elegirTorneo.Visible = true;
                    pnl_InfoTorneo.Visible = false;
                    pnl_Inscripcion.Visible = false;
                    pnl_dni.Visible = false;
                    CargarComboTorneos();
                }


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
            dp_fecha.Text = "";
            txt_nombre.Text = "";
            txt_peso.Text = "";

            //ya se que no usamos el index pero solo tiene que setearlo en el primer valor que haya en el combo
            if (ddl_fajas.Items.Count > 0) ddl_fajas.SelectedIndex = 0;


            txt_apellido.ReadOnly = false;
            txt_nombre.ReadOnly = false;
            dp_fecha.ReadOnly = false;
            dp_fecha.Enabled = true;
            ddl_fajas.Enabled = true;
            rbSexo.Enabled = true;


            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            if (limpiaTodo == true)
            {
                txtDni.Text = "";
                // ddl_torneos.SelectedIndex = 1;
            }

        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {

            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            int idTorneo;
            if (Session["torneoSeleccionado"] != null)
            {
                idTorneo = (int)Session["torneoSeleccionado"];

            }
            else
            {
                idTorneo = int.Parse(ddl_torneos.SelectedValue);

            }


            //solo para invitados
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            double peso = float.Parse(txt_peso.Text.Replace(".", ","));
            string[] formats = { "MM/dd/yyyy" };

            DateTime fechaNac = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"),
                System.Globalization.DateTimeStyles.None);
            int dni = int.Parse(txtDni.Text);


            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue, out idFaja);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

            //para alumnos
            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(int.Parse(txtDni.Text));
            if (alumnoEncontrado != null) idAlumno = alumnoEncontrado.id_alumno;

            //para todos

            short tipoInsc = 0;
            if (rb_tipo.SelectedIndex == 0) tipoInsc = 0;//categoria
            if (rb_tipo.SelectedIndex == 1) tipoInsc = 1;//absoluto
            string sReturn = gestorInscripciones.InscribirATorneo(idTorneo, nombre, apellido, peso, fechaNac.Date,
                idFaja, sexo, dni, idAlumno, tipoInsc);



            if (sReturn.CompareTo("") == 0)
            {
                mensaje("La inscripción se ha realizado exitosamente", true);
                pnl_pago.Visible = true;
                Session["TorneoPagar"] = idTorneo;
                Session["ParticipanteDNI"] = dni;




                try
                {

                    string sFile;
                    string mail = null;
                    if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                    {

                    }
                    else
                    {

                        ////para usuarios
                        Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                        mail = sesionActiva.usuario.mail;

                    }
                    sFile = gestorInscripciones.ComprobanteInscripcion(
                        gestorInscripciones.obtenerInscripcionAEventoPorIdParticipantePorDni(dni, idTorneo).id_inscripcion, mail);
                    pnl_comprobante.Visible = true;
                    btn_descargar.Attributes.Add("href", "Downloader.ashx?" + "sFile=" + sFile);
                }
                catch (Exception ex)
                {
                    //Response.Write("<script>window.alert('" + "Usted se ha inscripto exitosamente pero no se le pudo generar el comprobante. Puede fijarse en sus incripciones si es necesario".Trim() + "');</script>");
                    mensaje("Usted se ha inscripto exitosamente pero no se le pudo generar el comprobante. Puede fijarse en sus incripciones si es necesario", true);
                }





            }
            else mensaje(sReturn, false);



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

            if (Session["torneoSeleccionado"] != null)
            {
                int id = (int)Session["torneoSeleccionado"];
                ddl_torneos.SelectedValue = id.ToString();
            }
        }

        protected void CargarComboFajas(int pIdTipoClase)
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajasPorTipoClase(pIdTipoClase);
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "descripcion";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        protected void btnAceptarTorneo_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;


            int idTorneo = 0;
            int.TryParse(ddl_torneos.SelectedValue, out idTorneo);
            cargarInfoTorneo(idTorneo);
            int idTipoClase = (int)gestorDeTorneos.BuscarTorneoPorID(idTorneo).id_tipo_clase;
            CargarComboFajas(idTipoClase);

        }

        protected void cargarInfoTorneo(int idTorneo)
        {

            if (idTorneo.CompareTo(0) > 0)
            {
                int idTipoClase = (int)gestorDeTorneos.BuscarTorneoPorID(idTorneo).id_tipo_clase;
                CargarComboFajas(idTipoClase);
                torneoSeleccionado = gestorDeTorneos.BuscarTorneoPorID(idTorneo);
                GestorSedes gestorSede = new GestorSedes();
                SedeDireccion sede = gestorSede.ObtenerDireccionSede((int)torneoSeleccionado.id_sede);


                pnl_InfoTorneo.Visible = true;
                pnl_Inscripcion.Visible = true;
                btn_aceptar.Visible = true;
                lbl_NombreTorneo.Text = torneoSeleccionado.nombre;
                lbl_FechaCierreInscripcion.Text = torneoSeleccionado.fecha_cierre.Value.ToLongDateString();
                lbl_FechaDeTorneo.Text = torneoSeleccionado.fecha.Value.ToLongDateString();
                lbl_HoraTorneo.Text = torneoSeleccionado.hora.ToString();
                lbl_CostoInscripcion.Text = torneoSeleccionado.precio_categoria.ToString();
                lbl_CostoInscripcionAbsoluto.Text = torneoSeleccionado.precio_absoluto.ToString();
                lbl_HoraCierreTorneo.Text = torneoSeleccionado.hora_cierre.ToString();
                lbl_sede.Text = sede.sede;
                lbl_direccion_sede.Text = sede.calle + " " + sede.numero + " - " + sede.ciudad + " - " + sede.provincia + " - " + sede.pais;
            }

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
            Response.AddHeader("Content-Disposition",
                "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
            Response.WriteFile(sFile);


        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            //Page.Validate();
            //if (Page.IsValid)
            //{
            limpiar(false);

            int idTorneo;
            if (Session["torneoSeleccionado"] != null)
            {
                idTorneo = (int)Session["torneoSeleccionado"];

            }
            else
            {
                idTorneo = int.Parse(ddl_torneos.SelectedValue);

            }



            participante participanteEncontrado =
                gestorInscripciones.obtenerParticipanteDeTorneo(int.Parse(txtDni.Text), idTorneo);

            //Partipante ya estaba inscripto con ese dni
            if (participanteEncontrado != null)
            {
                mensaje("Este participante ya está inscripto a este torneo", false);
                return;
            }
            else pnl_Inscripcion.Visible = true;

            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(int.Parse(txtDni.Text));
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
                string format = "MM/dd/yyyy";
                dp_fecha.Text = fecha.ToString(format, new CultureInfo("en-US"));


                // txt_edad.Text = calcularEdad(alumnoEncontrado.fecha_nacimiento);


                int idTipoClase = (int)gestorDeTorneos.BuscarTorneoPorID(idTorneo).id_tipo_clase;
                faja fajaAlumno = gestorAlumnos.ObtenerFajaAlumno(alumnoEncontrado.id_alumno, idTipoClase);
                if (fajaAlumno != null)
                {
                    ddl_fajas.Enabled = false;
                    ddl_fajas.SelectedValue = fajaAlumno.id_faja.ToString();
                }


                if (alumnoEncontrado.sexo == 0) rbSexo.SelectedIndex = 0;
                if (alumnoEncontrado.sexo == 1) rbSexo.SelectedIndex = 1;

                idAlumno = alumnoEncontrado.id_alumno;
            }
        }
        //}

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

        protected void val_sexo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (rbSexo.SelectedIndex == 0 && rbSexo.SelectedIndex == 1)
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

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            limpiar(true);
            Response.Redirect("../Presentacion/Inicio.aspx");
        }
    }
}