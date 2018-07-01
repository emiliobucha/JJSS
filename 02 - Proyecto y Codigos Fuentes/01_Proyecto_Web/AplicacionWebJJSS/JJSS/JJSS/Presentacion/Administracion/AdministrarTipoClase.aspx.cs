﻿using System;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarTipoClase : System.Web.UI.Page
    {
        private GestorTipoClase gtc;
        private static int idTipoClase = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            gtc = new GestorTipoClase();

            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "Menu_Administracion.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();

                cargarGrilla();
            }

        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            string nombre = txt_nombre.Text;

            if (idTipoClase == 0)
            {
                string res = gtc.crearTipoClase(nombre);
                if (res.CompareTo("") == 0) mensaje("Se ha creado el tipo de clase exitosamente", true);
                else mensaje(res, false);
                cargarGrilla();
            }
            else
            {
                string res = gtc.modificarTipoClase(idTipoClase, txt_nombre.Text);
                if (res.CompareTo("") == 0)
                {
                    mensaje("Se ha eliminado exitosamente", true);
                    idTipoClase = 0;
                }
                else mensaje(res, false);
                cargarGrilla();
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

        public void cargarGrilla()
        {
            gv_tipo_evento.DataSource = gtc.ObtenerTodosTipoClasesConFiltro("");
            gv_tipo_evento.DataBind();
        }

        protected void gv_tipo_evento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idClase = Convert.ToInt32(gv_tipo_evento.DataKeys[index].Value);

            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                string res = gtc.eliminarTipoClase(idClase);
                if (res.CompareTo("") == 0) mensaje("Se ha eliminado exitosamente", true);
                else mensaje(res, false);
                cargarGrilla();
            } else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                txt_nombre.Text = gv_tipo_evento.Rows[index].Cells[0].Text;
                idTipoClase = idClase;
            }
        }

        protected void gv_tipo_evento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_tipo_evento.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["RefUrl"].ToString());
        }
    }
}