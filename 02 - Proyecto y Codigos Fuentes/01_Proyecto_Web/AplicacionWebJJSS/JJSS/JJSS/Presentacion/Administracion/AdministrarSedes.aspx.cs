using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio.Administracion;

namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarSedes : System.Web.UI.Page
    {
        private GestorCategoria gc;

        protected void Page_Load(object sender, EventArgs e)
        {
            gc = new GestorCategoria();
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            //filtros
            gvCategorias.DataSource = gc.ObtenerTodasCategoriasConFiltros("", 0, "");
            gvCategorias.DataBind();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {

        }

        protected void gvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}