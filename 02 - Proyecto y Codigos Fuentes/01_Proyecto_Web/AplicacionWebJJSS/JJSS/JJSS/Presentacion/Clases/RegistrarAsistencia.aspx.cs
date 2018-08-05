using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Administracion;

namespace JJSS.Presentacion
{
    public partial class RegistrarAsistencia : System.Web.UI.Page
    {
        private GestorAcademias gestorAcademias;
        private GestorAsistencia gestorAsistencia;
        private GestorAlumnos gestorAlumno;
        private GestorInscripcionesClase gestorInscripcionesClase;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_ASISTENCIA'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o no tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {

                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                }

                CargarComboTipoDocumentos();
                cargarComboUbicacion();
            }

        }

        protected void cargarComboUbicacion()
        {
            gestorAcademias = new GestorAcademias();
            List<academia> academias = gestorAcademias.ObtenerAcademias();
            academia primerElemento = new academia();
            primerElemento.id_academia = 0;
            primerElemento.nombre = "Seleccione una ubicación";
            academias.Insert(0, primerElemento);

            ddl_ubicacion.DataSource = academias;
            ddl_ubicacion.DataTextField = "nombre";
            ddl_ubicacion.DataValueField = "id_academia";
            ddl_ubicacion.DataBind();


        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_mensaje_exito.Visible = false;
                pnl_mensaje_error.Visible = false;

                gestorAsistencia = new GestorAsistencia();
                gestorAlumno = new GestorAlumnos();
                gestorInscripcionesClase = new GestorInscripcionesClase();
                int ubicacion = int.Parse(ddl_ubicacion.SelectedValue);

                ClasesHorariosAsistencia claseActual = gestorAsistencia.buscarClaseSegunHoraActual(ubicacion);

                if (claseActual == null) mensaje("No hay clases disponibles en este horario", false);
                else
                {

                    var dni = txtDni.Text;

                    int idTipo;
                    int.TryParse(ddl_tipo.SelectedValue, out idTipo);

                    if (!modValidaciones.validarFormatoDocumento(dni, idTipo))
                    {
                        mensaje("El documento debe tener sólo números", false);
                        return;
                    }

                    alumno alu = gestorAlumno.ObtenerAlumnoPorDNITipo(idTipo, dni);


                    if (alu == null)
                    {
                        mensaje("No se encuentra ningún alumno registrado con ese tipo y número de documento", false);
                        return;
                    }

                    //ACA!

                    List<inscripcion_clase> inscripcionClase =
                        gestorInscripcionesClase.ObtenerAlumnoInscriptoList(alu.id_alumno, claseActual.idClase);

                    if (inscripcionClase == null || inscripcionClase.Count == 0)
                    {
                        mensaje("No se encuentra el alumno " + alu.apellido + ", " + alu.nombre + " inscripto a la clase " + claseActual.nombreClase, false);
                        return;
                    }

                    cargarDatos(claseActual,alu);

                }
            }
            catch (Exception ex)
            {
                mensaje("Ha ocurrido un error al tratar de buscar los datos del alumno. Error: " + ex.Message, false);
            }

        }

        private void cargarDatos(ClasesHorariosAsistencia claseActual, alumno alumnoIngresado)
        {
            divConfirmacion.Visible = true;
            divIngreso.Visible = false;

            lblAcademia.InnerText = ddl_ubicacion.SelectedItem.Text;
            lblClase.InnerText = claseActual.nombreClase;
            lblDni.InnerText = alumnoIngresado.dni;
            lblNombre.InnerText = alumnoIngresado.nombre + " " + alumnoIngresado.apellido;
            lblTipoDoc.InnerText = ddl_tipo.SelectedItem.Text;
            

        }

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


        protected void cstm_ubicacion_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                int ubicacion = int.Parse(ddl_ubicacion.SelectedValue);
                if (ubicacion == 0) args.IsValid = false;
                else args.IsValid = true;
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void CargarComboTipoDocumentos()
        {
            gestorAlumno = new GestorAlumnos();
            List<tipo_documento> tiposdoc = gestorAlumno.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }

        protected void btnSI_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            try
            {
                gestorAsistencia = new GestorAsistencia();
                gestorAlumno = new GestorAlumnos();
                gestorInscripcionesClase = new GestorInscripcionesClase();
                int ubicacion = int.Parse(ddl_ubicacion.SelectedValue);

                ClasesHorariosAsistencia claseActual = gestorAsistencia.buscarClaseSegunHoraActual(ubicacion);

                if (claseActual == null) mensaje("No hay clases disponibles en este horario", false);
                else
                {

                    var dni = txtDni.Text;

                    int idTipo;
                    int.TryParse(ddl_tipo.SelectedValue, out idTipo);

                    if (!modValidaciones.validarFormatoDocumento(dni, idTipo))
                    {
                        mensaje("El documento debe tener sólo números", false);
                        return;
                    }

                    alumno alu = gestorAlumno.ObtenerAlumnoPorDNITipo(idTipo, dni);


                    if (alu == null)
                    {
                        mensaje("No se encuentra ningún alumno registrado con ese tipo y número de documento", false);
                        return;
                    }

                    //ACA!

                    List<inscripcion_clase> inscripcionClase =
                        gestorInscripcionesClase.ObtenerAlumnoInscriptoList(alu.id_alumno, claseActual.idClase);

                    if (inscripcionClase == null || inscripcionClase.Count == 0)
                    {
                        mensaje("No se encuentra el alumno " + alu.apellido + ", " + alu.nombre + " inscripto a la clase " + claseActual.nombreClase, false);
                        return;
                    }


                    var hoy = DateTime.Now;

                    //Filtro las inscripciones por las que estan dentro del periodo y son activas o las que te permiten pasar como moroso

                    var inscripcion = inscripcionClase.Where(x => (
                        x.fecha_vencimiento.Value.Date >= hoy && hoy >= x.fecha_desde.Value.Date && x.provisoria == 0 && x.recargo == 0)
                        || (x.fecha_vencimiento.Value.Date >= hoy && hoy >= x.fecha_desde.Value.Date && x.provisoria == 1 && x.recargo == 1 && x.moroso_si == 1)).OrderBy(x => x.fecha_vencimiento);

                    if (!inscripcion.Any())
                    {
                        mensaje("El alumno tiene vencida la inscripción a la clase", false);
                        return;
                    }

                    var inscripcionARegistrar = inscripcion.FirstOrDefault();

                    asistencia_clase asistenciaDeHoy = gestorAsistencia.ValidarAsistenciaAnteriorInscripcionClase(inscripcionARegistrar.id_inscripcion);

                    if (asistenciaDeHoy != null)
                    {
                        mensaje("El alumno " + alu.apellido + ", " + alu.nombre + " ya asistió a la clase " + claseActual.nombreClase + " de hoy", false);
                        return;
                    }

                    var resultado = gestorAsistencia.registrarAsistencia(alu.id_alumno, claseActual.idClase,
                        claseActual.idHorario, DateTime.Now, inscripcionARegistrar.id_inscripcion);
                    if (resultado == "")
                    {
                        mensaje("Asistencia registrada exitosamente", true);
                        divConfirmacion.Visible = false;
                        divIngreso.Visible = true;
                        return;
                    }

                    mensaje(resultado, false);
                    
                }
            }
            catch (Exception ex)
            {
                mensaje("Ha ocurrido un error al tratar de registrar una nueva asistencia. Error: " + ex.Message, false);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            divConfirmacion.Visible = false;
            divIngreso.Visible = true;

            lblAcademia.InnerText = "";
            lblClase.InnerText = "";
            lblDni.InnerText = "";
            lblNombre.InnerText = "";
            lblTipoDoc.InnerText = "";

            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }
    }
}