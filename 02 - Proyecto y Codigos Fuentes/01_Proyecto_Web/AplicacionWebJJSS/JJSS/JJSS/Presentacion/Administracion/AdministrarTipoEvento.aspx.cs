﻿using System;
using System.Web;
using JJSS_Negocio;
using JJSS_Negocio.Administracion;

namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarTipoEvento : System.Web.UI.Page
    {
        private GestorTipoEvento gte;
        private static int idTipoEvento = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            gte = new GestorTipoEvento();

            if (!IsPostBack)
            {

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TIPO_EVENTO_ADMINISTRACION'");
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

                cargarGrilla();
            }

        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            string nombre = txt_nombre.Text;

            if (idTipoEvento == 0)
            {
                string res = gte.crearTipoEvento(nombre);
                if (res.CompareTo("") == 0) redirigirPorExito("Se ha creado el tipo de evento exitosamente");
                else mensaje(res, false);
                cargarGrilla();
                txt_nombre.Text = "";
            }
            else
            {
                string res = gte.modificarTipoEvento(idTipoEvento, txt_nombre.Text);
                if (res.CompareTo("") == 0)
                {
                    idTipoEvento = 0;
                    redirigirPorExito("Se ha modificado el tipo de evento exitosamente");
                }
                else mensaje(res, false);
                cargarGrilla();
                btn_aceptar.Text = "Agregar";
                txt_nombre.Text = "";
            }
        }

        private void redirigirPorExito(string strMensaje)
        {
            string anterior = ViewState["RefUrl"].ToString();

            if (anterior.EndsWith("CrearEvento"))
            {
                Session["mensaje"] = strMensaje;
                Session["exito"] = true;
                Response.Redirect(anterior);
            }
            else this.mensaje(strMensaje, true);
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
            gv_tipo_evento.DataSource = gte.ObtenerTodosTipoEventosConFiltro("");
            gv_tipo_evento.DataBind();
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["RefUrl"].ToString());
        }

        protected void gv_tipo_evento_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gv_tipo_evento.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void gv_tipo_evento_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idEvento = Convert.ToInt32(gv_tipo_evento.DataKeys[index].Value);

                string res = gte.eliminarTipoEvento(idEvento);
                if (res.CompareTo("") == 0) mensaje("Se ha eliminado exitosamente", true);
                else mensaje(res, false);
                cargarGrilla();
            }
            else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idEvento = Convert.ToInt32(gv_tipo_evento.DataKeys[index].Value);

                txt_nombre.Text = gv_tipo_evento.Rows[index].Cells[0].Text;
                idTipoEvento = idEvento;
                btn_aceptar.Text = "Guardar edición";
            }
        }
    }
}