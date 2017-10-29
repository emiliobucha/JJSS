using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;

namespace JJSS_Negocio
{
    /*Clase que se encarga de gestionar las inscripciones a Torneos*/
    public class GestorInscripciones
    {
        private alumno alumnoExistente;

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
        public string InscribirATorneo(int pTorneo, string pNombre, string pApellido, double pPeso, DateTime pFechaNacimiento, int pFaja, short pSexo, int pDni, int? pIDAlumno)
        {

            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();

                
                try
                {
                    //Foraneos
                    torneo torneoInscripto = db.torneo.Find(pTorneo);

                    faja fajaElegida = db.faja.Find(pFaja);
                    int edad = DateTime.Today.AddTicks(-pFechaNacimiento.Ticks).Year - 1;

                    var cat =
                        from categoria in db.categoria
                        where (categoria.edad_desde <= edad)
                        && (categoria.edad_hasta > edad)
                        && (categoria.peso_desde <= pPeso)
                        && (categoria.peso_hasta > pPeso)
                        && (categoria.sexo == pSexo)
                        select categoria;

                    categoria categoriaPerteneciente = cat.First();

                    //Nuevos


                    if (obtenerParticipanteDeTorneo(pDni, pTorneo) != null)
                    {
                        return "Participante exitente";
                    }

                    //alumnoExistente = ObtenerAlumnoPorDNI(pDni);

                    participante nuevoParticipante;

                    nuevoParticipante = new participante()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        //peso = pPeso,

                        sexo = pSexo,
                        fecha_nacimiento = pFechaNacimiento,
                        dni = pDni,
                        id_alumno = pIDAlumno

                    };
                    db.participante.Add(nuevoParticipante);
                    db.SaveChanges();
                    var catTorneoExistente = from catTor in db.categoria_torneo
                                             where (catTor.id_categoria == categoriaPerteneciente.id_categoria)
                                             // && (catTor.faja.id_faja == fajaElegida.id_faja)
                                             && (catTor.sexo == pSexo)
                                             select catTor;
                    categoria_torneo nuevaCategoriaTorneo;
                    //+rever esto
                    if (catTorneoExistente.Count() == 0)

                    {
                        nuevaCategoriaTorneo = new categoria_torneo()
                        {
                            id_categoria = categoriaPerteneciente.id_categoria,
                            //faja = fajaElegida,
                            sexo = pSexo,

                        };
                    }
                    else
                    {
                        nuevaCategoriaTorneo = catTorneoExistente.First();
                    }
                    db.categoria_torneo.Add(nuevaCategoriaTorneo);
                    db.SaveChanges();

                    string hora = hora = DateTime.Now.ToString("hh:mm tt");
                    DateTime fecha = DateTime.Now.Date;
                    inscripcion nuevaInscripcion = new inscripcion()
                    {

                        hora = hora,
                        fecha = fecha,
                        codigo_barra = 123456789,
                        participante = nuevoParticipante,
                        id_participante = nuevoParticipante.id_participante,
                        id_torneo = torneoInscripto.id_torneo,
                        torneo = torneoInscripto,
                        peso = pPeso

                    };



                    db.inscripcion.Add(nuevaInscripcion);
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
        public List<torneo> ObtenerTorneos()
        {
            GestorTorneos gestorTorneos = new GestorTorneos();
            return gestorTorneos.ObtenerTorneos();
        }

        /*
         * Metodo que nos devuelve una lista de fajas para luego ser seleccionadas al momento de inscribirse
         */

        public List<faja> ObtenerFajas()
        {
            using (var db = new JJSSEntities())
            {
                return db.faja.ToList();
            }
        }

        public List<faja> ObtenerFajasPorTipoClase(int pIdTipoClase)
        {
            using (var db = new JJSSEntities())
            {
                var fajasEncontradas = from faj in db.faja
                                       where faj.id_tipo_clase == pIdTipoClase
                                       select faj;
                return fajasEncontradas.ToList();
            }
        }

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

        /*
         * Método que busca un participante por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */

        public participante ObtenerParticipanteporDNI(int pDni)
        {
            GestorParticipantes gestorParticipantes = new GestorParticipantes();
            return gestorParticipantes.ObtenerParticipantePorDNI(pDni);
        }


        public participante obtenerParticipanteDeTorneo(int pDni, int pIDTorneo)
        {
            using (var db = new JJSSEntities())
            {
                participante participante = (from part in db.participante
                                             join ins in db.inscripcion on part.id_participante equals ins.id_participante
                                             where part.dni == pDni && ins.id_torneo == pIDTorneo
                                             select part).FirstOrDefault();
                return participante;
            }
        }


        public inscripcion obtenerInscripcionATorneoPorIdParticipante(int pId, int pIDTorneo)
        {
            using (var db = new JJSSEntities())
            {
                inscripcion inscripcion = (from ins in db.inscripcion
                                             join part in db.participante on ins.id_participante  equals part.id_participante
                                             where part.id_participante == pId && ins.id_torneo == pIDTorneo
                                             select ins).FirstOrDefault();
                return inscripcion;
            }
        }
    }
}
