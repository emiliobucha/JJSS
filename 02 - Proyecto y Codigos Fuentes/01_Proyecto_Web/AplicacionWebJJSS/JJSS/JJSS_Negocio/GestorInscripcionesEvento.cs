using System;
using System.Collections.Generic;
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
        public alumno ObtenerAlumnoPorDNI(int pDni)
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
                        participante_evento = nuevoParticipante,
                        id_participante = nuevoParticipante.id_participante,
                        id_evento = eventoInscripto.id_evento,
                        evento_especial = eventoInscripto,
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
    }
}
