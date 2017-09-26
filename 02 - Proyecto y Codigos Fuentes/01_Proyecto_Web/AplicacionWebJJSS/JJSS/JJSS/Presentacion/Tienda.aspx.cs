using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;

namespace JJSS.Presentacion
{
    public partial class Tienda : System.Web.UI.Page
    {
        GestorProductos gestorProductos;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorProductos = new GestorProductos();
        }

        protected void cargarClasesView()
        {
            lv_tienda.DataSource = gestorProductos.ObtenerProductosTienda();
            lv_tienda.DataBind();
        }

        protected void lv_tienda_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }
    }
}