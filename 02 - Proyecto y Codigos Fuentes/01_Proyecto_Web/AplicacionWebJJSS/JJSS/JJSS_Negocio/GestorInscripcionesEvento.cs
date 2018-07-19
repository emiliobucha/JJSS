using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio.Resultados;

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


        /*
         * Método que busca un alumno por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */
        public alumno ObtenerAlumnoPorDNITipo(int pTipo, string pDni)
        {
            GestorAlumnos gestorAlumnos = new GestorAlumnos();

            return gestorAlumnos.ObtenerAlumnoPorDNITipo(pTipo, pDni);
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
        public string InscribirAEvento(int pEvento, string pNombre, string pApellido, DateTime pFechaNacimiento, short pSexo, int pTipo, string pDni, int? pIDAlumno, int idPais)
        {

            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {

                    if (obtenerParticipanteEvento(pTipo, pDni, pEvento) != null)
                    {
                        return "Participante exitente";
                    }


                    var nuevoParticipante = ObtenerParticipanteEventoPorDniTipo(pTipo, pDni);

                    if(nuevoParticipante == null)
                    {
                        nuevoParticipante = new participante_evento()
                        {
                            nombre = pNombre,
                            apellido = pApellido,
                            sexo = pSexo,
                            fecha_nacimiento = pFechaNacimiento,
                            dni = pDni,
                            id_alumno = pIDAlumno,
                            id_tipo_documento = pTipo,
                            id_pais = idPais

                        };
                        db.participante_evento.Add(nuevoParticipante);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (nuevoParticipante.nombre != pNombre)
                        {
                            nuevoParticipante.nombre = pNombre;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.apellido != pApellido)
                        {
                            nuevoParticipante.apellido = pApellido;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.fecha_nacimiento != pFechaNacimiento)
                        {
                            nuevoParticipante.fecha_nacimiento = pFechaNacimiento;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.id_pais != idPais)
                        {
                            nuevoParticipante.id_pais = idPais;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.sexo != pSexo)
                        {
                            nuevoParticipante.sexo = pSexo;
                            db.SaveChanges();
                        }


                    }


                    string hora = DateTime.Now.ToString("hh:mm tt");
                    DateTime fecha = DateTime.Now.Date;

                    inscripcion_evento nuevaInscripcion = new inscripcion_evento()
                    {

                        hora = hora,
                        fecha = fecha,
                        id_participante = nuevoParticipante.id_participante,
                        id_evento = pEvento
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

        public participante_evento obtenerParticipanteEvento(int pTipo, string pDni, int pIDEvento)
        {
            using (var db = new JJSSEntities())
            {
                participante_evento participante = (from part in db.participante_evento
                                                    join ins in db.inscripcion_evento on part.id_participante equals ins.id_participante
                                                    where part.dni == pDni && ins.id_evento == pIDEvento && part.id_tipo_documento == pTipo
                                                    select part).FirstOrDefault();
                return participante;
            }
        }

        public participante_evento ObtenerParticipanteEventoPorDniTipo(int pTipo, string pDni)
        {
            using (var db = new JJSSEntities())
            {
                var participante =
                    db.participante_evento.FirstOrDefault(x => x.dni == pDni && x.id_tipo_documento == pTipo);
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

        public inscripcion_evento obtenerInscripcionAEventoPorIdParticipantePorDni(int pTipo, string pDni, int pIdEvento)
        {
            using (var db = new JJSSEntities())
            {
                inscripcion_evento inscripcion = (from ins in db.inscripcion_evento
                                                  join part in db.participante_evento on ins.id_participante equals part.id_participante
                                                  where part.dni == pDni && ins.id_evento == pIdEvento && part.id_tipo_documento == pTipo
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
                                        par_dni = part.dni.ToString(),
                                        par_tipo_documento = part.tipo_documento.codigo


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
                if (pMail != null)
                {
                    EnviarMail(sFile, pMail);
                }

                return sFile;

            }

        }

        /*
* Obtenemos un listado de todos los participantes que estan en un torneo con datos de su categoria a la cual esta inscripto, la faja, y datos
* propios del participante
*/
        public string ComprobanteInscripcionPago(int pID, string pMail,int formaPago)
        {
            GestorReportes gestorReportes = new GestorReportes();

            using (var db = new JJSSEntities())
            {
                var forma = db.forma_pago.Find(formaPago);
                string formaString = "";

                if (forma != null)
                {
                    formaString = forma.nombre;
                }
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
                                        par_dni = part.dni,
                                        pag_forma_pago = formaString,
                                        par_tipo_documento = part.tipo_documento.codigo

                                    };
                List<CompInscripcionEvento> participantesList = participantes.ToList();


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
                    part.pag_fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " hs";
                    part.par_fecha_nac = part.par_fecha_nacD?.ToString("dd/MM/yyyy") ?? " - ";
                    part.ev_fecha = part.ev_fechaD?.ToString("dd/MM/yyyy") ?? " - ";
                }

                string sFile = gestorReportes.GenerarReporteComprInscripcionEventoPago(participantesList);
                if (pMail != null)
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

        public List<pais> ObtenerNacionalidades()
        {
            try
            {
                using (var db = new JJSSEntities())
                {
                    var list = db.pais.ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                return new List<pais>();
            }
        }

        public List<tipo_documento> ObtenerTiposDocumentos()
        {
            try
            {
                using (var db = new JJSSEntities())
                {

                    var list = db.tipo_documento.ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                return new List<tipo_documento>();
            }
        }

        public List<MisInscripciones> ObtenerInscripcionesDeAlumno(int pIDAlumno, Boolean verTodos)
        {
            DateTime fechaActual = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var inscripcion = from ie in db.inscripcion_evento
                                  where ie.participante_evento.id_alumno == pIDAlumno && (verTodos || ie.evento_especial.fecha >= fechaActual)
                                  select new MisInscripciones()
                                  {
                                      nombre = ie.evento_especial.nombre,
                                      dtFecha = ie.evento_especial.fecha,
                                      hora = ie.evento_especial.hora,
                                      id_inscripcion = ie.id_inscripcion,
                                      dtFechaInscripcion = ie.fecha,
                                      pago = ie.pago,
                                      tipo_evento = ie.evento_especial.tipo_evento_especial.nombre,
                                  };
                List<MisInscripciones> inscripcionesList = inscripcion.ToList();
                foreach (MisInscripciones ins in inscripcionesList)
                {
                    ins.fecha_inscripcion = ((DateTime)ins.dtFechaInscripcion).ToString("dd/MM/yyyy");
                    ins.fecha = ((DateTime)ins.dtFecha).ToString("dd/MM/yyyy");
                    ins.hora = ins.hora + " hs";
                }
                return inscripcionesList;
            }
        }

    }


}

