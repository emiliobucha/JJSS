using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{
    public class GestorReservas
    {
        private const int Reservada = 5;
        private const int Cancelada = 6;
        private const int Retirada = 7;

        /*
         * Metodo que reserva los productos seleccionados para un usuario
         */
        public String ConfirmarReserva(int pIDUsuario, DataTable pItems, int pIDUsuario2)
        {
            DateTime fechaActual = DateTime.Now;
            int usuarioQueReserva = pIDUsuario;
            int estado=Reservada;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    if (usuarioQueReserva == 1)//es admin
                    {
                        usuarioQueReserva = pIDUsuario2; //se le asigna la reserva otro usuario
                        estado = Retirada; // se crea la reserva como retirada
                    }
                    reserva nuevaReserva = new reserva()
                    {
                        fecha = fechaActual,
                        id_usuario = usuarioQueReserva,

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
                            cantidad = cantidad,
                        };
                        db.detalle_reserva.Add(detalle);
                        db.SaveChanges();


                        if (productoSeleccionado.stock <= 0 || productoSeleccionado.stock < cantidad) throw new Exception(productoSeleccionado.nombre + " no tiene stock suficiente");
                        else
                        {
                            productoSeleccionado.stock = productoSeleccionado.stock - cantidad;
                            db.SaveChanges();
                        }

                    }

                    estado_reserva nuevoEstado = new estado_reserva()
                    {
                        fecha = fechaActual,
                        id_estado = estado,
                        id_reserva = nuevaReserva.id_reserva,
                        actual = 1,
                    };
                    db.estado_reserva.Add(nuevoEstado);
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
         * actualiza el estado de la reserva y si hay que cancelarla se setea el stock de los productos
         */
        public string cambiarEstadoReserva(int pIDReserva, int pIDNuevoEstado)
        {
            DateTime fechaActual = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();

                try
                {
                    estado_reserva estadoActual = (from est in db.estado_reserva
                                                   where est.id_reserva == pIDReserva && est.actual == 1 && est.id_estado == Reservada
                                                   select est).FirstOrDefault();

                    estadoActual.actual = 0;
                    db.SaveChanges();

                    estado_reserva nuevoEstado = new estado_reserva()
                    {
                        fecha = fechaActual,
                        id_estado = pIDNuevoEstado,
                        id_reserva = pIDReserva,
                        actual = 1,
                    };
                    db.estado_reserva.Add(nuevoEstado);
                    db.SaveChanges();

                    if (pIDNuevoEstado == Cancelada)
                    {
                        List<detalle_reserva> detalles = (from det in db.detalle_reserva
                                                          where det.id_reserva == pIDReserva
                                                          select det).ToList();
                        foreach (detalle_reserva d in detalles)
                        {
                            int cantidad = (int)d.cantidad;
                            producto prod = db.producto.Find(d.id_producto);
                            prod.stock = prod.stock + cantidad;
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
         *  busca todas las reservas que hay
         */
        public DataTable BuscarReservas()
        {
            using (var db = new JJSSEntities())
            {
                var reservas = from res in db.reserva
                               join est in db.estado_reserva on res.id_reserva equals est.id_reserva
                               join usu in db.seguridad_usuario on res.id_usuario equals usu.id_usuario
                               join alu in db.alumno on usu.id_usuario equals alu.id_usuario


                               where est.actual == 1 && est.id_estado == Reservada
                               select new
                               {
                                   nombre = alu.nombre,
                                   apellido = alu.apellido,
                                   fecha = res.fecha,
                                   id_reserva = res.id_reserva
                               };

                var reservas2 = from res in db.reserva
                                join est in db.estado_reserva on res.id_reserva equals est.id_reserva
                                join usu in db.seguridad_usuario on res.id_usuario equals usu.id_usuario
                                join pro in db.profesor on usu.id_usuario equals pro.id_usuario


                                where est.actual == 1 && est.id_estado == Reservada
                                select new
                                {
                                    nombre = pro.nombre,
                                    apellido = pro.apellido,
                                    fecha = res.fecha,
                                    id_reserva = res.id_reserva
                                };

                DataTable dt = modUtilidadesTablas.unirDataTable(modUtilidadesTablas.ToDataTable(reservas.ToList()), modUtilidadesTablas.ToDataTable(reservas2.ToList()));

                return dt;
            }
        }

        /*
         * devuelve el detalle de la reserva pasada por parametro
         */
        public List<DetalleReservaResultado> ObtenerDetalles(int pIDReserva)
        {
            using (var db = new JJSSEntities())
            {
                var detalle = from det in db.detalle_reserva
                              join pro in db.producto on det.id_producto equals pro.id_producto
                              where det.id_reserva == pIDReserva
                              select new DetalleReservaResultado
                              {
                                  cantidad = det.cantidad,
                                  precio_venta = det.precio,
                                  nombre = pro.nombre,
                                  id_detalle = det.id_detalle_reserva,
                                  total = det.precio * det.cantidad,
                              };
                List<DetalleReservaResultado> lista = detalle.ToList<DetalleReservaResultado>();
                return lista;
            }
        }

    }
}
