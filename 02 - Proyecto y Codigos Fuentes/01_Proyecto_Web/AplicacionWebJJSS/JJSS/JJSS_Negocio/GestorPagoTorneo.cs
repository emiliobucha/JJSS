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
    public class GestorPagoTorneo
    {
        /*
         *Metodo que registra un pago
         * Parametros:
         *              pParticipante : entero que representa el id del particiapnte
         *              pTorneo : entero que representa el id de la torneo
         *            
         *              pFormaPago : entero que representa el id de la forma de pago
         *Retornos:     String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              El participante no está inscripto a esa torneo
         *              Ya se registró este pago
         */
        public string registrarPago(int pParticipante, int pTorneo, int pFormaPago)
        {
            string sReturn = "";

            DateTime pFecha = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //validar que el participante este inscripto en ese torneo
                    if (validarInscripcion(pParticipante, pTorneo) == null)
                    {
                        return "El participante no está inscripto a ese torneo";
                    }

                    //buscar alumno
                    participante participanteSelect = db.participante.Find(pParticipante);
                    //buscar clase
                    torneo torneoSelect = db.torneo.Find(pTorneo);
                    //buscar forma pago
                    forma_pago formaSelect = db.forma_pago.Find(pFormaPago);

                    // validar que no pago antes el torneo
                    if (!validarPago(participanteSelect.id_participante, pTorneo))
                    {
                        return "Ya se registró este pago";
                    }


                    //crear pago
                    pago_torneo nuevoPago;
                    nuevoPago = new pago_torneo()
                    {
                        id_participante = pParticipante,
                        id_torneo = pTorneo,
                        id_forma_pago = pFormaPago,
                        forma_pago = formaSelect,
                        torneo = torneoSelect,
                        participante = participanteSelect,
                        pago_monto = torneoSelect.precio_absoluto,
                        fecha =  pFecha
                       
                                    
                        
                    };
                  

                    db.pago_torneo.Add(nuevoPago);

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
         *              pClase : entero que representa el id de la torneo
         *              
         * Retornos:    false si no se encontro un pago similar
         *              true si se encontro un pago similar
         * 
         */
        public bool validarPago(int pParticipante, int pTorneo)
        {
            using (var db = new JJSSEntities())
            {
                var pagoEncontrado = from pago in db.pago_torneo
                                     where pago.id_participante == pParticipante && pago.id_torneo == pTorneo

                                     select pago;


                return pagoEncontrado.ToList().Count <= 0;
            }

        }

       

        /*
         * Obtener inscripcion de un alumno a un torneo
         * Parametros:
         *              pParticipante: entero que representa el id del participante
         *              ptorneo: entero que representa el id de la torneo 
         * Retornos:Inscripcion_Torneo
         *          Inscripcion del participante a dich torneo pudiendo ser nulo el resultado si no estaba inscripto
         *              
         */
        public inscripcion validarInscripcion(int pParticipante, int pTorneo)
        {
            GestorInscripciones inscripcion = new GestorInscripciones();

            return inscripcion.obtenerInscripcionATorneoPorIdParticipante(pParticipante, pTorneo);
        }


      
    }
}
