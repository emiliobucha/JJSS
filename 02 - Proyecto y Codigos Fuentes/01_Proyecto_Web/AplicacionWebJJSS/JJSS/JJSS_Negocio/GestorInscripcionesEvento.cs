using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorInscripcionesEvento
    {

        /*
         * Método que busca un alumno por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */
        public alumno ObtenerAlumnoPorDNI(string pDni)
        {
            GestorAlumnos gestorAlumnos = new GestorAlumnos();

            return gestorAlumnos.ObtenerAlumnoPorDNI(pDni);
        }

        /*Método que permite crear un objeto de Entidad de la clase Inscripción
         * Como asi tambien genera una nueva categoria si esta no estaba 
         * Y si el participante no estaba ya inscripto
         * 
         * Parametros: 
         *              pTorneo : Entero id del torneo a inscribir
         *              pNombre : String nombre del participante
         *              pApellido : String apellido del participante
         *              pPeso : Float peso del participante
         *              pFechaNacimiento : DateTime fecha de nacimiento del participante
         *              pFaja : Entero id de la faja que posee el participante
         *              pSexo : Short 0 Mujer 1 Hombre
         *              pDni : 
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              Participante ya inscripto
         *          
         * 
         */
        public string InscribirAEvento(int pEvento, string pNombre, string pApellido, DateTime pFechaNacimiento, short pSexo, int pDni, int? pIDAlumno)
        {

            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //Foraneos
                    evento_especial eventoInscripto = db.evento_especial.Find(pEvento);

                    int edad = DateTime.Today.AddTicks(-pFechaNacimiento.Ticks).Year - 1;

                    //Nuevos


                    if (obtenerParticipanteEvento(pDni, pEvento) != null)
                    {
                        return "Participante exitente";
                    }

                    //alumnoExistente = ObtenerAlumnoPorDNI(pDni);

                    participante_evento nuevoParticipante;

                    nuevoParticipante = new participante_evento()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        //peso = pPeso,

                        sexo = pSexo,
                        fecha_nacimiento = pFechaNacimiento,
                        dni = pDni,
                        id_alumno = pIDAlumno

                    };
                    db.participante_evento.Add(nuevoParticipante);
                    db.SaveChanges();

                    string hora = hora = DateTime.Now.ToString("hh:mm tt");
                    DateTime fecha = DateTime.Now.Date;
                    inscripcion_evento nuevaInscripcion = new inscripcion_evento()
                    {

                        hora = hora,
                        fecha = fecha,

                        id_participante = nuevoParticipante.id_participante,
                        id_evento = eventoInscripto.id_evento



                    };



                    db.inscripcion_evento.Add(nuevaInscripcion);
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
         * Metodo que nos permite bajar el acoplamiente y obtener los diferentes torneos para el momento de inscribirse
         */
        public List<evento_especial> ObtenerEventos()
        {
            GestorEventos gestorEventos = new GestorEventos();
            return gestorEventos.ObtenerEventos();
        }

        public participante_evento obtenerParticipanteEvento(int pDni, int pIDEvento)
        {
            using (var db = new JJSSEntities())
            {
                participante_evento participante = (from part in db.participante_evento
                                                    join ins in db.inscripcion_evento on part.id_participante equals ins.id_participante
                                                    where part.dni == pDni && ins.id_evento == pIDEvento
                                                    select part).FirstOrDefault();
                return participante;
            }
        }

        public inscripcion_evento obtenerInscripcionAEventoPorIdParticipante(int pId, int pIdEvento)
        {
            using (var db = new JJSSEntities())
            {
                inscripcion_evento inscripcion = (from ins in db.inscripcion_evento
                                                  join part in db.participante_evento on ins.id_participante equals part.id_participante
                                                  where part.id_participante == pId && ins.id_evento == pIdEvento
                                                  select ins).FirstOrDefault();
                return inscripcion;
            }
        }

        public inscripcion_evento obtenerInscripcionAEventoPorIdParticipantePorDni(int pDni, int pIdEvento)
        {
            using (var db = new JJSSEntities())
            {
                inscripcion_evento inscripcion = (from ins in db.inscripcion_evento
                    join part in db.participante_evento on ins.id_participante equals part.id_participante
                    where part.dni == pDni && ins.id_evento == pIdEvento
                    select ins).FirstOrDefault();
                return inscripcion;
            }
        }

        /*
* Obtenemos un listado de todos los participantes que estan en un torneo con datos de su categoria a la cual esta inscripto, la faja, y datos
* propios del participante
*/
        public string ComprobanteInscripcion(int pID, string pMail)
        {
            GestorReportes gestorReportes = new GestorReportes();

            using (var db = new JJSSEntities())
            {
                var participantes = from inscr in db.inscripcion_evento
                                    join part in db.participante_evento on inscr.id_participante equals part.id_participante
                                    where inscr.id_inscripcion == pID
                                    select new CompInscripcionEvento()
                                    {
                                        ev_nombre = inscr.evento_especial.nombre,
                                        ev_sede = inscr.evento_especial.sede.nombre,
                                        ev_direccion = inscr.evento_especial.sede.direccion.calle + " " + inscr.evento_especial.sede.direccion.numero + " - " + inscr.evento_especial.sede.direccion.ciudad.nombre + " - " + inscr.evento_especial.sede.direccion.ciudad.provincia.nombre + " - " + inscr.evento_especial.sede.direccion.ciudad.provincia.pais.nombre,
                                        ev_fechaD = inscr.evento_especial.fecha,
                                        ev_hora = inscr.evento_especial.hora,
                                        ev_tipo = inscr.evento_especial.tipo_evento_especial.nombre,
                                        ev_precio = inscr.evento_especial.precio.ToString(),
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nacD = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_dni = part.dni.ToString()

                                    };
                List<CompInscripcionEvento> participantesList = participantes.ToList<CompInscripcionEvento>();


                foreach (CompInscripcionEvento part in participantesList)
                {
                    if (part.par_sexo == 1)
                    {
                        part.par_sexo_nombre = "M";

                    }
                    else
                    {
                        part.par_sexo_nombre = "F";
                    }
                    part.par_fecha_nac = part.par_fecha_nacD?.ToString("dd/MM/yyyy") ?? " - ";
                    part.ev_fecha = part.ev_fechaD?.ToString("dd/MM/yyyy") ?? " - ";

                }

                string sFile = gestorReportes.GenerarReporteComprInscripcionEvento(participantesList);
                if (pMail !=null)
                {
                    EnviarMail(sFile, pMail);
                }
                
                return sFile;

            }

        }

        private void EnviarMail(string sFile, string mail)
        {
            modEmails md = new modEmails();
            md.Msg_Adjuntos.Add(sFile);
            md.Msg_Destinatarios.Add(mail);
            md.Msg_Asunto = "Comprobante de Inscripción a Evento de Lotus Club - Equipo Hinojal";
            md.Enviar();

        }


        /*
         * Aun no aplica
         */

        /*
         * Aun no aplica
         */




    }


}

