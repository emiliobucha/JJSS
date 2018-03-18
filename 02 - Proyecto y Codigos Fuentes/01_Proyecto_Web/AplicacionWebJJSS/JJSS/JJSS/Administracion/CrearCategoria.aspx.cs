﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Administracion;
using System.Globalization;

namespace JJSS.Administracion
{
    public partial class CrearCategoria : System.Web.UI.Page
    {
        private static GestorCategoria gestorCategorias;
        private static GestorTipoClase gestorTipoClase;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorCategorias = new GestorCategoria();
                gestorTipoClase = new GestorTipoClase();

                CargarComboTipoClase();
            }
            
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void CargarComboTipoClase()
        {
            List<tipo_clase> tipos = gestorTipoClase.ObtenerTipoClase();
            ddlDisciplina.DataSource = tipos;
            ddlDisciplina.DataTextField = "nombre";
            ddlDisciplina.DataValueField = "id_tipo_clase";
            ddlDisciplina.DataBind();
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {

                float pesoMin = float.Parse(txtPesoMinimo.Text, CultureInfo.InvariantCulture);
                Math.Round(pesoMin, 2);

                float pesoMax = float.Parse(txtPesoMaximo.Text, CultureInfo.InvariantCulture);
                Math.Round(pesoMax, 2);

                categoria nuevaCategoria = new categoria();
                nuevaCategoria.nombre = txt_nombre.Text;
                nuevaCategoria.peso_desde = pesoMin;
                nuevaCategoria.peso_hasta = pesoMax;
                nuevaCategoria.edad_desde = int.Parse(txtEdadMinima.Text);
                nuevaCategoria.edad_hasta = int.Parse(txtEdadMaxima.Text);

                short? sexo = null;
                if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
                if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

                nuevaCategoria.sexo = sexo;
                nuevaCategoria.id_tipo_clase = int.Parse(ddlDisciplina.SelectedValue);

                String res = gestorCategorias.crearCategoria(nuevaCategoria);
                if (res.CompareTo("") == 0)
                {
                    limpiar();
                    mensaje("Se ha creado la categoría exitosamente", true);
                }
                else mensaje(res, false);
            }

        }

        private Boolean validar()
        {
            float pesoMin = float.Parse(txtPesoMinimo.Text, CultureInfo.InvariantCulture);
            float pesoMax = float.Parse(txtPesoMaximo.Text, CultureInfo.InvariantCulture);
            if (pesoMax <= pesoMin)
            {
                mensaje("El peso mínimo debe ser menor al peso máximo", false);
                return false;
            }
            int edadMin = int.Parse(txtEdadMinima.Text);
            int edadMax = int.Parse(txtEdadMaxima.Text);
            if (edadMax <= edadMin)
            {
                mensaje("La edad mínima debe ser menor a la edad máxima", false);
                return false;
            }
            return true;
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
            // Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>");
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

        private void limpiar()
        {
            txtEdadMaxima.Text = "";
            txtEdadMinima.Text = "";
            txtPesoMaximo.Text = "";
            txtPesoMinimo.Text = "";
            txt_nombre.Text = "";
            
            ddlDisciplina.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = true;

        }
    }
}