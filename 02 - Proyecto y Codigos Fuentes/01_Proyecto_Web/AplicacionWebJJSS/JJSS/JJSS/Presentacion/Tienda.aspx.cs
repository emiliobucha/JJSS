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
        
        private GestorProductos gestorProductos;
        private List<int> items;

        protected void Page_Load(object sender, EventArgs e)
        {
            items = new List<int>();
            if (!IsPostBack)
            {
                gestorProductos = new GestorProductos();
                cargarClasesView();
                
            }
        }

        protected void cargarClasesView()
        {
            lv_tienda.DataSource = gestorProductos.ObtenerProductosTienda();
            lv_tienda.DataBind();
        }

        protected void lv_tienda_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int idProducto = Convert.ToInt32(e.CommandArgument);
            
            items = (List<int>)Session["items"];
            if (items ==null) items = new List<int>();
            items.Add(idProducto);
            Session["items"] = items;
        }

        protected void btn_confirmar_reserva_Click(object sender, EventArgs e)
        {
            //items.Add(1);
            //items.Add(2);
            gestorProductos = new GestorProductos();
            GestorSesiones gestorSesion = new GestorSesiones();
            seguridad_usuario alumnoActual = gestorSesion.getActual().usuario;
            int idUsuario = alumnoActual.id_usuario;
            gestorProductos.ConfirmarReserva(idUsuario, (List<int>)Session["items"]);
        }

    }
}