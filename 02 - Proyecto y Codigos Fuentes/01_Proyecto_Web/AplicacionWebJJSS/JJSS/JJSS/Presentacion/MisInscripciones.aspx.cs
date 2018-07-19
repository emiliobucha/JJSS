using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Presentacion
{
    public partial class MisInscripciones : System.Web.UI.Page
    {
        private GestorAlumnos gestorAlumnos;
        private GestorProfesores gestorProfesores;
        private GestorInscripcionesClase gic;
        private GestorInscripciones git;
        private GestorInscripcionesEvento gie;
        private static Boolean verTodosTorneo = false;
        private static Boolean verTodosClase = false;
        private static Boolean verTodosEvento = false;
        private static int idAlumno = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorAlumnos = new GestorAlumnos();
            gic = new GestorInscripcionesClase();
            git = new GestorInscripciones();
            gie = new GestorInscripcionesEvento();

            if (!IsPostBack)
            {
                btn_todos_clases.Text = "Ver Todas";
                btn_todos_eventos.Text = "Ver Todas";
                btn_todos_torneos.Text = "Ver Todas";

                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {
                    var usuario = sesionActiva.usuario;
                    if (usuario != null)
                    {
                        var alumno = gestorAlumnos.ObtenerAlumnoPorIdUsuario(usuario.id_usuario);

                        if (alumno != null)
                        {
                            idAlumno = alumno.id_alumno;
                            CargarGrillaClases();
                            CargarGrillaTorneos();
                            CargarGrillaEventos();
                        }
                        else
                        {

                            var profesor = gestorProfesores.ObtenerProfesorPorIdUsuario(usuario.id_usuario);
                            if (profesor != null)
                            {

                            }
                            else
                            {
                                //var admin = gestorAdmin.ObtenerAdminPorIdUsuario(usuario.id_usuario);
                                //if (admin != null)
                                //{
                                //    if (admin.id_tipo_documento != null) tipoDoc = (int)admin.id_tipo_documento;
                                //    dni = admin.dni;

                                //    nombre = admin.nombre + " " + admin.apellido;
                                //    CargarGrilla();
                                //}
                            }
                        }
                    }
                }
            }
        }

        private void CargarGrillaClases()
        {
            gv_clases.DataSource = gic.ObtenerInscripcionesDeAlumno(idAlumno, verTodosClase);
            gv_clases.DataBind();
        }

        private void CargarGrillaTorneos()
        {
            gv_torneos.DataSource = git.ObtenerInscripcionesDeAlumno(idAlumno, verTodosTorneo);
            gv_torneos.DataBind();
        }

        private void CargarGrillaEventos()
        {
            gv_eventos.DataSource = gie.ObtenerInscripcionesDeAlumno(idAlumno, verTodosEvento);
            gv_eventos.DataBind();
        }

        protected void gv_clases_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_clases.PageIndex = e.NewPageIndex;
            CargarGrillaClases();
        }

        protected void gv_torneos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_torneos.PageIndex = e.NewPageIndex;
            CargarGrillaTorneos();
        }

        protected void gv_eventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_eventos.PageIndex = e.NewPageIndex;
            CargarGrillaEventos();
        }

        protected void gv_torneos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkPago = (HyperLink)e.Row.FindControl("lnk_pago");

                Boolean pago = DataBinder.Eval(e.Row.DataItem, "pago") != null;
                DateTime? fechaTorneo = Convert.ToDateTime( DataBinder.Eval(e.Row.DataItem, "dtFecha") );
                if (pago || fechaTorneo<=DateTime.Now) lnkPago.Visible = false;
                else lnkPago.Visible = true;
            }
        }

        protected void gv_clases_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("baja") == 0)
            {
                int idClase = Convert.ToInt32(e.CommandArgument);
               string res = gic.DarDeBajaInscripcion(idAlumno, idClase);
                if (res.CompareTo("") == 0)
                {
                    mensaje("Se dió de baja a la inscripción correctamente", true);
                    verTodosClase = false;
                    CargarGrillaClases();
                }else
                {
                    mensaje(res, false);
                }

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

        protected void btn_todos_clases_Click(object sender, EventArgs e)
        {
            if (btn_todos_clases.Text.CompareTo("Ver Todas") == 0)
            {
                verTodosClase = true;
                CargarGrillaClases();
                btn_todos_clases.Text = "Ver Menos";
            }
            else
            {
                verTodosClase = false;
                CargarGrillaClases();
                btn_todos_clases.Text = "Ver Todas";
            }
            
        }

        protected void btn_todos_torneos_Click(object sender, EventArgs e)
        {
            if (btn_todos_torneos.Text.CompareTo("Ver Todas") == 0)
            {
                verTodosTorneo = true;
                CargarGrillaTorneos();
                btn_todos_torneos.Text = "Ver Menos";
            }
            else
            {
                verTodosTorneo = false;
                CargarGrillaTorneos();
                btn_todos_torneos.Text = "Ver Todas";
            }
        }

        protected void btn_todos_eventos_Click(object sender, EventArgs e)
        {
            if (btn_todos_eventos.Text.CompareTo("Ver Todas") == 0)
            {
                verTodosEvento = true;
                CargarGrillaEventos();
                btn_todos_eventos.Text = "Ver Menos";
            }
            else
            {
                verTodosEvento = false;
                CargarGrillaEventos();
                btn_todos_eventos.Text = "Ver Todas";
            }
        }

        protected void gv_clases_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnBaja = (Button)e.Row.FindControl("btn_baja");

                int actual = Convert.ToInt16( DataBinder.Eval(e.Row.DataItem, "insActual"));
                
                if (actual == 0) btnBaja.Visible = false;
                else btnBaja.Visible = true;
            }
        }
    }
}