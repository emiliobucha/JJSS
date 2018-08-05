using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Presentacion.Pagos
{
    public partial class PagosCambioVto : System.Web.UI.Page
    {

        private static GestorVencimientos gestorVencimientos;
        private static GestorAlumnos gestorAlumnos;
        private static GestorClases gestorClases;
        private static GestorInscripcionesClase gestorInscripcionesClase;
        private static int idAlumno;
        private static int idClase;
        private static int idInscripcion;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorVencimientos = new GestorVencimientos();
            gestorAlumnos = new GestorAlumnos();
            gestorClases = new GestorClases();
            gestorInscripcionesClase = new GestorInscripcionesClase();


            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'PAGO_VENCIMIENTO'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                }


                if (Session["idInscripcion"]!=null)
                {
                    idInscripcion = int.Parse(Session["idInscripcion"].ToString());
                    var inscripcion = gestorInscripcionesClase.ObtenerInscripcionClasePorId(idInscripcion);

                    if (inscripcion?.id_alumno == null || inscripcion?.id_clase == null)
                    {
                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
                        return;
                    }

                    idAlumno = inscripcion.id_alumno.Value;
                    idClase = inscripcion.id_clase.Value;
                    CargarDatosAlumno();
                    CargarDatosClase();

                }
                else
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
                }
            }



        }


        private void CargarDatosAlumno()
        {
            var alumno = gestorAlumnos.ObtenerAlumnoResultadoId(idAlumno);
            lbl_nombre_alumno.Text ="Nombre: " +  alumno.apellido + ", " + alumno.nombre;
            lbl_tipo_dni.Text = "Tipo: " + alumno.tipo_documento;
            lbl_dni.Text = "Número: " + alumno.dni;
        }

        private void CargarDatosClase()
        {
            var clase = gestorClases.ObtenerClasePorId(idClase);
            lbl_clase_nombre.Text = "Clase: " + clase.nombre;
            var inscripcion = gestorInscripcionesClase.ObtenerInscripcionClasePorId(idInscripcion);


            dp_fecha.Text = inscripcion.fecha_desde.Value.ToString("dd/MM/yyyy");


        }



        protected void btn_guardar_OnClick(object sender, EventArgs e)
        {
            try
            {

                DateTime fecha = DateTime.Parse(dp_fecha.Text);

                gestorVencimientos.ActualizarPeriodo(idInscripcion, fecha);
                Mensaje("Se ha modificado correctamente la fecha de vencimiento" , true);
            }
            catch (Exception exception)
            {
                Mensaje("Error: " + exception.Message,false);
            }
        }

        private void Mensaje(string pMensaje, bool pEstado)
        {
            if (pEstado)
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
    }
}