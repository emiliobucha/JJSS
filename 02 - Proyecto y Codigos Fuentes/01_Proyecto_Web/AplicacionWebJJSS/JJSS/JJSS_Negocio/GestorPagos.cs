using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{
    public class GestorPagos
    {
       

        public List<ObjetoPagable> ObtenerObjetosPagablesPendientes(int tipoDoc, string dni)
        {
            using (var db = new JJSSEntities())
            {
                var lista = new List<ObjetoPagable>();

                var participanteTorneo =
                    db.participante.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);

                var participanteEvento =
                    db.participante_evento.FirstOrDefault(x => x.id_tipo_documento == tipoDoc && x.dni == dni);

                var inscripcionesTorneos = new List<inscripcion>();
                var inscripcionesEventos = new List<inscripcion_evento>();


                if (participanteTorneo != null)
                {
                    inscripcionesTorneos = participanteTorneo.inscripcion.ToList();
                }
                if (participanteEvento != null)
                {
                    inscripcionesEventos = participanteEvento.inscripcion_evento.ToList();
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
                        IdObjeto = (int)inscTorneo.id_torneo
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
                        montoPagable = (decimal) inscEvento.evento_especial.precio;
                    }

                    var pagable = new ObjetoPagable
                    {
                        Fecha = fechaPagable,
                        TipoPago = ConstantesTipoPago.EVENTO(),
                        Monto = montoPagable,
                        Nombre = inscEvento.evento_especial.nombre,
                        Inscripcion = inscEvento.id_inscripcion,
                        Participante = (int)inscEvento.id_participante,
                        IdObjeto = (int)inscEvento.id_evento
                    };
                    lista.Add(pagable);
                }

                return lista;

            }
        }
    }
}
