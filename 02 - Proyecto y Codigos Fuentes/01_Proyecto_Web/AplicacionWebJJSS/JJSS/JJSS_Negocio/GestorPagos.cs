using System;
using System.Collections.Generic;
using System.Data.Entity;
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


                if (participanteTorneo != null && (alumno != null || profesor != null))
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
                        MontoString = "$ " + montoPagable.ToString("N2")
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
                        MontoString = "$ " + montoPagable.ToString("N2")

                    };
                    lista.Add(pagable);
                }

                foreach (var inscClase in inscripcionesClases.Where(x => x.actual == 1))
                {
                    //Validaciones al vicio pero por la estructura de la base
                    if (inscClase.id_clase == null) continue;

                    if (!inscClase.fecha_vencimiento.HasValue) continue;

                    if (!inscClase.fecha_desde.HasValue) continue;

                    if (inscClase.clase.precio == null) continue;

                    var pago = inscClase.pago_clase.FirstOrDefault();


                    var recargoParametro = db.parametro.Find(1);

                    var recargo = recargoParametro == null ? 0 : (double)recargoParametro.valor;

                    //Generamos un pago para el periodo siguiente si estamos a 10 dias de vencimiento del periodo
                    if (inscClase.fecha_vencimiento.Value.AddDays(-10) <= DateTime.Today && pago != null)
                    {
                        var fechaPagable = inscClase.fecha_vencimiento.Value.AddDays(10);

                        var montoPagable = (decimal)inscClase.clase.precio;

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
                            MontoString = "$ " + montoPagable.ToString("N2"),
                            MesSiguiente = true
                        };

                        lista.Add(pagable);
                    }
                    else if (inscClase.provisoria == 1 && pago == null)
                    {
                        var fechaPagable = inscClase.fecha_desde.Value.AddDays(10);

                        decimal montoPagable;

                        if (inscClase.recargo == 1)
                        {
                            montoPagable = (decimal)(inscClase.clase.precio + recargo);

                        }
                        else
                        {
                            montoPagable = (decimal)inscClase.clase.precio;
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
                            MontoString = "$ " + montoPagable.ToString("N2")
                        };

                        lista.Add(pagable);
                    }

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
                                db.inscripcion_clase.Find(objeto.Inscripcion);

                            if (inscClase == null)
                            {
                                throw new Exception("El alumno no está inscripto a esta clase para ese periodo a pagar");
                            }
                            if (!inscClase.fecha_vencimiento.HasValue)
                            {
                                throw new Exception("Hay un problema en la inscripción del alumno");
                            }

                            var pago = db.pago_clase.FirstOrDefault(x => x.id_inscripcion_clase == inscClase.id_inscripcion);

                            if (pago != null)
                            {
                                throw new Exception("Ya hay un pago de esa inscripción");
                            }

                            //crear pago
                            var nuevoPagoClase = new pago_clase()
                            {
                                monto = objeto.Monto,
                                fecha_hora = DateTime.Now,
                                fecha_vencimiento_cumple = inscClase.fecha_vencimiento,
                                fecha_desde_cumple = inscClase.fecha_desde,
                                id_forma_pago = pagoMultiple.FormaPago,
                                recargo = inscClase.recargo,
                                id_pago_multiple = nuevoPago.id_pago,
                                id_inscripcion_clase = objeto.Inscripcion
                            };
                            db.pago_clase.Add(nuevoPagoClase);

                            inscClase.recargo = 0;
                            short esActual = 1;
                            if (inscClase.fecha_vencimiento < DateTime.Now) esActual = 0;
                            inscClase.actual = esActual;
                            inscClase.provisoria = 0;
                            inscClase.moroso_si = 0;

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
                    MontoString = "$ " + inscEvento.evento_especial.precio.Value.ToString("N2")
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
                    MontoString = "$ " + montoPagable.ToString("N2")
                };
                lista.Add(pagable);



                return lista;

            }
        }

        public List<ObjetoPagable> ObtenerObjetosPagablesHistoricos(int tipoDoc, string dni)
        {
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();


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


                if (participanteTorneo != null && (alumno != null || profesor != null))
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

                    if (pago == null)
                    {
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
                            MontoString = "$ " + montoPagable.ToString("N2"),
                            Pago = 0,
                            PagoString = "No"
                        };
                        lista.Add(pagable);
                    }
                    else
                    {
                        var fechaPagable = DateTime.Today;
                        if (inscTorneo.torneo.fecha != null)
                        {
                            fechaPagable = (DateTime)inscTorneo.torneo.fecha;
                        }

                        var pagable = new ObjetoPagable
                        {
                            Fecha = fechaPagable,
                            TipoPago = ConstantesTipoPago.TORNEO(),
                            Monto = pago.pago_monto,
                            Nombre = inscTorneo.torneo.nombre,
                            Inscripcion = inscTorneo.id_inscripcion,
                            Participante = (int)inscTorneo.id_participante,
                            NombreParticipante = inscTorneo.participante.nombre + " " + inscTorneo.participante.apellido,
                            IdObjeto = (int)inscTorneo.id_torneo,
                            DescripcionObjeto = "Tipo de Inscripción: " + (inscTorneo.id_absoluto != null ? "Absoluta" : "Categoría"),
                            MontoString = "$ " + pago.pago_monto.ToString("N2"),
                            FechaPago = pago.fecha,
                            Pago = 1,
                            PagoString = "Si"
                        };
                        lista.Add(pagable);



                    }
                }

                foreach (var inscEvento in inscripcionesEventos)
                {
                    var pago = db.pago_evento.FirstOrDefault(x => x.id_inscripcion_evento == inscEvento.id_inscripcion);
                    if (pago == null)
                    {
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
                            NombreParticipante = inscEvento.participante_evento.nombre + " " +
                                                 inscEvento.participante_evento.apellido,
                            IdObjeto = (int)inscEvento.id_evento,
                            MontoString = "$ " + montoPagable.ToString("N2"),
                            Pago = 0,
                            PagoString = "N"
                        };
                        lista.Add(pagable);
                    }
                    else
                    {

                        var fechaPagable = DateTime.Today;
                        if (inscEvento.evento_especial.fecha != null)
                        {
                            fechaPagable = (DateTime)inscEvento.evento_especial.fecha;
                        }


                        var pagable = new ObjetoPagable
                        {
                            Fecha = fechaPagable,
                            TipoPago = ConstantesTipoPago.EVENTO(),
                            Monto = pago.pago_monto,
                            Nombre = inscEvento.evento_especial.nombre,
                            Inscripcion = inscEvento.id_inscripcion,
                            Participante = (int)inscEvento.id_participante,
                            NombreParticipante = inscEvento.participante_evento.nombre + " " +
                                                 inscEvento.participante_evento.apellido,
                            IdObjeto = (int)inscEvento.id_evento,
                            MontoString = "$ " + pago.pago_monto.ToString("N2"),
                            FechaPago = pago.fecha,
                            Pago = 1,
                            PagoString = "Si"

                        };
                        lista.Add(pagable);
                    }
                }

                //Clases sin Pagar
                foreach (var inscClase in inscripcionesClases.Where(x => x.actual == 1))
                {
                    //Validaciones al vicio pero por la estructura de la base
                    if (inscClase.id_clase == null) continue;

                    if (!inscClase.fecha_vencimiento.HasValue) continue;

                    if (inscClase.clase.precio == null) continue;


                    var pago = inscClase.pago_clase;
                    if (pago != null)
                    {

                        continue;

                    }

                    var recargoParametro = db.parametro.Find(1);

                    var recargo = recargoParametro == null ? 0 : (double)recargoParametro.valor;

                    var fechaPagable = inscClase.fecha_vencimiento.Value;

                    decimal montoPagable = 0;

                    if (inscClase.recargo == 1)
                    {
                        montoPagable = (decimal)(inscClase.clase.precio + recargo);

                    }
                    else
                    {
                        montoPagable = (decimal)inscClase.clase.precio;
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
                        MontoString = "$ " + montoPagable.ToString("N2"),
                        Pago = 0,
                        PagoString = "No"
                    };

                    if (inscClase.recargo == 1)
                    {
                        pagable.DescripcionObjeto = "Pago con recargo por demora: $" + recargo;
                    }
                    lista.Add(pagable);
                }

                //Clases pagadas
                foreach (var inscClase in inscripcionesClases)
                {
                    //Validaciones al vicio pero por la estructura de la base
                    if (inscClase.id_clase == null) continue;

                    if (!inscClase.fecha_vencimiento.HasValue) continue;

                    if (!inscClase.fecha_desde.HasValue) continue;

                    if (inscClase.clase.precio == null) continue;

                    var pago = inscClase.pago_clase.FirstOrDefault();


                    var recargoParametro = db.parametro.Find(1);

                    var recargo = recargoParametro == null ? 0 : (double) recargoParametro.valor;

                    //Generamos un pago para el periodo siguiente si estamos a 10 dias de vencimiento del periodo
                    if (inscClase.fecha_vencimiento.Value.AddDays(-10) <= DateTime.Today && pago != null)
                    {
                        var fechaPagable = inscClase.fecha_vencimiento.Value.AddDays(10);

                        var montoPagable = (decimal) inscClase.clase.precio;

                        var pagable = new ObjetoPagable
                        {
                            Fecha = fechaPagable,
                            TipoPago = ConstantesTipoPago.CLASE(),
                            Monto = montoPagable,
                            Nombre = inscClase.clase.nombre,
                            Inscripcion = inscClase.id_inscripcion,
                            Participante = (int) inscClase.id_alumno,
                            NombreParticipante = inscClase.alumno.nombre + " " + inscClase.alumno.apellido,
                            IdObjeto = (int) inscClase.id_clase,
                            MontoString = "$ " + montoPagable.ToString("N2"),
                            MesSiguiente = true
                        };

                        lista.Add(pagable);
                    }
                    else if (inscClase.provisoria == 1 && pago == null)
                    {
                        var fechaPagable = inscClase.fecha_desde.Value.AddDays(10);

                        decimal montoPagable;

                        if (inscClase.recargo == 1)
                        {
                            montoPagable = (decimal) (inscClase.clase.precio + recargo);

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
                            Participante = (int) inscClase.id_alumno,
                            NombreParticipante = inscClase.alumno.nombre + " " + inscClase.alumno.apellido,
                            IdObjeto = (int) inscClase.id_clase,
                            MontoString = "$ " + montoPagable.ToString("N2")
                        };

                        lista.Add(pagable);
                    }
                }
                return lista.OrderBy(x => x.Fecha).ToList();

            }
        }

        //public string AnularPagoClase()

        /*Pagos del mes*/
        public List<ObjetoPagable> ObtenerObjetosPagablesIntervaloPendientes(DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();
                DateTime fechaActual = DateTime.Now;

                //Sin pagar 
                var inscripcionesTorneoSp = db.inscripcion.Where(x =>
                    x.participante.id_alumno != null);
                var inscripcionesEventosSp = db.inscripcion_evento.Where(x =>
                    x.participante_evento.id_alumno != null);
                var inscripcionesClases = db.inscripcion_clase.Where(x => x.actual == 1);


                foreach (var inscTorneo in inscripcionesTorneoSp)
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
                        NombreParticipante = inscTorneo.participante.apellido + ", " + inscTorneo.participante.nombre,
                        IdObjeto = (int)inscTorneo.id_torneo,
                        DescripcionObjeto = "Tipo de Inscripción: " +
                                            (inscTorneo.id_absoluto != null ? "Absoluta" : "Categoría"),
                        MontoString = "$ " + montoPagable.ToString("N2"),
                        TipoDocumento = inscTorneo.participante.tipo_documento.codigo,
                        Numero = inscTorneo.participante.dni
                    };
                    lista.Add(pagable);
                }

                foreach (var inscEvento in inscripcionesEventosSp)
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
                        NombreParticipante = inscEvento.participante_evento.apellido + ", " +
                                             inscEvento.participante_evento.nombre,
                        IdObjeto = (int)inscEvento.id_evento,
                        MontoString = "$ " + montoPagable.ToString("N2"),
                        TipoDocumento = inscEvento.participante_evento.tipo_documento.codigo,
                        Numero = inscEvento.participante_evento.dni
                    };
                    lista.Add(pagable);
                }

                foreach (var inscClase in inscripcionesClases)
                {
                    //Validaciones al vicio pero por la estructura de la base
                    if (inscClase.id_clase == null) continue;

                    if (!inscClase.fecha_vencimiento.HasValue) continue;

                    if (inscClase.clase.precio == null) continue;


                    var pago = inscClase.pago_clase;
                    if (pago != null && pago.Count > 0)
                    {
                        continue;

                    }

                    var recargoParametro = db.parametro.Find(1);

                    var recargo = recargoParametro == null ? 0 : (double)recargoParametro.valor;

                    //Generamos un pago para el periodo siguiente si estamos a 10 dias de vencimiento del periodo
                    if (inscClase.fecha_vencimiento.Value.AddDays(-10) <= DateTime.Today && pago != null)
                    {
                        var fechaPagable = inscClase.fecha_vencimiento.Value.AddDays(10);
                        var montoPagable = (decimal)inscClase.clase.precio;

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
                            MontoString = "$ " + montoPagable.ToString("N2"),
                            MesSiguiente = true,
                            TipoDocumento = inscClase.alumno.tipo_documento.codigo,
                            Numero = inscClase.alumno.dni
                        };

                        lista.Add(pagable);
                    }
                    else if (inscClase.provisoria == 1 && pago == null)
                    {
                        var fechaPagable = inscClase.fecha_desde.Value.AddDays(10);

                        decimal montoPagable;

                        if (inscClase.recargo == 1)
                        {
                            montoPagable = (decimal)(inscClase.clase.precio + recargo);
                        }
                        else
                        {
                            montoPagable = (decimal)inscClase.clase.precio;
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
                            MontoString = "$ " + montoPagable.ToString("N2"),
                            TipoDocumento = inscClase.alumno.tipo_documento.codigo,
                            Numero = inscClase.alumno.dni
                        };

                        if (inscClase.recargo == 1)
                        {
                            pagable.DescripcionObjeto = "Pago con recargo por demora";
                        }
                        lista.Add(pagable);
                    }                    
                }
                return lista.OrderBy(x => x.Fecha).ToList();
            }
        }

        public List<ObjetoPagable> ObtenerObjetosPagablesPagados(DateTime fechaDesde, DateTime fechaHasta)
        {
            //Pagados
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();
                var desde = fechaDesde.Date;
                var hasta = fechaHasta.Date;

                var pagosTorneos = db.pago_torneo.Where(x =>
                    DbFunctions.TruncateTime(x.fecha) >= desde &&
                    DbFunctions.TruncateTime(x.fecha) <= hasta);

                var pagosEventos = db.pago_evento.Where(x =>
                    DbFunctions.TruncateTime(x.fecha) >= desde &&
                    DbFunctions.TruncateTime(x.fecha) <= hasta);

                var pagoClases = db.pago_clase.Where(x =>
                    DbFunctions.TruncateTime(x.fecha_hora) >= desde &&
                    DbFunctions.TruncateTime(x.fecha_hora) <= hasta);

                foreach (var pago in pagosTorneos)
                {
                    var fechaPagable = DateTime.Today;
                    if (pago.inscripcion?.torneo?.fecha != null)
                    {
                        fechaPagable = (DateTime)pago.inscripcion.torneo.fecha;
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.TORNEO(),
                        Monto = pago.pago_monto,
                        Nombre = pago.inscripcion?.torneo?.nombre,
                        Inscripcion = pago.id_inscripcion_torneo,
                        Participante = pago.id_participante,
                        NombreParticipante = pago.participante.apellido + ", " +
                                             pago.participante.nombre,
                        IdObjeto = pago.inscripcion?.id_torneo ?? 0,
                        DescripcionObjeto = "Tipo de Inscripción: " + (pago.inscripcion?.id_absoluto != null ? "Absoluta" : "Categoría"),
                        MontoString = "$ " + pago.pago_monto.ToString("N2"),
                        FechaPago = pago.fecha,
                        Pago = 1,
                        PagoString = "Si",
                        TipoDocumento = pago.participante.tipo_documento.codigo,
                        Numero = pago.participante.dni

                    };

                    lista.Add(pagable);

                }

                foreach (var pago in pagosEventos)
                {
                    var fechaPagable = DateTime.Today;
                    if (pago.inscripcion_evento?.evento_especial?.fecha != null)
                    {
                        fechaPagable = (DateTime)pago.inscripcion_evento.evento_especial.fecha;
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.EVENTO(),
                        Monto = pago.pago_monto,
                        Nombre = pago.inscripcion_evento?.evento_especial?.nombre,
                        Inscripcion = pago.id_inscripcion_evento,
                        Participante = pago.id_participante,
                        NombreParticipante = pago.participante_evento.apellido + ", " +
                                             pago.participante_evento.nombre,
                        IdObjeto = pago.inscripcion_evento?.id_evento ?? 0,
                        MontoString = "$ " + pago.pago_monto.ToString("N2"),
                        FechaPago = pago.fecha,
                        Pago = 1,
                        PagoString = "Si",
                        TipoDocumento = pago.participante_evento.tipo_documento.codigo,
                        Numero = pago.participante_evento.dni

                    };
                    lista.Add(pagable);

                }

                foreach (var detalle in pagoClases)
                {
                    var fechaPagable = DateTime.Today;
                    if (detalle.fecha_vencimiento_cumple != null)
                    {
                        fechaPagable = (DateTime)detalle.fecha_vencimiento_cumple;
                    }


                    var inscripcion = detalle.inscripcion_clase;

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.CLASE(),
                        Monto = detalle.monto ?? 0,
                        Nombre = inscripcion.clase.nombre,
                        Inscripcion = inscripcion?.id_inscripcion ?? 0,
                        Participante = inscripcion.id_alumno ?? 0,
                        NombreParticipante = inscripcion.alumno.apellido + ", " +
                                             inscripcion.alumno.nombre,
                        IdObjeto = inscripcion.id_clase ?? 0,
                        MontoString = "$ " + detalle.monto.Value.ToString("N2"),
                        FechaPago = detalle.fecha_hora,
                        Pago = 1,
                        PagoString = "Si",
                        TipoDocumento = inscripcion.alumno.tipo_documento.codigo,
                        Numero = inscripcion.alumno.dni

                    };

                    lista.Add(pagable);
                }

                return lista.OrderBy(x => x.FechaPago).ThenBy(x => x.Fecha).ToList();
            }

        }


        public List<ObjetoPagable> ObtenerObjetosPagablesPagadosPorAlumno(DateTime fechaDesde, DateTime fechaHasta, int idAlumno)
        {
            //Pagados
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();
                var desde = fechaDesde.Date;
                var hasta = fechaHasta.Date;

                var pagosTorneos = db.pago_torneo.Where(x =>
                    DbFunctions.TruncateTime(x.fecha) >= desde &&
                    DbFunctions.TruncateTime(x.fecha) <= hasta && x.inscripcion.participante.id_alumno == idAlumno);

                var pagosEventos = db.pago_evento.Where(x =>
                    DbFunctions.TruncateTime(x.fecha) >= desde &&
                    DbFunctions.TruncateTime(x.fecha) <= hasta && x.inscripcion_evento.participante_evento.id_alumno == idAlumno);

                var pagoClases = db.pago_clase.Where(x =>
                    DbFunctions.TruncateTime(x.fecha_hora) >= desde &&
                    DbFunctions.TruncateTime(x.fecha_hora) <= hasta && x.inscripcion_clase.id_alumno == idAlumno);

                foreach (var pago in pagosTorneos)
                {
                    var fechaPagable = DateTime.Today;
                    if (pago.inscripcion?.torneo?.fecha != null)
                    {
                        fechaPagable = (DateTime)pago.inscripcion.torneo.fecha;
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.TORNEO(),
                        Monto = pago.pago_monto,
                        Nombre = pago.inscripcion?.torneo?.nombre,
                        Inscripcion = pago.id_inscripcion_torneo,
                        IdObjeto = pago.inscripcion?.id_torneo ?? 0,
                        DescripcionObjeto = "Tipo de Inscripción: " + (pago.inscripcion?.id_absoluto != null ? "Absoluta" : "Categoría"),
                        MontoString = "$ " + pago.pago_monto.ToString("N2"),
                        FechaPago = pago.fecha,

                    };

                    lista.Add(pagable);

                }

                foreach (var pago in pagosEventos)
                {
                    var fechaPagable = DateTime.Today;
                    if (pago.inscripcion_evento?.evento_especial?.fecha != null)
                    {
                        fechaPagable = (DateTime)pago.inscripcion_evento.evento_especial.fecha;
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.EVENTO(),
                        Monto = pago.pago_monto,
                        Nombre = pago.inscripcion_evento?.evento_especial?.nombre,
                        Inscripcion = pago.id_inscripcion_evento,
                        IdObjeto = pago.inscripcion_evento?.id_evento ?? 0,
                        MontoString = "$ " + pago.pago_monto.ToString("N2"),
                        FechaPago = pago.fecha,

                    };
                    lista.Add(pagable);

                }

                foreach (var detalle in pagoClases)
                {
                    var fechaPagable = DateTime.Today;
                    if (detalle.fecha_vencimiento_cumple != null)
                    {
                        fechaPagable = (DateTime)detalle.fecha_vencimiento_cumple;
                    }


                    var inscripcion = detalle.inscripcion_clase;

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.CLASE(),
                        Monto = detalle.monto ?? 0,
                        Nombre = inscripcion.clase.nombre,
                        Inscripcion = inscripcion?.id_inscripcion ?? 0,
                        IdObjeto = inscripcion.id_clase ?? 0,
                        MontoString = "$ " + detalle.monto.Value.ToString("N2"),
                        FechaPago = detalle.fecha_hora,

                    };

                    lista.Add(pagable);
                }

                return lista.OrderBy(x => x.FechaPago).ThenBy(x => x.Fecha).ToList();
            }

        }
    }
}
