using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion.Clases
{
    public partial class GraduarAlumnoIndividual : System.Web.UI.Page
    {
        private static int idAlumno = 0;
        private static int idTipo = 0;
        private GestorGraduacion gg;
        private GestorAlumnos ga;

        protected void Page_Load(object sender, EventArgs e)
        {
            gg = new GestorGraduacion();
            ga = new GestorAlumnos();

            if (!IsPostBack)
            {
                if (Session["alumnoGraduar"] != null && Session["tipoGraduar"] != null)
                {
                    idAlumno = Convert.ToInt32(Session["alumnoGraduar"]);
                    idTipo = Convert.ToInt32(Session["tipoGraduar"]);
                    CargarDatos();
                }

                
            }
        }

        private void CargarDatos()
        {
            AlumnoFaja alumnoSeleccionado = gg.buscarAlumnoFajaPorID(idAlumno, idTipo);
            lblNombre.InnerText = alumnoSeleccionado.nombre + " " + alumnoSeleccionado.apellido;
            lbl_disciplina.InnerText = alumnoSeleccionado.tipo;
            lbl_faja_actual.InnerText = alumnoSeleccionado.faja;
            lblDni.InnerText = alumnoSeleccionado.dni;
            
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int grados = Convert.ToInt32( txt_grados.Text);

            try
            {
                gg.graduarIndividual(idAlumno, idTipo, grados);
                mensaje("Se graduó el alumno exitosamente", true);
                CargarDatos();
            }catch(Exception ex)
            {
                mensaje(ex.Message, false);
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
    }
}