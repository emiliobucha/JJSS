﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;

namespace JJSS_Negocio
{
    public class GestorProductos
    {

        /*
         * Metodo que busca todas las categorias de producto
         */
        public List<categoria_producto> ObtenerCategorias()
        {
            using (var db = new JJSSEntities())
            {
                return db.categoria_producto.ToList();
            }
        }


        /*
         * Metodo que agrea un nuevo producto
         * Parametros: pIDCategoria entero categoria del producto
         *              pNombre string nombre del producto
         *              pImagen byte[] foto del producto
         */
        public string AgregarProducto(int pIDCategoria, string pNombre, byte[] pImagen)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    producto nuevoProducto = new producto()
                    {
                        nombre = pNombre,
                        id_categoria = pIDCategoria,
                        stock = 0,
                    };
                    db.producto.Add(nuevoProducto);
                    db.SaveChanges();

                    producto_imagen nuevoProductoImagen = new producto_imagen()
                    {
                        imagen = pImagen,
                        id_producto = nuevoProducto.id_producto,
                    };
                    db.producto_imagen.Add(nuevoProductoImagen);
                    db.SaveChanges();

                    transaction.Commit();
                    return "";

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        /*
         * Metodo que obtiene una lista con todos los productos y su categoria
         */
        public DataTable ObtenerProductos()
        {
            using (var db = new JJSSEntities())
            {
                var productosEncontrados = from prod in db.producto
                                           join cat in db.categoria_producto on prod.id_categoria equals cat.id_categoria
                                           orderby prod.nombre
                                           select new
                                           {
                                               nombre = prod.nombre,
                                               categoria = cat.nombre,
                                               stock = prod.stock,
                                           };
                return modUtilidadesTablas.ToDataTable(productosEncontrados.ToList());
            }
        }

        /*
        * Obtener Productos Para la Tienda
        */
        public List<Object> ObtenerProductosTienda()
        {
            using (var db = new JJSSEntities())
            {
                var productosEncontrados = from prod in db.producto
                                           join cat in db.categoria_producto on prod.id_categoria equals cat.id_categoria
                                           join img in db.producto_imagen on prod.id_producto equals img.id_producto
                                           into ps
                                           from img in ps.DefaultIfEmpty()
                                           where prod.stock > 0
                                           select new
                                           {
                                               id_producto = prod.id_producto,
                                               categoria = cat.nombre,
                                               nombre = prod.nombre,
                                               stock = prod.stock,
                                               precio = prod.precio_venta,
                                               imagen = img.imagen,
                                           };
                return productosEncontrados.ToList<Object>();
            }
        }


        /*
         * Metodo que reserva los productos seleccionados para un usuario
         */
        public String ConfirmarReserva(int pIDUsuario, DataTable pItems)
        {
            DateTime fechaActual = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    reserva nuevaReserva = new reserva()
                    {
                        fecha = fechaActual,
                        id_usuario = pIDUsuario,

                    };
                    db.reserva.Add(nuevaReserva);
                    db.SaveChanges();

                    for (int i = 0; i < pItems.Rows.Count; i++)
                    {
                        DataRow dr = pItems.Rows[i];
                        int cantidad = int.Parse(dr["cantidad"].ToString());

                        producto productoSeleccionado = db.producto.Find((int)dr["id_producto"]);


                        detalle_reserva detalle = new detalle_reserva()
                        {
                            id_producto = productoSeleccionado.id_producto,
                            precio = productoSeleccionado.precio_venta,
                            id_reserva = nuevaReserva.id_reserva,
                            cantidad =cantidad,
                        };
                        db.detalle_reserva.Add(detalle);
                        db.SaveChanges();


                        if (productoSeleccionado.stock <= 0|| productoSeleccionado.stock< cantidad) throw new Exception(productoSeleccionado.nombre+" no tiene stock suficiente");
                        else
                        {//+suponiendo que puede comprar solo uno
                            productoSeleccionado.stock= productoSeleccionado.stock- cantidad;
                            db.SaveChanges();
                        }

                    }
                    transaction.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        /*
         * Devuelve un Datatable con los datos del producto cuyo id se pasa por parametro
         */
        public DataTable ObtenerProductoConID(int pIdProducto)
        {
            using (var db = new JJSSEntities())
            {
                var prod = from pr in db.producto
                           where pr.id_producto == pIdProducto
                           select pr;

                return modUtilidadesTablas.ToDataTable(prod.ToList());
            }
        }
    }
}