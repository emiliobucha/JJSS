using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;
using System.Globalization;

namespace JJSS_Negocio
{
    public class GestorPagoEvento
    {
        /*
         *Metodo que registra un pago
         * Parametros:
         *              pParticipante : entero que representa el id del particiapnte
         *              pEvento : entero que representa el id de la evento
         *            
         *              pFormaPago : entero que representa el id de la forma de pago
         *Retornos:     String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              El participante no está inscripto a esa evento
         *              Ya se registró este pago
         */
        public string registrarPago(int pParticipante, int pEvento, int pFormaPago)
        {
            string sReturn = "";

            DateTime pFecha = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //validar que el participante este inscripto en ese evento
                    if (validarInscripcion(pParticipante, pEvento) == null)
                    {
                        return "El participante no está inscripto a ese evento";
                    }

                    //buscar alumno
                    participante participanteSelect = db.participante.Find(pParticipante);
                    //buscar clase
                    evento_especial eventoSelect = db.evento_especial.Find(pEvento);
                    //buscar forma pago
                    forma_pago formaSelect = db.forma_pago.Find(pFormaPago);

                    // validar que no pago antes el evento
                    if (!validarPago(participanteSelect.id_participante, pEvento))
                    {
                        return "Ya se registró este pago";
                    }


                    //crear pago
                    pago_evento nuevoPago;
                    nuevoPago = new pago_evento()
                    {
                        id_participante = pParticipante,
                        id_evento = pEvento,
                        id_forma_pago = pFormaPago,
                        forma_pago = formaSelect,
                        evento_especial = eventoSelect,
                        participante = participanteSelect,
                        pago_monto = eventoSelect.precio,
                        fecha =  pFecha
                    };
                  

                    db.pago_evento.Add(nuevoPago);

                    db.SaveChanges();
                    transaction.Commit();
                    return sReturn;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        /*
         * busca si existe un pago con caracteristicas similares
         * Parametros: pAlumno : entero que representa el id del participante
         *              pClase : entero que representa el id de la evento
         *              
         * Retornos:    false si no se encontro un pago similar
         *              true si se encontro un pago similar
         * 
         */
        public bool validarPago(int pParticipante, int pEvento)
        {
            using (var db = new JJSSEntities())
            {
                var pagoEncontrado = from pago in db.pago_evento
                                     where pago.id_participante == pParticipante && pago.id_evento == pEvento

                                     select pago;


                return pagoEncontrado.ToList().Count <= 0;
            }

        }

       

        /*
         * Obtener inscripcion de un alumno a un evento
         * Parametros:
         *              pParticipante: entero que representa el id del participante
         *              pevento: entero que representa el id de la evento 
         * Retornos:Inscripcion_Evento
         *          Inscripcion del participante a dich evento pudiendo ser nulo el resultado si no estaba inscripto
         *              
         */
        public inscripcion_evento validarInscripcion(int pParticipante, int pEvento)
        {
            GestorInscripcionesEvento inscripcion = new GestorInscripcionesEvento();

            return inscripcion.obtenerInscripcionAEventoPorIdParticipante(pParticipante, pEvento);
        }


      
    }
}
