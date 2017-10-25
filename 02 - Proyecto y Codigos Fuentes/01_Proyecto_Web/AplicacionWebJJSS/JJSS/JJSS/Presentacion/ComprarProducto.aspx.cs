using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data;

namespace JJSS.Presentacion
{
    public partial class ComprarProducto : System.Web.UI.Page
    {
        private static GestorCompras gestorCompras;
        private static GestorProductos gestorProductos;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                gestorCompras = new GestorCompras();
                gestorProductos = new GestorProductos();
                cargarCombos();
            }

        }

        private void cargarCombos()
        {
            DataTable productos = gestorProductos.ObtenerProductos();
            ddl_producto.DataSource = productos;
            ddl_producto.DataTextField = "nombre";
            ddl_producto.DataValueField = "id_producto";
            ddl_producto.DataBind();

            ddl_proveedor.DataSource = gestorCompras.ObtenerProveedores();
            ddl_proveedor.DataValueField = "id_proveedor";
            ddl_proveedor.DataTextField = "razon_social";
            ddl_proveedor.DataBind();
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            int producto = int.Parse(ddl_producto.SelectedValue);
            int proveedor = int.Parse(ddl_proveedor.SelectedValue);
            decimal costo = decimal.Parse(txt_costo.Text);
            decimal precioVenta = decimal.Parse(txt_precio_venta.Text);
            int cantidad = int.Parse(txt_cantidad.Text);

            string result = gestorCompras.registrarCompra(producto, proveedor, costo, precioVenta, cantidad);
            if (result.CompareTo("") == 0)
            {
                limpiar();
                mensaje("Compra registrada exitosamente", true);

            }
            else
            {
                mensaje(result, false);
            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx");
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

        private void limpiar()
        {
            txt_cantidad.Text = "";
            txt_costo.Text = "";
            if (ddl_producto.Items.Count > 0) ddl_producto.SelectedIndex = 0;
            lbl_categoria.Text = "";
            lbl_precio_actual.Text = "";
            lbl_stock_actual.Text = "";
            txt_precio_venta.Text = "";
            if (ddl_proveedor.Items.Count > 0) ddl_proveedor.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

        }

        protected void txt_costo_TextChanged(object sender, EventArgs e)
        {
            //decimal precio = decimal.Parse(txt_precio.Text.Replace(".", ","));
            float costo;
            if (float.TryParse(txt_costo.Text.Replace(".", ","), out costo))
            {
                //float precio = gestorCompras.CalcularPrecioEstimado(costo);
                //txt_precio_venta.Text = precio.ToString();
                txt_precio_venta.Text = gestorCompras.CalcularPrecioEstimado(costo).ToString() + "";
            }
        }

        protected void ddl_producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosProducto();
        }

        private void CargarDatosProducto()
        {
            int id = int.Parse(ddl_producto.SelectedValue);
            DataTable prod = gestorProductos.ObtenerProductoCategoria(id);

            if (prod.Rows[0]["stock"].ToString().CompareTo("") == 0) lbl_stock_actual.Text = "S/D";
            else lbl_stock_actual.Text =  prod.Rows[0]["stock"].ToString() + " unidades";
            if (prod.Rows[0]["precio"].ToString().CompareTo("") == 0) lbl_precio_actual.Text = "S/D";
            else lbl_precio_actual.Text = prod.Rows[0]["precio"].ToString();
            lbl_categoria.Text = prod.Rows[0]["categoria"].ToString();



        }
    }
}