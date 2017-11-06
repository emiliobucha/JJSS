using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;
using System.Data;

namespace JJSS.Presentacion
{
    public partial class Tienda : System.Web.UI.Page
    {
        private static GestorReservas gestorReservas;
        private static GestorProductos gestorProductos;
        private static GestorUsuarios gestorUsuarios;
        private static GestorSesiones gestorSesion;
        private static DataTable dtItems;
        private static int _idUsuarioSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorProductos = new GestorProductos();
                gestorReservas = new GestorReservas();
                gestorUsuarios = new GestorUsuarios();
                gestorSesion = new GestorSesiones();
                cargarClasesView();
                cargarGrillaProductos();

                _idUsuarioSeleccionado = 0;

                seguridad_usuario alumnoActual = gestorSesion.getActual().usuario;
                int idUsuario = alumnoActual.id_usuario;
                if (idUsuario == 1)
                {
                    pnl_usuarios.Visible = true;
                    cargarGrillaUsuarios();
                }
                else pnl_usuarios.Visible = false;
            }
        }

        protected void cargarClasesView()
        {
            lv_tienda.DataSource = gestorProductos.ObtenerProductosTienda();
            lv_tienda.DataBind();
        }

        private void cargarGrillaProductos()
        {
            gv_items.DataSource = dtItems;
            gv_items.DataBind();
            if (dtItems == null)
            {
                btn_confirmar_reserva.Visible = false;
                btn_limpiar.Visible = false;
            }
            else
            {
                btn_confirmar_reserva.Visible = true;
                btn_limpiar.Visible = true;
            }
        }

        protected void lv_tienda_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            int idProducto = Convert.ToInt32(e.CommandArgument);

            DataTable auxdt = gestorProductos.ObtenerProductoConID(idProducto);
            TextBox tb = (TextBox)e.Item.FindControl("txt_cantidad");
            int cantidad = int.Parse(tb.Text);
            bool aux = false;

            if (dtItems == null)
            {
                dtItems = auxdt.Clone();
                dtItems.Columns.Add("cantidad");
                dtItems.Columns.Add("total");
            }
            else
            {

                for (int i = 0; i < dtItems.Rows.Count; i++)
                {
                    DataRow dr1 = dtItems.Rows[i];
                    if (int.Parse(dr1["id_producto"].ToString()) == int.Parse(auxdt.Rows[0]["id_producto"].ToString()))
                    {
                        dr1["cantidad"] = int.Parse(dr1["cantidad"].ToString()) + cantidad;
                        dr1["total"] = int.Parse(dr1["cantidad"].ToString()) * decimal.Parse(dr1["precio_venta"].ToString());
                        aux = true;
                        break;
                    }

                }
            }
            if (!aux)
            {
                dtItems.ImportRow(auxdt.Rows[0]);
                DataRow dr = dtItems.Rows[dtItems.Rows.Count - 1];
                dr["cantidad"] = cantidad;
                dr["total"] = int.Parse(dr["cantidad"].ToString()) * decimal.Parse(dr["precio_venta"].ToString());
            }

            cargarGrillaProductos();

            decimal total = 0;
            for (int i = 0; i < dtItems.Rows.Count; i++)
            {
                DataRow dr = dtItems.Rows[i];
                total += decimal.Parse(dr["total"].ToString());
            }
            lbl_total.Text = total+"";
        }

        protected void btn_confirmar_reserva_Click(object sender, EventArgs e)
        {

            seguridad_usuario alumnoActual = gestorSesion.getActual().usuario;
            int idUsuario = alumnoActual.id_usuario;
            string sReturn = gestorReservas.ConfirmarReserva(idUsuario, dtItems, _idUsuarioSeleccionado);
            if (sReturn.CompareTo("") == 0)
            {
                mensaje("La reserva se realizó correctamente", true);
                dtItems = null;
                lbl_total.Text = 0+"";
                cargarGrillaProductos();
                cargarClasesView();
            }
            else mensaje(sReturn, false);
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

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            dtItems = null;
            cargarGrillaProductos();
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            _idUsuarioSeleccionado = 0;
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            dtItems = null;
            cargarGrillaProductos();
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            _idUsuarioSeleccionado = 0;
            Response.Redirect("../Presentacion/Inicio.aspx");

        }

        protected void gv_items_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gv_items.DataKeys[index].Value);
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                for (int i = 0; i < dtItems.Rows.Count; i++)
                {
                    DataRow dr = dtItems.Rows[i];
                    if (int.Parse(dr["id_producto"].ToString()) == id) dtItems.Rows.Remove(dr);
                    break;
                }
                cargarGrillaProductos();
            }
        }

        protected void gv_usuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gv_usuarios.DataKeys[index].Value);

            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                _idUsuarioSeleccionado = id;
            }
        }

        protected void gv_usuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_usuarios.PageIndex = e.NewPageIndex;
            cargarGrillaUsuarios();
        }

        private void cargarGrillaUsuarios()
        {
            DataTable dtCompleta = gestorUsuarios.BuscarProfesAlumnos();
            DataTable dtConFiltro;

            dtConFiltro = dtCompleta.Clone();
            string filtroApellido = txt_filtro_apellido.Text.ToUpper().Trim();

            for (int i = 0; i < dtCompleta.Rows.Count; i++)
            {
                DataRow dr = dtCompleta.Rows[i];
                if (dr["apellido"].ToString().ToUpper().StartsWith(filtroApellido))
                {
                    dtConFiltro.ImportRow(dr);
                };
            }
            gv_usuarios.DataSource = dtConFiltro;
            gv_usuarios.DataBind();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrillaUsuarios();
        }
    }
}