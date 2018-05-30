﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio.Administracion;
using JJSS_Negocio;

namespace JJSS.Presentacion.Administracion
{
    public partial class CrearSede : System.Web.UI.Page
    {
        private static GestorCiudades gestorCiudades;
        private static GestorProvincias gestorProvincias;
        private static GestorSedes gestorSedes;
        private static GestorAcademias gestorAcademias;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/MenuInicial.aspx";
            else ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            if (!IsPostBack)
            {
                gestorCiudades = new GestorCiudades();
                gestorProvincias = new GestorProvincias();
                gestorSedes = new GestorSedes();
                gestorAcademias = new GestorAcademias();

                CargarComboProvincias();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void CargarComboProvincias()
        {
            List<provincia> provincias = gestorProvincias.ObtenerProvincias();
            ddl_provincia.DataSource = provincias;
            ddl_provincia.DataTextField = "nombre";
            ddl_provincia.DataValueField = "id_provincia";
            ddl_provincia.DataBind();
        }

        protected void CargarComboCiudades(int pProvincia)
        {
            List<ciudad> ciudades = gestorCiudades.ObtenerCiudadesPorProvincia(pProvincia);
            ddl_localidad.DataSource = ciudades;
            ddl_localidad.DataTextField = "nombre";
            ddl_localidad.DataValueField = "id_ciudad";
            ddl_localidad.DataBind();
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

        protected void ddl_provincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_provincia = int.Parse(ddl_provincia.SelectedValue);
            CargarComboCiudades(id_provincia);
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

        private void limpiar()
        {
            txt_calle.Text = "";
            txt_nombre.Text = "";
            txt_nro_dpto.Text = "";
            txt_numero.Text = "";
            txt_piso.Text = "";

            if (ddl_localidad.Items.Count != 0) ddl_localidad.SelectedIndex = 0;

            ddl_provincia.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = true;

        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            
            string calle = txt_calle.Text;
            string departamento = txt_nro_dpto.Text;
            string torre = txt_torre.Text;

            int? piso = null;
            if (txt_piso.Text != "")
            {
                piso = int.Parse(txt_piso.Text);
            }
            int? numero = null;
            if (txt_numero.Text != "")
            {
                numero = int.Parse(txt_numero.Text);
            }
            int ciudad = int.Parse(ddl_localidad.SelectedValue);
            long? telefono = null;
            if (txt_telefono.Text != "")
            {
                telefono = long.Parse(txt_telefono.Text);
            }

            direccion nuevaDireccion = new direccion();
            nuevaDireccion.calle = calle;
            nuevaDireccion.id_ciudad = ciudad;
            nuevaDireccion.numero = numero;
            nuevaDireccion.piso = piso;
            nuevaDireccion.torre = torre;
            nuevaDireccion.departamento = departamento;

            string nombre = txt_nombre.Text;

            if (rbCrear.SelectedValue.CompareTo("sede") == 0)
            {
                crearSede(nombre,nuevaDireccion,telefono);
            }else if (rbCrear.SelectedValue.CompareTo("academia") == 0)
            {
                crearAcademia(nombre, nuevaDireccion,telefono);
            }


        }

        private void crearSede(String nombre, direccion nuevaDireccion,long? telefono)
        {
            String res = gestorSedes.GenerarNuevaSede(nombre, nuevaDireccion,telefono);
            if (res.CompareTo("") == 0)
            {
                limpiar();
                mensaje("Se ha creado la sede correctamente", true);
                
            }
            else mensaje(res, false);
        }

        private void crearAcademia(String nombre, direccion nuevaDireccion, long? telefono)
        {
            String res = gestorAcademias.GenerarAcademia(nombre, nuevaDireccion, telefono);
            if (res.CompareTo("") == 0)
            {
                limpiar();
                mensaje("Se ha creado la academia correctamente", true);
                
            }
            else mensaje(res, false);
        }

        protected void btn_cancelar_Click1(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
    }
}