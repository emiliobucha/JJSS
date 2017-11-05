using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class RegistrarAsistencia : System.Web.UI.Page
    {
        private GestorAcademias gestorAcademias;
        private GestorAsistencia gestorAsistencia;
        private GestorAlumnos gestorAlumno;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            gestorAsistencia = new GestorAsistencia();
            gestorAlumno = new GestorAlumnos();
            int ubicacion = int.Parse(ddl_ubicacion.SelectedValue);

            ClasesHorariosAsistencia claseActual = gestorAsistencia.buscarClaseSegunHoraActual(ubicacion);

            if (claseActual == null) mensaje("No hay clases disponibles en este horario", false);
            else
            {
                alumno alu = gestorAlumno.ObtenerAlumnoPorDNI(int.Parse(txtDni.Text));
                if (alu != null)
                {



                    string resultado = gestorAsistencia.ValidarTipoClaseAlumno(alu.id_alumno, claseActual.tipoClase);
                    if (resultado == "")
                    {
                        if (gestorAsistencia.ValidarPagoParaAsistencia(alu.id_alumno, claseActual.tipoClase))
                        {
                            resultado = gestorAsistencia.registrarAsistencia(alu.id_alumno, claseActual.idClase,
                                claseActual.idHorario);
                            if (resultado == "") mensaje("Asistencia registrada exitosamente", true);
                            else mensaje(resultado, false);
                        }
                        else mensaje("Falta pago", false);

                    }
                    else mensaje(resultado, false);
                }
                else mensaje("No esta inscripto el alumno con ese DNI", false);
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
            Response.Redirect("../Presentacion/Inicio.aspx");
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
    }
}