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
        public String InscribirAlumnoAClase(string pDNIAlumno, int pClase, string pHora, DateTime pFecha, int pIdFaja)
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
                            hora = pHora,
                            actual = Constantes.ConstatesBajaLogica.ACTUAL,
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
                                actual = Constantes.ConstatesBajaLogica.ACTUAL,
                            };
                            db.alumnoxfaja.Add(nuevaFaja);
                            db.SaveChanges();
                        }
                        alumno alumnoInscribir = db.alumno.Find(pAlumno.id_alumno);
                        gestorAlumnos.cambiarEstadoAActivo(alumnoInscribir.id_alumno);
                        
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


        public String InscribirAlumnoAClaseID(int id, int pClase, string pHora, DateTime pFecha, int pIdFaja)
        {
            String sReturn = "";
            GestorAlumnos gestorAlumnos = new GestorAlumnos();
            alumno pAlumno = gestorAlumnos.ObtenerAlumnoPorID(id);
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
                            hora = pHora,
                            actual = Constantes.ConstatesBajaLogica.ACTUAL,
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
                                actual = Constantes.ConstatesBajaLogica.ACTUAL,
                            };
                            db.alumnoxfaja.Add(nuevaFaja);
                            db.SaveChanges();
                        }
                        alumno alumnoInscribir = db.alumno.Find(pAlumno.id_alumno);
                        gestorAlumnos.cambiarEstadoAActivo(alumnoInscribir.id_alumno);

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
                                         && ic.actual == Constantes.ConstatesBajaLogica.ACTUAL
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

                if (tipoClase.FirstOrDefault() == null) return false;
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
                              && ins.actual == Constantes.ConstatesBajaLogica.ACTUAL
                              orderby alu.apellido
                              select alu;
                return alumnos.ToList();
            }
        }

        public string DarDeBajaInscripcion(int idAlumno, int idClase)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var ins = from i in db.inscripcion_clase
                              where i.id_alumno == idAlumno && i.id_clase == idClase
                              && i.actual == Constantes.ConstatesBajaLogica.ACTUAL
                              select i;
                    inscripcion_clase inscripcionSeleccionada = ins.FirstOrDefault();
                    inscripcionSeleccionada.actual = Constantes.ConstatesBajaLogica.NO_ACTUAL;
                    db.SaveChanges();

                    ins = from i in db.inscripcion_clase
                          where i.id_alumno == idAlumno
                          && i.actual == Constantes.ConstatesBajaLogica.ACTUAL
                          select i;
                    if (ins.Count() == 0)
                    {
                        alumno alumnoSeleccionado = db.alumno.Find(idAlumno);
                        alumnoSeleccionado.id_estado = Constantes.ConstantesEstado.ALUMNOS_INACTIVO;
                        db.SaveChanges();
                    }
                    
                    transaction.Commit();
                    return "";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }
            }
        }



        /*
* Obtenemos un listado de todos los participantes que estan en un torneo con datos de su categoria a la cual esta inscripto, la faja, y datos
* propios del participante
*/

        private static string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public string ComprobanteInscripcionPago(int pID, string pMail, int formaPago)
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

                var gestorClases = new GestorClases();
               

                var participantes = from inscr in db.inscripcion_clase
                                    join alu in db.alumno on inscr.id_alumno equals alu.id_alumno
                                    where inscr.id_inscripcion == pID
                                    select new CompInscripcionClasePago()
                                    {
                                        cla_id =(int) inscr.id_clase,
                                        cla_alumno = (int) inscr.id_alumno,
                                        cla_nombre = inscr.clase.nombre,
                                        cla_academia = inscr.clase.academia.nombre,
                                        cla_direccion = inscr.clase.academia.direccion.calle + " " + inscr.clase.academia.direccion.numero + " - " + inscr.clase.academia.direccion.ciudad.nombre + " - " + inscr.clase.academia.direccion.ciudad.provincia.nombre + " - " + inscr.clase.academia.direccion.ciudad.provincia.pais.nombre,
                                       
                                        cla_precio = inscr.clase.precio.ToString(),
                                        cla_tipo = inscr.clase.tipo_clase.nombre,
                                     
                                        par_nombre = alu.nombre,
                                        par_apellido = alu.apellido,
                                        par_fecha_nacD = alu.fecha_nacimiento,
                                        par_sexo = alu.sexo,
                                        par_dni = alu.dni,
                                        pag_forma_pago = formaString,
                                        par_tipo_documento = alu.tipo_documento.codigo

                                    };
                List<CompInscripcionClasePago> participantesList = participantes.ToList();
                int mes = DateTime.Today.Month;

                foreach (CompInscripcionClasePago part in participantesList)
                {
                    var recargo = gestorClases.calcularRecargo(part.cla_id, part.cla_alumno);
                    part.cla_precio = part.cla_precio + recargo;


                    string mesNombre = meses[mes - 1];

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
                    part.pag_mes = mesNombre;
                }

                string sFile = gestorReportes.GenerarReporteComprInscripcionClasePago(participantesList);
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

    }
}
