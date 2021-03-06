﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Administracion;

namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarSedes : System.Web.UI.Page
    {
        private GestorSedes gs;
        private GestorAcademias ga;

        protected void Page_Load(object sender, EventArgs e)
        {
            gs = new GestorSedes();
            ga = new GestorAcademias();
            if (!IsPostBack)
            {

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'SEDES_ADMINISTRACION'");
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



                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "Menu_Administracion.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();


                if (Session["mensaje"] != null && Session["mensaje"].ToString().Trim()!="")
                {
                    mensaje(Session["mensaje"].ToString(), Convert.ToBoolean(Session["exito"]));
                    Session["mensaje"] = null;
                }
                CargarComboCiudades();
                CargarGrilla();
            }
        }

        private void CargarComboCiudades()
        {
            List<ciudad> ciudadList = gs.ObtenerCiudades();
            ciudad primerElemento = new ciudad()
            {
                id_ciudad = 0,
                nombre = "Todos",
            };
            ciudadList.Insert(0, primerElemento);
            ddl_filtro_ciudad.DataSource = ciudadList;
            ddl_filtro_ciudad.DataValueField = "id_ciudad";
            ddl_filtro_ciudad.DataTextField = "nombre";
            ddl_filtro_ciudad.DataBind();

        }

        private void CargarGrilla()
        {
            //filtros
            string filtroNombre = txt_filtro_nombre.Text;
            string filtroCiudad = ddl_filtro_ciudad.SelectedItem.Text;
            int filtroSede = Convert.ToInt16(rbSede.SelectedItem.Value);

            if (filtroSede == 0)
            {
               gvSedes.DataSource = gs.ObtenerSedesConFiltro(filtroNombre, filtroCiudad);
                gvSedes.DataBind();
            }else if (filtroSede == 1)
            {
                gvSedes.DataSource = ga.ObtenerAcademiasConFiltro(filtroNombre, filtroCiudad);
                gvSedes.DataBind();
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void gvSedes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSedes.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvSedes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gvSedes.DataKeys[index].Value);
                int filtroSede = Convert.ToInt16(rbSede.SelectedItem.Value);

                if (filtroSede == 0) Session["sede"] = id;
                else if (filtroSede == 1) Session["academia"] = id;

                Response.Redirect("CrearSede.aspx");
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

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["RefUrl"].ToString());
        }

        protected void btn_si_Click1(object sender, EventArgs e)
        {
            int filtroSede = Convert.ToInt16(rbSede.SelectedItem.Value);
            if (txtIDSeleccionado.Text != "")
            {
                int idSede = Convert.ToInt16(txtIDSeleccionado.Text);
                string res = "";

                if (filtroSede == 0)
                {
                    res = gs.EliminarSede(idSede);
                }
                else if (filtroSede == 1)
                {
                    res = ga.EliminarAcademia(idSede);
                }

                res = gs.EliminarSede(idSede);
                if (res.CompareTo("") == 0) mensaje("Se eliminó la sede exitosamente", true);
                else mensaje(res, false);
                CargarGrilla();
                txtIDSeleccionado.Text = "";
            }
        }
    }
}