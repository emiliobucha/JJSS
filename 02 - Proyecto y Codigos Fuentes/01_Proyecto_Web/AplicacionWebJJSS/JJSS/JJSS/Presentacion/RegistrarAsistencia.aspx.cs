﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

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
            ddl_ubicacion.DataSource = academias;
            ddl_ubicacion.DataTextField = "nombre";
            ddl_ubicacion.DataValueField = "id_academia";
            ddl_ubicacion.DataBind();


        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            gestorAsistencia = new GestorAsistencia();
            gestorAlumno = new GestorAlumnos();

            int idTipoClase = gestorAsistencia.buscarTipoClaseSegunHoraActual(int.Parse(ddl_ubicacion.SelectedValue));

            if (idTipoClase == 0) mensaje("No hay clases disponibles en este horario", false);
            else
            {


                alumno alu = gestorAlumno.ObtenerAlumnoPorDNI(int.Parse(txtDni.Text));

                string resultado = gestorAsistencia.ValidarTipoClaseAlumno(alu.id_alumno, idTipoClase);
                if (resultado == "")
                {
                    if (gestorAsistencia.ValidarPagoParaAsistencia(alu.id_alumno, idTipoClase))
                    {
                        resultado = gestorAsistencia.registrarAsistencia(alu.id_alumno, idTipoClase);
                        if (resultado == "") mensaje("Asistencia registrada exitosamente", true);
                        else mensaje(resultado, false);
                    }
                    else mensaje("Falta pago", false);

                }
                else mensaje(resultado, false);
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
    }
}