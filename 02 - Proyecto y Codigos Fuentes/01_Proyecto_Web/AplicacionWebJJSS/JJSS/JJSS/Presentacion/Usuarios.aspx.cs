using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data;

using System.Collections;

namespace JJSS.Presentacion
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private GestorUsuarios gestorUsuarios;
      

        private alumno alumnoElegido;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorUsuarios = new GestorUsuarios();
           
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'SEGURIDAD_PERMISOS'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }


                cargarUsuarios();

            }
        }


        protected void cargarUsuarios()
        {
            DataTable dtCompleta= gestorUsuarios.obtenerTablaUsuarios();
            DataTable dtConFiltro;

            dtConFiltro = dtCompleta.Clone();
            string filtroApellido = txt_filtro_apellido.Text.ToUpper().Trim();
            string filtroGrupo = txt_filtro_grupo.Text.ToUpper().Trim();

            for (int i = 0; i < dtCompleta.Rows.Count; i++)
            {
                DataRow dr = dtCompleta.Rows[i];
                if (dr["nombre"].ToString().ToUpper().StartsWith(filtroApellido) && dr["grupo_nombre"].ToString().ToUpper().Contains(filtroGrupo))
                {
                    dtConFiltro.ImportRow(dr);
                };
            }
            gvUsuarios.DataSource = dtConFiltro;
            gvUsuarios.DataBind();
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


        protected void gvClases_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("permisos") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idUsuario = Convert.ToInt32(gvUsuarios.DataKeys[index].Value);
                Session["UsuarioEditar"] = idUsuario.ToString();
                Response.Redirect("../Presentacion/ModificarPerfil");
            }
        }

        protected void gvClases_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            cargarUsuarios();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarUsuarios();
        }

        protected void btn_Cancelar_Click1(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/MenuInicial.aspx");
        }
    }
}