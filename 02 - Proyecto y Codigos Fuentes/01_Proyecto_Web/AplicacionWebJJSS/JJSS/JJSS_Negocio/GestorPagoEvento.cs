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
        public string registrarPago(int pParticipante, int pEvento, int pFormaPago, int pUsuario, int pInscripcion)
        {
            string sReturn = "";

            DateTime pFecha = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //validar que el participante este inscripto en ese evento
                    if (ValidarInscripcion(pParticipante, pEvento) == null)
                    {
                        return "El participante no está inscripto a ese evento";
                    }

                    //buscar alumno
                    participante participanteSelect = db.participante.Find(pParticipante);
                    //buscar clase
                    evento_especial eventoSelect = db.evento_especial.Find(pEvento);
                    //buscar forma pago
                    forma_pago formaSelect = db.forma_pago.Find(pFormaPago);

                    if (participanteSelect == null)
                    {
                        return "No existe participante";
                    }

                    if (eventoSelect == null)
                    {
                        return "No existe evento";
                    }

                    // validar que no pago antes el evento
                    if (!ValidarPago(pInscripcion))
                    {
                        return "Ya se registró este pago";
                    }

                    decimal monto;
                    if (eventoSelect.precio == null)
                    {
                        monto = 0;
                    }
                    else
                    {
                        monto = (decimal)eventoSelect.precio;
                    }
                    //crear pago
                    pago_evento nuevoPago;
                    nuevoPago = new pago_evento()
                    {
                        id_participante = pParticipante,
                        id_forma_pago = pFormaPago,
                        forma_pago = formaSelect,
                        pago_monto =monto,
                        fecha =  pFecha,
                        id_usuario = pUsuario
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
        public bool ValidarPago(int pInscripcion)
        {
            using (var db = new JJSSEntities())
            {
                var pagoEncontrado = db.pago_evento.FirstOrDefault(x => x.id_inscripcion_evento == pInscripcion);


                return pagoEncontrado != null;
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
        public inscripcion_evento ValidarInscripcion(int pParticipante, int pEvento)
        {
            GestorInscripcionesEvento inscripcion = new GestorInscripcionesEvento();

            return inscripcion.obtenerInscripcionAEventoPorIdParticipante(pParticipante, pEvento);
        }


      
    }
}
