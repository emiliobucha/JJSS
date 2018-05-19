using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;
using System.IO;
using System.Data;


namespace JJSS.Presentacion
{
    public partial class AgregarProducto : System.Web.UI.Page
    {
        private GestorProductos gestorProductos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'PRODUCTOS_AGREGAR'");
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



                gestorProductos = new GestorProductos();
                cargarComboCategorias();
                MultiView1.SetActiveView(view_formulario);
                btn_formulario.CssClass = "btn btn-info";
                btn_grilla.CssClass = "btn btn-default";
                gv_productos.AllowPaging = true;
                gv_productos.PageSize = 20;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            string nombre = txt_nombre.Text;
            int idCategoria = int.Parse(ddl_categoria.SelectedValue);

            System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
            byte[] imagenByte;
            //if (imagen == null)
            //{
            //    imagenByte = null;
            //}
            //else
            //{
                
                using (MemoryStream ms = new MemoryStream())
                {
                    imagen.CopyTo(ms);
                    imagenByte = ms.ToArray();
                }
            //}

            gestorProductos = new GestorProductos();
            string sReturn = gestorProductos.AgregarProducto(idCategoria, nombre, imagenByte);
            if (sReturn == "")
            {
                mensaje("Producto agregado exitosamente", true);
                limpiar();
            }
            else mensaje(sReturn, false);

        }

        protected void cargarComboCategorias()
        {
            ddl_categoria.DataSource = gestorProductos.ObtenerCategorias();
            ddl_categoria.DataTextField = "nombre";
            ddl_categoria.DataValueField = "id_categoria";
            ddl_categoria.DataBind();
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

        private void limpiar()
        {
            txt_nombre.Text = "";
            ddl_categoria.SelectedIndex = 0;

        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx#section_tienda");
        }

        protected void btn_formulario_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_formulario);
            btn_formulario.CssClass = "btn btn-info";
            btn_grilla.CssClass = "btn btn-default";
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void btn_grilla_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_grilla);
            btn_formulario.CssClass = "btn btn-default";
            btn_grilla.CssClass = "btn btn-info";
            CargarGrilla();
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void CargarGrilla()
        {
            gestorProductos = new GestorProductos();
            DataTable dtCompleta = new DataTable();
            DataTable dtConFiltro = new DataTable();


            dtCompleta = gestorProductos.ObtenerProductos();

            dtConFiltro = dtCompleta.Clone();
            string filtroNombre = txt_filtro_nombre.Text.ToUpper().Trim();

            for (int i = 0; i < dtCompleta.Rows.Count; i++)
            {
                DataRow dr = dtCompleta.Rows[i];
                if (dr["nombre"].ToString().ToUpper().StartsWith(filtroNombre))
                {
                    dtConFiltro.ImportRow(dr);
                };
            }

            gv_productos.DataSource = dtConFiltro;
            gv_productos.DataBind();
        }

        protected void gv_productos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_productos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

    }
}