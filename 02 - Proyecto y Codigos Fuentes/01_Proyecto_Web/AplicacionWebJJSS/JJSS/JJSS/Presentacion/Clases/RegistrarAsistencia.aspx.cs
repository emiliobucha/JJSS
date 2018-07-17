using System;
using System.Collections.Generic;
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

                    inscripcion_clase inscripcionClase =
                        gestorInscripcionesClase.ObtenerAlumnoInscripto(alu.id_alumno, claseActual.idClase);

                    if (inscripcionClase == null)
                    {
                        mensaje("No se encuentra el alumno " + alu.apellido + ", " + alu.nombre + " inscripto a la clase " + claseActual.nombreClase, false);
                        return;
                    }

                    asistencia_clase asistenciaDeHoy = gestorAsistencia.ValidarAsistenciaClaseAnterior(alu.id_alumno, claseActual.idClase);

                    if (asistenciaDeHoy != null) mensaje("El alumno " + alu.apellido + ", " + alu.nombre + " ya asistió a la clase " + claseActual.nombreClase + " de hoy", false);


                    var hoy = DateTime.Today;

                    if (inscripcionClase.proximo_vencimiento.Value.AddDays(10) >= hoy && hoy >= inscripcionClase.fecha_desde)
                    {
                        var resultado = gestorAsistencia.registrarAsistencia(alu.id_alumno, claseActual.idClase,
                            claseActual.idHorario, DateTime.Now);
                        if (resultado == "") mensaje("Asistencia registrada exitosamente", true);
                        else mensaje(resultado, false);
                    }
                    else mensaje("El alumno tiene vencida la inscripción a la clase", false);

                }
            }
            catch (Exception)
            {
                mensaje("Ha ocurrido un error al tratar de registrar una nueva asistencia", false);
            }
       
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


        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Inicio.aspx#section_clases");
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

    }
}