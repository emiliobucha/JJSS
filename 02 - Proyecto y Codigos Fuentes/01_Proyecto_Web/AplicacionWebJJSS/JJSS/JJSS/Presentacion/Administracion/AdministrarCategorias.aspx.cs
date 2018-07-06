using System;
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
    public partial class AdministrarCategorias : System.Web.UI.Page
    {
        private GestorCategoria gc;

        protected void Page_Load(object sender, EventArgs e)
        {
            gc = new GestorCategoria();
            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CATEGORIA_ADMINISTRACION'");
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



                if (Session["mensaje"] != null)
                {
                    mensaje(Session["mensaje"].ToString(), Convert.ToBoolean( Session["exito"]));
                    Session["mensaje"] = null;
                }
                CargarComboTipoClase();
                CargarGrilla();
            }
        }

        private void CargarComboTipoClase()
        {
            JJSS_Negocio.GestorTipoClase gtc = new JJSS_Negocio.GestorTipoClase();
            List<tipo_clase> tipoClaseList = gtc.ObtenerTipoClase();
            tipo_clase primerElemento = new tipo_clase()
            {
                id_tipo_clase = 0,
                nombre = "Todos",
            };
            tipoClaseList.Insert(0, primerElemento);
            ddl_filtro_disciplina.DataSource = tipoClaseList;
            ddl_filtro_disciplina.DataValueField = "id_tipo_clase";
            ddl_filtro_disciplina.DataTextField = "nombre";
            ddl_filtro_disciplina.DataBind();
        }

        private void CargarGrilla()
        {
            //filtros
            string filtroNombre = txt_filtro_nombre.Text;
            string filtroDisciplina = ddl_filtro_disciplina.SelectedItem.Text;
            int filtroSexo = Convert.ToInt16(rbSexo.SelectedItem.Value);

            gvCategorias.DataSource = gc.ObtenerTodasCategoriasConFiltros(filtroNombre, filtroSexo, filtroDisciplina);
            gvCategorias.DataBind();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void gvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategorias.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idCategoria = Convert.ToInt32(gvCategorias.DataKeys[index].Value);

                Session["categoria"] = idCategoria;
                Response.Redirect("CrearCategoria.aspx");
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

        protected void btn_si_Click1(object sender, EventArgs e)
        {
            if (txtIDSeleccionado.Text != "")
            {
                int idCategoria = Convert.ToInt16(txtIDSeleccionado.Text);
                string res = gc.EliminarCategoria(idCategoria);
                if (res.CompareTo("") == 0) mensaje("Se eliminó la categoría exitosamente", true);
                else mensaje(res, false);
                CargarGrilla();
                txtIDSeleccionado.Text = "";
            }
        }
    }
}