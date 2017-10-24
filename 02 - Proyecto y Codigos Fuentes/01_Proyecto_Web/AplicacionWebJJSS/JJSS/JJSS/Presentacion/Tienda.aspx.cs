﻿using System;
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

        private GestorProductos gestorProductos;
        static DataTable dtItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorProductos = new GestorProductos();
                cargarClasesView();
                cargarGrillaProductos();
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

            gestorProductos = new GestorProductos();
            DataTable auxdt = gestorProductos.ObtenerProductoConID(idProducto);
            TextBox tb = (TextBox)e.Item.FindControl("txt_cantidad");
            int cantidad = int.Parse(tb.Text);
            bool aux = false;

            if (dtItems == null)
            {
                dtItems = auxdt.Clone();
                dtItems.Columns.Add("cantidad");
            }
            else
            {

                for (int i = 0; i < dtItems.Rows.Count; i++)
                {
                    DataRow dr1 = dtItems.Rows[i];
                    if (int.Parse(dr1["id_producto"].ToString()) == int.Parse(auxdt.Rows[0]["id_producto"].ToString()))
                    {
                        dr1["cantidad"] = int.Parse(dr1["cantidad"].ToString()) + cantidad;
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
            }

            cargarGrillaProductos();


        }

        protected void btn_confirmar_reserva_Click(object sender, EventArgs e)
        {
            gestorProductos = new GestorProductos();
            GestorSesiones gestorSesion = new GestorSesiones();
            seguridad_usuario alumnoActual = gestorSesion.getActual().usuario;
            int idUsuario = alumnoActual.id_usuario;
            string sReturn = gestorProductos.ConfirmarReserva(idUsuario, dtItems);
            if (sReturn.CompareTo("") == 0)
            {
                mensaje("Se reservaron los productos correctamente", true);
                dtItems = null;
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
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            dtItems = null;
            cargarGrillaProductos();
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void gv_items_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gv_items.DataKeys[index].Value);
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                for(int i= 0; i < dtItems.Rows.Count; i++)
                {
                    DataRow dr = dtItems.Rows[i];
                    if (int.Parse(dr["id_producto"].ToString()) == id) dtItems.Rows.Remove(dr);
                    break;
                }
                cargarGrillaProductos();
            }
        }
    }
}