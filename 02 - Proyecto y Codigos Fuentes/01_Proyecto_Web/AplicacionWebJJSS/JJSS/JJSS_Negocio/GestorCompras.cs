using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorCompras
    {
        private const int porcentajePrecio = 3;


        /*
         * obtiene una lista con los proveedores
         */
        public List<proveedor> ObtenerProveedores()
        {
            using (var db = new JJSSEntities())
            {
                return db.proveedor.ToList();
            }
        }


        /*
         * registra la compra del producto, ingresando idproducto, idproveedor, costo, precio venta y cantidad
         */
        public string registrarCompra(int pIDProducto, int pIDproveedor, decimal pCosto, decimal pPrecioVenta, int pCantidad)
        {
            DateTime fechaActual = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    compra nuevaCompra = new compra()
                    {
                         id_producto=pIDProducto,
                         id_proveedor=pIDproveedor,
                         precio=pCosto,
                         fecha=fechaActual,
                         cantidad=pCantidad,
                    };
                    db.compra.Add(nuevaCompra);
                    db.SaveChanges();

                    producto productoComprado = db.producto.Find(pIDProducto);
                    productoComprado.precio_venta = pPrecioVenta;
                    int? stock = productoComprado.stock;
                    if (stock == null) productoComprado.stock = pCantidad;
                    else productoComprado.stock = stock + pCantidad;
                    db.SaveChanges();


                    transaction.Commit();
                    return "";
                }catch(Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        /*
         * calcula el precio estimado segun el costo del producto
         */
        public float CalcularPrecioEstimado(float pCosto)
        {
            using (var db = new JJSSEntities())
            {
                pCosto += (float)db.parametro.Find(porcentajePrecio).valor * pCosto / 100;
                return pCosto;
            }

        }
    }
}
