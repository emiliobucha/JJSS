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
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "/Presentacion/Login.aspx" + "', 2000);</script>");
                        }
                    }
                }
            }
        }

        private void CargarGrillaClases()
        {
            gv_clases.DataSource = gic.ObtenerInscripcionesDeAlumno(idAlumno);
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
                Label lblPago = (Label)e.Row.FindControl("lbl_pagado");

                Boolean pago = DataBinder.Eval(e.Row.DataItem, "pago") != null;
                DateTime? fechaTorneo = Convert.ToDateTime( DataBinder.Eval(e.Row.DataItem, "dtFecha") );
                if (pago || fechaTorneo <= DateTime.Now) lblPago.Text = "SI";
                else lblPago.Text = "NO";
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

        protected void btn_si_Click(object sender, EventArgs e)
        {
            int inscripcion = int.Parse(txtIDSeleccionado.Text);
            string res = gic.DarDeBajaInscripcionPorId(inscripcion, idAlumno);

            if (res.CompareTo("") == 0)
            {
                mensaje("Se dió de baja a la inscripción correctamente", true);
                CargarGrillaClases();
            }
            else
            {
                mensaje(res, false);
            }
            txtIDSeleccionado.Text = "";
        }
    }
}