using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Data;

namespace JJSS_Negocio
{
    /*
     * Clase que nos permite gestionar las incripciones a una clase por parte de un participante
     */
    public class GestorInscripcionesClase
    {
        private const int ALUMNO_ACTIVO = 9;

        /*
         * Método que nos permite obtener un listado de todas las inscripciones a clases
         * Retorno:List<incripcion_clase>
         *          Listado de todas las inscripciones a las distintas clases
         *          
         */
        public List<inscripcion_clase> ObtenerInscripcionesClase()
        {
            using (var db = new JJSSEntities())
            {
                return db.inscripcion_clase.ToList();
            }
        }

        /*
         * Método que nos permite inscribir un alumno a una clase
         * Paramétros:
         *              pDNIAlumno: entero que representa el dni del alumno a inscribir
         *              pClase: entero que indica el id de la clase a la que se va a inscribir
         *              pHora: Hora de la transacción, en que se generó la inscripción
         *              pFecha: datetime de la fecha en la que se inscribió
         *              pIdFaja: entero que representa el id de la faja a asignar
         * Retornos:String  
         *          "" - Transacción completada correctamente
         *          Mensaje de Excepcion de transaccion
         * Excepciones:
         *              Alumno no Registrado
         *              Ya esta inscripto el alumno
         *  
         */
        public String InscribirAlumnoAClase(int pDNIAlumno, int pClase, string pHora, DateTime pFecha, int pIdFaja)
        {
            String sReturn = "";
            GestorAlumnos gestorAlumnos = new GestorAlumnos();
            alumno pAlumno = gestorAlumnos.ObtenerAlumnoPorDNI(pDNIAlumno);
            if (pAlumno == null)
            {
                throw new Exception("El alumno no está registrado");
            }
            if (ObtenerAlumnoInscripto(pAlumno.id_alumno, pClase) != null)
            {
                throw new Exception("El alumno ya está inscripto a esa clase");
            }

            try
            {
                using (var db = new JJSSEntities())
                {
                    /*Probando esta otra forma para luego ver cual forma tiene mayor rendimiento*/


                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        inscripcion_clase nuevaInscripcion = new inscripcion_clase()
                        {
                            id_alumno = pAlumno.id_alumno,
                            id_clase = pClase,
                            fecha = pFecha,
                            hora = pHora
                        };
                        db.inscripcion_clase.Add(nuevaInscripcion);
                        db.SaveChanges();
                        if (pIdFaja > 0)
                        {
                            alumnoxfaja nuevaFaja = new alumnoxfaja()
                            {
                                id_alumno = pAlumno.id_alumno,
                                id_faja = pIdFaja,
                                fecha = pFecha,
                                actual = 1,
                            };
                            db.alumnoxfaja.Add(nuevaFaja);
                            db.SaveChanges();
                        }
                        alumno alumnoInscribir = db.alumno.Find(pAlumno.id_alumno);
                        alumnoInscribir.id_estado = ALUMNO_ACTIVO;
                        
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /*
         * Obtener inscripcion de un alumno a una clase
         * Parametros:
         *              pIDAlumno: entero que representa el id del alumno
         *              pIDClase: entero que representa el id de la clase 
         * Retornos:Inscripcion_clase
         *          Inscripcion del alumno a dicha clase pudiendo ser nulo el resultado si no estaba inscripto
         *              
         */
        public inscripcion_clase ObtenerAlumnoInscripto(int pIDAlumno, int pIDClase)
        {
            using (var db = new JJSSEntities())
            {
                var inscripcion = from ic in db.inscripcion_clase
                                  where ic.id_clase == pIDClase &&
                                         ic.id_alumno == pIDAlumno
                                  select ic;
                return inscripcion.FirstOrDefault();
            }
        }

        /*
         * Metodo que valida si el alumno esta inscripto a ese tipo de clase
         * Parametros:  pIdAlumno: entero - ID del alumno
         *              pIdTipoClase: entero - ID tipo clase
         * Retorno: true: el alumno está inscripto en la clase
         *          false: el alumno no esta inscripto en la clase
         * 
         */
        public Boolean ValidarTipoClaseAlumno(int pIdAlumno, int pIdTipoClase)
        {
            using (var db = new JJSSEntities())
            {
                var tipoClase = from alu in db.alumno
                                join ins in db.inscripcion_clase on alu.id_alumno equals ins.id_alumno
                                join clase in db.clase on ins.id_clase equals clase.id_clase
                                join tipo in db.tipo_clase on clase.id_tipo_clase equals tipo.id_tipo_clase
                                where (alu.id_alumno == pIdAlumno) && (tipo.id_tipo_clase == pIdTipoClase)
                                select tipo;

                if (tipoClase.ToList() == null) return false;
                else return true;
            }
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
            using (var db= new JJSSEntities())
            {
                var fajasEncontradas = from faj in db.faja
                                       where faj.id_tipo_clase == pIdTipoClase
                                       select faj;
                return fajasEncontradas.ToList();
            }
        }

        /*
         * metodo que obtiene todos los alumnos inscriptos a una clase particular
         */ 
        public List<alumno> ObtenerAlumnosDeUnaClase(int pIDClase)
        {
            using (var db = new JJSSEntities())
            {
                var alumnos = from alu in db.alumno
                              join ins in db.inscripcion_clase on alu.id_alumno equals ins.id_alumno
                              where ins.id_clase == pIDClase && alu.baja_logica==1
                              orderby alu.apellido
                              select alu;
                return alumnos.ToList();
            }
        }
    }
}
