﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;

namespace JJSS.Presentacion
{
    public partial class CrearClase : System.Web.UI.Page
    {
        private DataTable dtHorarios;
        private GestorClases gestorClases;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dg_horarios.AutoGenerateColumns = false;
                gestorClases = new GestorClases();
                CargarComboTipos();
                CargarComboUbicacion();
                cargarDatosClase();
            }
        }

        protected void cargarDatosClase()
        {
            /*Si el id de la clase no esta seleccionada de modificacion*/
            int idClase = 0;

            if (Session["clase"] != null)
            {
                idClase = int.Parse(Session["clase"].ToString());
                clase claseSeleccionada = gestorClases.ObtenerClasePorId(idClase);
                txt_nombre.Text = claseSeleccionada.nombre.ToString();
                txt_precio.Text = claseSeleccionada.precio.ToString();
                ddl_tipo_clase.SelectedValue = claseSeleccionada.id_tipo_clase.ToString();
                ddl_ubicacion.SelectedValue = claseSeleccionada.id_ubicacion.ToString();
                txt_nombre.Enabled = false;
                ddl_tipo_clase.Enabled = false;
                ddl_ubicacion.Enabled = false;
            }
            else limpiar();
            dtHorarios = gestorClases.ObtenerTablaHorarios(idClase);
            dtHorarios.AcceptChanges();

            DataView dv_horarios = dtHorarios.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_horarios.DataSource = dv_horarios;
            dg_horarios.DataBind();




            //Session.Add("dtHorarios", dtHorarios);
            Session["dtHorarios"] = dtHorarios;
        }

        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            dtHorarios = (DataTable)Session["dtHorarios"];
            //dtHorarios = (DataTable)dg_horarios;


            if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex).Length > 0)
            {
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde <= '" + txt_horadesde.Text + "' and hora_hasta > '" + txt_horadesde.Text + "'").Length > 0)
                {
                    mensaje("Ya hay una clase en ese horario",false);
                    return;
                }
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde < '" + txt_horahasta.Text + "' and hora_hasta >= '" + txt_horahasta.Text + "'").Length > 0)
                {
                    mensaje("Ya hay una clase en ese horario",false);
                    return;
                }
            }

            DataRow drNuevoHorario = dtHorarios.NewRow();
            drNuevoHorario["nombre_dia"] = ddl_dia.SelectedValue;
            drNuevoHorario["dia"] = ddl_dia.SelectedIndex;
            drNuevoHorario["hora_desde"] = txt_horadesde.Text;
            drNuevoHorario["hora_hasta"] = txt_horahasta.Text;
            dtHorarios.Rows.Add(drNuevoHorario);

            dtHorarios.AcceptChanges();
            DataView dv_horarios = dtHorarios.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_horarios.DataSource = dv_horarios;
            dg_horarios.DataBind();
            Session["dtHorarios"] = dtHorarios;

        }

        private void CargarComboTipos()
        {
            List<tipo_clase> tipo = gestorClases.ObtenerTipoClases();
            ddl_tipo_clase.DataSource = tipo;
            ddl_tipo_clase.DataTextField = "nombre";
            ddl_tipo_clase.DataValueField = "id_tipo_clase";
            ddl_tipo_clase.DataBind();
        }

        private void CargarComboUbicacion()
        {
            List<academia> academias = gestorClases.ObtenerAcademias();
            ddl_ubicacion.DataSource = academias;
            ddl_ubicacion.DataTextField = "nombre";
            ddl_ubicacion.DataValueField = "id_academia";
            ddl_ubicacion.DataBind();
        }

        protected void dg_horarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int dia = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "dia"));
            switch (dia)
            {
                case 0:
                    e.Row.BackColor = System.Drawing.Color.Lavender;
                    break;
                case 1:
                    e.Row.BackColor = System.Drawing.Color.PaleTurquoise;
                    break;
                case 2:
                    e.Row.BackColor = System.Drawing.Color.PowderBlue;
                    break;
                case 3:
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                    break;
                case 4:
                    e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                    break;
                case 5:
                    e.Row.BackColor = System.Drawing.Color.Turquoise;
                    break;
                case 6:
                    e.Row.BackColor = System.Drawing.Color.MediumTurquoise;
                    break;
                default:
                    e.Row.BackColor = System.Drawing.Color.Black;
                    break;
            }


        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            gestorClases = new GestorClases();
            string sReturn = "";
            Boolean estado = false;
            if (Session["clase"] != null)
            {
                int idClase = 0;
                idClase = int.Parse(Session["clase"].ToString());
                sReturn = gestorClases.modificarClase(idClase, (DataTable)Session["dtHorarios"], double.Parse(txt_precio.Text));
                if (sReturn.CompareTo("") == 0)
                {
                    estado = true;
                    sReturn = "La clase se actualizó correctamente";
                }
            }
            else
            {
                int tipo = int.Parse(ddl_tipo_clase.SelectedValue.ToString());
                int ubicacion = int.Parse(ddl_ubicacion.SelectedValue.ToString());
                sReturn = gestorClases.GenerarNuevaClase(tipo, Double.Parse(txt_precio.Text), (DataTable)Session["dtHorarios"], txt_nombre.Text, ubicacion);
                if (sReturn.CompareTo("") == 0)
                {
                    estado = true;
                    sReturn = "La clase se ha creado exitosamente";
                }
            }

            mensaje(sReturn,estado);

        }
        
        protected void dg_horarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Eliminar") == 0)
            {
                int horario = Convert.ToInt32(e.CommandArgument);

                dtHorarios = (DataTable)Session["dtHorarios"];
                dtHorarios.Rows.RemoveAt(horario);
                dg_horarios.DataSource = dtHorarios;
                dg_horarios.DataBind();
                
            }
        }

        protected void btn_nueva_clase_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void limpiar()
        {
            txt_horadesde.Text = "";
            txt_horahasta.Text = "";
            txt_nombre.Text = "";
            txt_precio.Text = "";
            txt_nombre.Enabled = true;
            ddl_ubicacion.Enabled = true;
            ddl_tipo_clase.Enabled = true;
            Session["clase"] = null;
            dtHorarios = (DataTable)Session["dtHorarios"];
            //if (dtHorarios != null)
            //{
            //    for (int i = 0; i < dtHorarios.Rows.Count; i++)
            //    {
            //        dtHorarios.Rows.RemoveAt(i);
            //    }
            //}

            dg_horarios.DataSource = null;
            dg_horarios.DataBind();
            Session["dtHorarios"] = null;
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