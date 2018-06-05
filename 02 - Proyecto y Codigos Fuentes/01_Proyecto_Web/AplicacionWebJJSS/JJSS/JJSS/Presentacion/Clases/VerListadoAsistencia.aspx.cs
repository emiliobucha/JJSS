﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion.Clases
{
    public partial class VerListadoAsistencia : System.Web.UI.Page
    {
        private GestorAsistencia gAsistencia;
        private static List<ListadoAsistencia> asistentes;
        protected void Page_Load(object sender, EventArgs e)
        {
            gAsistencia = new GestorAsistencia();
            if (!IsPostBack)
            {
                cargarComboClases();
                asistentes = new List<ListadoAsistencia>();
            }
        }

        private void cargarComboClases()
        {
            GestorClases gc = new GestorClases();
            List<ClasesDisponibles> clases = gc.ObtenerClasesDisponibles("", 0, 0);

            ClasesDisponibles primerElemento = new ClasesDisponibles()
            {
                id_clase = -1,
                nombre = "Seleccione una clase",
            };
            clases.Insert(0, primerElemento);
            ddl_clases.DataSource = clases;
            ddl_clases.DataValueField = "id_clase";
            ddl_clases.DataTextField = "nombre";
            ddl_clases.DataBind();
        }

        private void cargarGrillaAsistencia()
        {
            gv_asistencia.DataSource = asistentes;
            gv_asistencia.DataBind();
        }

        protected void gv_asistencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_asistencia.PageIndex = e.NewPageIndex;
            cargarGrillaAsistencia();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            int idClase = int.Parse(ddl_clases.SelectedValue);
            if (idClase == -1)
            {
                mensaje("Debe seleccionar una clase", false);
                return;
            }
            DateTime fecha = DateTime.Parse(dp_fecha.Text);

            asistentes = gAsistencia.ListadoAsistentes(idClase, fecha);
            cargarGrillaAsistencia();
            lbl_datos_clase.Text = asistentes.First().cla_tipo + " - " + asistentes.First().cla_nombre;
            lbl_hora.Text = asistentes.First().horario_nombre;
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

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                String sFile = gAsistencia.GenerarListado(asistentes);

                Response.Clear();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Disposition",
                    "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                Response.WriteFile(sFile);

            }
            catch (Exception ex)
            {
                mensaje("No se encuentran alumnos asistentes a esta clase", false);
            }
        }
    }
}