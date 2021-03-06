﻿using System;
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
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Resultados.Pagos;
using Newtonsoft.Json;

namespace JJSS
{
    public partial class InscripcionTorneo : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorTorneos gestorDeTorneos;
        private torneo torneoSeleccionado;
        private GestorAlumnos gestorAlumnos;
        private GestorAdministradores gestorAdministradores;
        private GestorProfesores gestorProfesores;
        private int? idAlumno = null;
        private seguridad_usuario usuario;
        private static int idTorneoSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorInscripciones = new GestorInscripciones();
            gestorDeTorneos = new GestorTorneos();
            gestorAlumnos = new GestorAlumnos();


            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/Torneos/MenuTorneo.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();


                try
                {

                    CargarComboTipoDocumentos();
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        usuario = sesionActiva.usuario;


                        try
                        {
                            alumno alumno = gestorAlumnos.ObtenerAlumnoPorIdUsuario(usuario.id_usuario);
                            if (alumno != null)
                            {
                                txtDni.Text = alumno.dni;
                                ddl_tipo.SelectedValue = alumno.id_tipo_documento.ToString();
                                limpiar(false);
                            }
                            else
                            {
                                limpiar(true);
                            }
                            //Por si hace falta que se inscriba otro que no sea alumno ESTA CONTEMPLADO SOLO ALUMNO
                            /*else
                            {
                                profesor profesor = gestorProfesores.ObtenerProfesorPorIdUsuario(usuario.id_usuario);
                                if (profesor != null)
                                {
                                    txtDni.Text = profesor.dni;
                                    ddl_tipo.SelectedValue = profesor.id_tipo_documento.ToString();
                                    limpiar(false);
                                }
                                else
                                {
                                    administrador admin = gestorAdministradores.ObtenerAdminPorIdUsuario(usuario.id_usuario);
                                   
                                    if (admin != null)
                                    {
                                        txtDni.Text = admin.dni;
                                        ddl_tipo.SelectedValue = admin.id_tipo_documento.ToString();
                                        limpiar(false);
                                    }
                                }
                            }*/


                        }
                        catch (Exception)
                        {
                            limpiar(true);
                        }

                    }
                    else
                    {
                        limpiar(true);
                    }
                }
                catch
                {
                    if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                    {
                        btn_aceptar.Text = "Pagar";
                        limpiar(true);
                    }

                }



                if (Session["idTorneo_inscribirTorneo"] != null)
                {
                    idTorneoSeleccionado = (int)Session["idTorneo_inscribirTorneo"];
                    Session["idTorneo_inscribirTorneo"] = null;
                    cargarInfoTorneo(idTorneoSeleccionado);
                    pnl_elegirTorneo.Visible = false;
                    pnl_InfoTorneo.Visible = true;
                    pnl_Inscripcion.Visible = false;
                    pnl_dni.Visible = true;
                    torneo t = gestorDeTorneos.BuscarTorneoPorID(idTorneoSeleccionado);
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
                CargarComboNacionalidades();


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
            ddl_nacionalidad.Enabled = true;

            lnk_pagos_panel.Visible = false;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            if (limpiaTodo)
            {
                txtDni.Text = "";
                ddl_nacionalidad.SelectedIndex = 0;
                ddl_tipo.SelectedIndex = 0;
                // ddl_torneos.SelectedIndex = 1;
            }

        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            Page.Validate("vgDatos");
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            int idTorneoCombo = 0;
            if (idTorneoSeleccionado == 0)
            {
                idTorneoCombo = int.Parse(ddl_torneos.SelectedValue);
            }
            else
            {
                idTorneoCombo = idTorneoSeleccionado;
            }



            //solo para invitados
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            double peso = float.Parse(txt_peso.Text.Replace(".", ","));

            /*FECHA SOMEE
            string[] formats = { "MM/dd/yyyy" };
            DateTime fechaNac = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
            */
            //LOCAL
            DateTime fechaNac = DateTime.Parse(dp_fecha.Text);

            var dni = txtDni.Text;

            int idTipo;
            int.TryParse(ddl_tipo.SelectedValue, out idTipo);

            if (!modValidaciones.validarFormatoDocumento(dni, idTipo))
            {
                Mensaje("El documento debe tener sólo números", false);
                return;
            }

            int idPais;
            int.TryParse(ddl_nacionalidad.SelectedValue, out idPais);


            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue, out idFaja);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = JJSS_Negocio.Constantes.ContantesSexo.FEMENINO;
            if (rbSexo.SelectedIndex == 1) sexo = JJSS_Negocio.Constantes.ContantesSexo.MASCULINO;

            //para alumnos
            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNITipo(idTipo, txtDni.Text);
            if (alumnoEncontrado != null) idAlumno = alumnoEncontrado.id_alumno;

            //para todos

            short tipoInsc = 0;
            if (rb_tipo.SelectedIndex == 0) tipoInsc = 0;//categoria
            if (rb_tipo.SelectedIndex == 1) tipoInsc = 1;//absoluto
            string sReturn = gestorInscripciones.InscribirATorneo(idTorneoCombo, nombre, apellido, peso, fechaNac.Date,
                idFaja, sexo, idTipo, dni, idAlumno, tipoInsc, idPais);

            if (string.IsNullOrEmpty(sReturn))
            {
                try
                {
                    string sFile;
                    string mail = null;
                    if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                    {
                        limpiar(true);

                        var gestorPagos = new GestorPagos();
                        var ultimo = gestorPagos.ObtenerUltimoTorneoObjetoPagableInvitado(idTipo, dni);

                        var gestorUsuarios = new GestorUsuarios();
                        var idUsuario = gestorUsuarios.ObtenerIdUsuarioInvitado();
                        if (idUsuario == -1)
                        {
                            Mensaje("Error, usuario invitado no existe y no se puede registrar pago", false);
                            return;
                        }
                        var pagoMultiple = new PagoMultiple(ultimo, ConstantesFormaPago.MERCADOPAGO, idUsuario, idTipo, dni, nombre+ " " + apellido);
                        HttpContext.Current.Session["PagoMultiple"] = JsonConvert.SerializeObject(pagoMultiple);

                        Response.Redirect("~/Presentacion/Pagos/PagosMultiple.aspx");

                    }
                    else
                    {

                        ////para usuarios
                        Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                        mail = sesionActiva.usuario.mail;
                        limpiar(true);
                        Mensaje("La inscripción se ha realizado exitosamente. Para pagar diríjase a ", true);
                        lnk_pagos_panel.Visible = true;

                    }
                    sFile = gestorInscripciones.ComprobanteInscripcion(
                        gestorInscripciones.obtenerInscripcionATorneoPorIdParticipantePorDni(idTipo, dni, idTorneoCombo).id_inscripcion, mail);
                    pnl_comprobante.Visible = true;
                    btn_descargar.Attributes.Add("href", "../Downloader.ashx?" + "sFile=" + sFile);
                }
                catch (Exception ex)
                {
                    //Response.Write("<script>window.alert('" + "Usted se ha inscripto exitosamente pero no se le pudo generar el comprobante. Puede fijarse en sus incripciones si es necesario".Trim() + "');</script>");
                    Mensaje("Usted se ha inscripto exitosamente pero no se le pudo generar el comprobante. Puede fijarse en sus incripciones si es necesario", true);
                }
            }
            else Mensaje(sReturn, false);

        }

        protected void CargarComboTorneos()
        {
            var x = ddl_torneos.SelectedValue;
            if (ddl_torneos.DataSource == null)
            {

                List<torneo> torneos = gestorInscripciones.ObtenerTorneos();
                if (torneos.Count == 0) Mensaje("No hay torneos disponibles", false);
                ddl_torneos.DataSource = torneos;
                ddl_torneos.DataTextField = "nombre";
                ddl_torneos.DataValueField = "id_torneo";
                ddl_torneos.DataBind();
            }

            if (idTorneoSeleccionado != 0)
            {
                ddl_torneos.SelectedValue = idTorneoSeleccionado.ToString();
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

        protected void CargarComboTipoDocumentos()
        {

            List<tipo_documento> tiposdoc = gestorInscripciones.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }

        protected void CargarComboNacionalidades()
        {

            List<pais> paises = gestorInscripciones.ObtenerNacionalidades();
            ddl_nacionalidad.DataSource = paises;
            ddl_nacionalidad.DataTextField = "nombre";
            ddl_nacionalidad.DataValueField = "id_pais";
            ddl_nacionalidad.DataBind();

        }


        protected void btnAceptarTorneo_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;


            int idTorneoCombo = 0;
            int.TryParse(ddl_torneos.SelectedValue, out idTorneoCombo);
            cargarInfoTorneo(idTorneoCombo);
            idTorneoSeleccionado = idTorneoCombo;

            int idTipoClase = (int)gestorDeTorneos.BuscarTorneoPorID(idTorneoCombo).id_tipo_clase;
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
                lbl_direccion_sede.Text = sede.calle + " " + sede.numero + " - B° " + sede.barrio + " - " + sede.ciudad + " - " + sede.provincia + " - " + sede.pais;
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
            limpiar(false);
            
            int idTipo;
            int.TryParse(ddl_tipo.SelectedValue, out idTipo);

            if (!modValidaciones.validarFormatoDocumento(txtDni.Text, idTipo))
            {
                Mensaje("El documento debe tener sólo números", false);
                return;
            }

            participante participanteEncontrado =
                gestorInscripciones.obtenerParticipanteDeTorneo(idTipo, txtDni.Text, idTorneoSeleccionado);

            //Partipante ya estaba inscripto con ese dni
            if (participanteEncontrado != null)
            {
                Mensaje("Este participante ya está inscripto a este torneo", false);
                return;
            }

            pnl_Inscripcion.Visible = true;




            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNITipo(idTipo, txtDni.Text);
            if (alumnoEncontrado != null)
            {
                //Completa los campos con los datos del alumno, asi luego cuando se va a inscribir, al participante ya le manda los datos y no hay que modificar el metodo de carga de participantes


                txt_apellido.ReadOnly = true;
                txt_nombre.ReadOnly = true;


                dp_fecha.Enabled = false;
                // dp_fecha.Attributes.Add("disabled", "true");

                ddl_nacionalidad.Enabled = false;

                //  ddl_nacionalidad.Attributes.Add("disabled", "true");

                rbSexo.Enabled = false;

                // rbSexo.Attributes.Add("disabled", "true");


                txt_apellido.Text = alumnoEncontrado.apellido;
                txt_nombre.Text = alumnoEncontrado.nombre;

                ddl_nacionalidad.SelectedValue = alumnoEncontrado.id_pais != null
                    ? alumnoEncontrado.id_pais.ToString()
                    : "";

                DateTime fecha = (DateTime)alumnoEncontrado.fecha_nacimiento;
                /*FECHA SOMEE
                string format = "MM/dd/yyyy";
                dp_fecha.Text = fecha.ToString(format, new CultureInfo("en-US"));
                */
                //LOCAL
                dp_fecha.Text = fecha.ToString("dd/MM/yyyy");

                // txt_edad.Text = calcularEdad(alumnoEncontrado.fecha_nacimiento);


                int idTipoClase = (int)gestorDeTorneos.BuscarTorneoPorID(idTorneoSeleccionado).id_tipo_clase;
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
            else
            {

                participante partAnterior = gestorInscripciones.ObtenerParticipanteporDNITipo(idTipo, txtDni.Text);

                if (partAnterior != null)
                {
                    //Completa los campos con los datos del alumno, asi luego cuando se va a inscribir, al participante ya le manda los datos y no hay que modificar el metodo de carga de participantes



                    txt_apellido.Text = partAnterior.apellido;
                    txt_nombre.Text = partAnterior.nombre;


                    ddl_nacionalidad.SelectedValue = partAnterior.id_pais != null
                        ? partAnterior.id_pais.ToString()
                        : "";

                    DateTime fecha = (DateTime)partAnterior.fecha_nacimiento;

                    dp_fecha.Text = fecha.ToShortDateString();

                    if (partAnterior.sexo == 0) rbSexo.SelectedIndex = 0;
                    if (partAnterior.sexo == 1) rbSexo.SelectedIndex = 1;


                }
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
        private void Mensaje(string pMensaje, bool pEstado)
        {
            if (pEstado)
            {
                //Session["mensaje"] = pMensaje;
                //Session["exito"] = pEstado;
                //Response.Redirect("MenuTorneo.aspx");
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
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
    }
}