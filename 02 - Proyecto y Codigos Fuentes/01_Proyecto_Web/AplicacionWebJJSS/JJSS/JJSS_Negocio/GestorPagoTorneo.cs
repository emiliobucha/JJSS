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
        public string registrarPago(int pParticipante, int pTorneo, int pFormaPago, int pUsuario, int pInscripcion)
        {
            string sReturn = "";

            DateTime pFecha = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //validar que el participante este inscripto en ese torneo
                    if (ValidarInscripcion(pParticipante, pTorneo) == null)
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
                    if (!ValidarPago(pInscripcion))
                    {
                        return "Ya se registró este pago";
                    }

                    if (torneoSelect == null)
                    {
                        return "No existe evento";
                    }



                    decimal monto;
                    if (torneoSelect.precio_absoluto == null)
                    {
                        monto = 0;
                    }
                    else
                    {
                        monto = (decimal)torneoSelect.precio_absoluto;
                    }

                   
                    //crear pago
                    pago_torneo nuevoPago;
                    nuevoPago = new pago_torneo()
                    {
                        id_participante = pParticipante,
                        id_forma_pago = pFormaPago,
                        forma_pago = formaSelect,
                        pago_monto = monto,
                        fecha = pFecha,
                        id_usuario = pUsuario



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
        public bool ValidarPago(int pInscripcion)
        {
            using (var db = new JJSSEntities())
            {
                var pagoEncontrado = db.pago_torneo.FirstOrDefault(x => x.id_inscripcion_torneo == pInscripcion);


                return pagoEncontrado != null;
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
        public inscripcion ValidarInscripcion(int pParticipante, int pTorneo)
        {
            GestorInscripciones inscripcion = new GestorInscripciones();

            return inscripcion.obtenerInscripcionATorneoPorIdParticipante(pParticipante, pTorneo);
        }


      
    }
}
