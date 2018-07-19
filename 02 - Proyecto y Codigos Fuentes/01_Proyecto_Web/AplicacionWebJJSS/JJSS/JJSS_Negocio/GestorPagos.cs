using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Resultados.Pagos;

namespace JJSS_Negocio
{
    public class GestorPagos
    {
        private static string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };


        public List<ObjetoPagable> ObtenerObjetosPagablesPendientes(int tipoDoc, string dni, bool invitado)
        {
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();

                if (invitado)
                {
                    return lista;
                }

                var participanteTorneo =
                    db.participante.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);

                var participanteEvento =
                    db.participante_evento.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);

                var alumno = db.alumno.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);

                var profesor = db.profesor.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);


                var inscripcionesTorneos = new List<inscripcion>();
                var inscripcionesEventos = new List<inscripcion_evento>();
                var inscripcionesClases = new List<inscripcion_clase>();

                var gestorClase = new GestorClases();


                if (participanteTorneo != null && (alumno!=null || profesor !=null))
                {
                    inscripcionesTorneos = participanteTorneo.inscripcion.ToList();
                }
                if (participanteEvento != null && (alumno != null || profesor != null))
                {
                    inscripcionesEventos = participanteEvento.inscripcion_evento.ToList();
                }
                if (alumno != null)
                {
                    inscripcionesClases = alumno.inscripcion_clase.ToList();
                }

                foreach (var inscTorneo in inscripcionesTorneos)
                {
                    var pago = db.pago_torneo.FirstOrDefault(x => x.id_inscripcion_torneo == inscTorneo.id_inscripcion);
                    if (pago != null) continue;
                    var fechaPagable = DateTime.Today;
                    if (inscTorneo.torneo.fecha != null)
                    {
                        fechaPagable = (DateTime)inscTorneo.torneo.fecha;
                    }
                    decimal montoPagable = 0;
                    if (inscTorneo.tipo_inscripcion != null)
                    {
                        if (inscTorneo.tipo_inscripcion == ConstantesTipoInscripcion.ABSOLUTO)
                        {
                            if (inscTorneo.torneo.precio_absoluto != null)
                            {
                                montoPagable = (decimal)inscTorneo.torneo.precio_absoluto;
                            }

                        }
                        if (inscTorneo.tipo_inscripcion == ConstantesTipoInscripcion.CATEGORIA)
                        {
                            if (inscTorneo.torneo.precio_categoria != null)
                            {
                                montoPagable = (decimal)inscTorneo.torneo.precio_categoria;
                            }

                        }
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.TORNEO(),
                        Monto = montoPagable,
                        Nombre = inscTorneo.torneo.nombre,
                        Inscripcion = inscTorneo.id_inscripcion,
                        Participante = (int)inscTorneo.id_participante,
                        NombreParticipante = inscTorneo.participante.nombre + " " + inscTorneo.participante.apellido,
                        IdObjeto = (int)inscTorneo.id_torneo,
                        DescripcionObjeto = "Tipo de Inscripción: " + (inscTorneo.id_absoluto != null ? "Absoluta" : "Categoría"),
                        MontoString = "$ " + montoPagable
                    };
                    lista.Add(pagable);
                }

                foreach (var inscEvento in inscripcionesEventos)
                {
                    var pago = db.pago_evento.FirstOrDefault(x => x.id_inscripcion_evento == inscEvento.id_inscripcion);
                    if (pago != null) continue;
                    var fechaPagable = DateTime.Today;
                    if (inscEvento.evento_especial.fecha != null)
                    {
                        fechaPagable = (DateTime)inscEvento.evento_especial.fecha;
                    }
                    decimal montoPagable = 0;
                    if (inscEvento.evento_especial.precio != null)
                    {
                        montoPagable = (decimal)inscEvento.evento_especial.precio;
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.EVENTO(),
                        Monto = montoPagable,
                        Nombre = inscEvento.evento_especial.nombre,
                        Inscripcion = inscEvento.id_inscripcion,
                        Participante = (int)inscEvento.id_participante,
                        NombreParticipante = inscEvento.participante_evento.nombre + " " + inscEvento.participante_evento.apellido,
                        IdObjeto = (int)inscEvento.id_evento,
                        MontoString = "$ " + montoPagable
                    };
                    lista.Add(pagable);
                }

                foreach (var inscClase in inscripcionesClases.Where(x => x.actual == 1 ))
                {
                    //Validaciones al vicio pero por la estructura de la base
                    if (inscClase.id_clase == null) continue;

                    if (!inscClase.proximo_vencimiento.HasValue) continue;

                    if (inscClase.clase.precio == null) continue;



                    //Si la fecha de vencimiento menos 10 dias es superior a hoy no puede pagar aun
                    if (inscClase.proximo_vencimiento.Value.AddDays(-10) > DateTime.Today) continue;

                   
                    int mes = DateTime.Today.Month;
                    string mesNombre = meses[mes - 1];


                    var pago = db.pago_clase.FirstOrDefault(x => x.id_clase == inscClase.id_clase && x.id_alumno == inscClase.id_alumno);
                    if (pago != null)
                    {
                        var detallePago =
                            db.detalle_pago_clase.FirstOrDefault(x =>
                                x.id_pago_clase == pago.id_pago_clase && x.fecha_vencimiento_cumple == inscClase.proximo_vencimiento);
                        if (detallePago != null)
                        {
                            continue;
                        }
                    }

                    var recargoParametro = db.parametro.Find(1);

                    var recargo = recargoParametro == null ? 0 : (double)recargoParametro.valor;

                    var fechaPagable = inscClase.proximo_vencimiento.Value;

                    decimal montoPagable = 0;

                    if (inscClase.recargo == 1)
                    {
                        montoPagable = (decimal)(inscClase.clase.precio + recargo);

                    }
                    else
                    {
                        montoPagable = (decimal) inscClase.clase.precio;
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.CLASE(),
                        Monto = montoPagable,
                        Nombre = inscClase.clase.nombre,
                        Inscripcion = inscClase.id_inscripcion,
                        Participante = (int)inscClase.id_alumno,
                        NombreParticipante = inscClase.alumno.nombre + " " + inscClase.alumno.apellido,
                        IdObjeto = (int)inscClase.id_clase,
                        MontoString = "$ " + montoPagable
                    };

                    if (inscClase.recargo == 1)
                    {
                        pagable.DescripcionObjeto = "Pago con recargo por demora: $" + recargo;
                    }
                    lista.Add(pagable);
                }

                return lista;

            }
        }

        public string RegistrarNuevoPagoMultiple(PagoMultiple pagoMultiple)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var gestorClase = new GestorClases();
                    var gestorPagoClase = new GestorPagoClase();

                    var nuevoPago = new pago_multiple()
                    {
                        descripcion = pagoMultiple.Descripcion,
                        dni = pagoMultiple.Dni,
                        fecha = pagoMultiple.Fecha,
                        id_forma_pago = pagoMultiple.FormaPago,
                        id_tipo_documento = pagoMultiple.TipoDocumento,
                        id_usuario = pagoMultiple.IdUsuario,
                        monto_total = pagoMultiple.MontoTotal,
                        estado_mercado_pago = pagoMultiple.EstadoMP
                    };
                    db.pago_multiple.Add(nuevoPago);
                    db.SaveChanges();

                    foreach (var objeto in pagoMultiple.ObjetosPagables)
                    {

                        if (objeto.TipoPago.Id == ConstantesTipoPago.TORNEO().Id)
                        {
                            var nuevoPagoTorneo = new pago_torneo
                            {
                                fecha = pagoMultiple.Fecha,
                                id_forma_pago = pagoMultiple.FormaPago,

                                id_participante = objeto.Participante,
                                id_inscripcion_torneo = objeto.Inscripcion,
                                id_pago_multiple = nuevoPago.id_pago,
                                pago_monto = objeto.Monto

                            };


                            if (pagoMultiple.IdUsuario != null)
                                nuevoPagoTorneo.id_usuario = (int)pagoMultiple.IdUsuario;

                            var inscripcion = db.inscripcion.Find(objeto.Inscripcion);
                            if (inscripcion != null)
                            {
                                inscripcion.pago = 1;
                            }
                           
                            db.pago_torneo.Add(nuevoPagoTorneo);
                            db.SaveChanges();
                        }

                        if (objeto.TipoPago.Id == ConstantesTipoPago.EVENTO().Id)
                        {
                            var nuevoPagoEvento = new pago_evento()
                            {
                                fecha = pagoMultiple.Fecha,
                                id_forma_pago = pagoMultiple.FormaPago,
                                id_participante = objeto.Participante,
                                id_inscripcion_evento = objeto.Inscripcion,
                                id_pago_multiple = nuevoPago.id_pago,
                                pago_monto = objeto.Monto
                            };
                            if (pagoMultiple.IdUsuario != null)
                                nuevoPagoEvento.id_usuario = (int)pagoMultiple.IdUsuario;

                            var inscripcion = db.inscripcion_evento.Find(objeto.Inscripcion);
                            if (inscripcion != null)
                            {
                                inscripcion.pago = 1;
                            }


                            db.pago_evento.Add(nuevoPagoEvento);
                            db.SaveChanges();
                        }
                        if (objeto.TipoPago.Id == ConstantesTipoPago.CLASE().Id)
                        {

                            var inscClase =
                                db.inscripcion_clase.FirstOrDefault(x => x.id_alumno == objeto.Participante && x.id_clase == objeto.IdObjeto);

                            if (inscClase == null)
                            {
                                throw new Exception("El alumno no está inscripto a esta clase");
                            }
                            if (!inscClase.proximo_vencimiento.HasValue)
                            {
                                throw new Exception("Hay un problema en la inscripción del alumno");
                            }

                            var pago = db.pago_clase.FirstOrDefault(x => x.id_alumno == objeto.Participante && x.id_clase == objeto.IdObjeto);

                            if (pago == null)
                            {
                                //crear pago
                                pago = new pago_clase()
                                {
                                    id_alumno = objeto.Participante,
                                    id_clase = objeto.IdObjeto
                                };
                                db.pago_clase.Add(pago);
                                db.SaveChanges();
                            }


                            int mes = inscClase.proximo_vencimiento.Value.Month;
                            string mesNombre = meses[mes - 1];


                            //crear detalle
                            var nuevoDetalle = new detalle_pago_clase()
                            {
                                id_pago_clase = pago.id_pago_clase,
                                monto = objeto.Monto,
                                fecha_hora = DateTime.Now,
                                mes = mesNombre,
                                fecha_vencimiento_cumple = inscClase.proximo_vencimiento,
                                fecha_desde_cumple = inscClase.fecha_desde,
                                id_forma_pago = pagoMultiple.FormaPago,
                                recargo = inscClase.recargo,
                                id_pago_multiple = nuevoPago.id_pago
                            };
                            db.detalle_pago_clase.Add(nuevoDetalle);

                            inscClase.fecha_desde = inscClase.proximo_vencimiento.Value;
                            inscClase.proximo_vencimiento = inscClase.proximo_vencimiento.Value.AddMonths(1);
                            inscClase.recargo = 0;
                            inscClase.actual = 1;

                            db.SaveChanges();


                        }
                    }


                    transaction.Commit();
                    return "";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }
            }
        }


        public List<ObjetoPagable> ObtenerUltimoEventoObjetoPagableInvitado(int tipoDoc, string dni)
        {
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();

                var participanteEvento =
                    db.participante_evento.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);

                var inscEvento = new inscripcion_evento();

                if (participanteEvento != null)
                {
                    inscEvento = participanteEvento.inscripcion_evento.Last();
                }

                var pago = db.pago_evento.FirstOrDefault(x => x.id_inscripcion_evento == inscEvento.id_inscripcion);
                if (pago != null) return new List<ObjetoPagable>();

                var fechaPagable = DateTime.Today;
                if (inscEvento.evento_especial.fecha != null)
                {
                    fechaPagable = (DateTime)inscEvento.evento_especial.fecha;
                }
            

                var pagable = new ObjetoPagable
                {
                    Fecha = fechaPagable,
                    TipoPago = ConstantesTipoPago.EVENTO(),
                    Monto = inscEvento.evento_especial.precio.Value,
                    Nombre = inscEvento.evento_especial.nombre,
                    Inscripcion = inscEvento.id_inscripcion,
                    Participante = (int)inscEvento.id_participante,
                    NombreParticipante = inscEvento.participante_evento.nombre + " " + inscEvento.participante_evento.apellido,
                    IdObjeto = (int)inscEvento.id_evento,
                    MontoString = "$ " + inscEvento.evento_especial.precio.Value
                };
                lista.Add(pagable);



                return lista;

            }
        }


        public List<ObjetoPagable> ObtenerUltimoTorneoObjetoPagableInvitado(int tipoDoc, string dni)
        {
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();

                var participanteTorneo =
                    db.participante.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);

                var inscTorneo = new inscripcion();

                if (participanteTorneo != null)
                {
                    inscTorneo = participanteTorneo.inscripcion.Last();
                }

                var pago = db.pago_torneo.FirstOrDefault(x => x.id_inscripcion_torneo == inscTorneo.id_inscripcion);
                if (pago != null) return new List<ObjetoPagable>();

                var fechaPagable = DateTime.Today;
                if (inscTorneo.torneo.fecha != null)
                {
                    fechaPagable = (DateTime)inscTorneo.torneo.fecha;
                }
                decimal montoPagable = 0;
                if (inscTorneo.tipo_inscripcion != null)
                {
                    if (inscTorneo.tipo_inscripcion == ConstantesTipoInscripcion.ABSOLUTO)
                    {
                        if (inscTorneo.torneo.precio_absoluto != null)
                        {
                            montoPagable = (decimal)inscTorneo.torneo.precio_absoluto;
                        }

                    }
                    if (inscTorneo.tipo_inscripcion == ConstantesTipoInscripcion.CATEGORIA)
                    {
                        if (inscTorneo.torneo.precio_categoria != null)
                        {
                            montoPagable = (decimal)inscTorneo.torneo.precio_categoria;
                        }

                    }
                }

                var pagable = new ObjetoPagable
                {
                    Fecha = fechaPagable,
                    TipoPago = ConstantesTipoPago.TORNEO(),
                    Monto = montoPagable,
                    Nombre = inscTorneo.torneo.nombre,
                    Inscripcion = inscTorneo.id_inscripcion,
                    Participante = (int)inscTorneo.id_participante,
                    NombreParticipante = inscTorneo.participante.nombre + " " + inscTorneo.participante.apellido,
                    IdObjeto = (int)inscTorneo.id_torneo,
                    DescripcionObjeto = "Tipo de Inscripción: " + (inscTorneo.id_absoluto != null ? "Absoluta" : "Categoría"),
                    MontoString = "$ " + montoPagable
                };
                lista.Add(pagable);



                return lista;

            }
        }

        
    }
}
