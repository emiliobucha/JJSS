﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Data;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Herramientas;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{
    /*
     * Clase que nos permite gestionar las incripciones a una clase por parte de un participante
     */
    public class GestorInscripcionesClase
    {

        /*
   * Método que nos permite obtener una inscripcion por id
   *          
   */
        public inscripcion_clase ObtenerInscripcionClasePorId(int id)
        {
            using (var db = new JJSSEntities())
            {
                return db.inscripcion_clase.Find(id);
            }
        }

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
                            fecha_desde = pFecha,
                            fecha_vencimiento = pFecha,
                            recargo = 0

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

        public int InscribirAlumnoAClaseIdReinscripcion(int id, int pClase, int inscripcionAnterior, DateTime pFecha, string pHora)
        {
            string sReturn = "";
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
                        var inscripcion = db.inscripcion_clase.Find(inscripcionAnterior);
                        inscripcion_clase nuevaInscripcion;
                        if (inscripcion != null)
                        {

                            nuevaInscripcion = new inscripcion_clase()
                            {
                                id_alumno = pAlumno.id_alumno,
                                id_clase = pClase,
                                fecha = pFecha,
                                hora = pHora,
                                fecha_desde = inscripcion.fecha_vencimiento,
                                fecha_vencimiento = inscripcion.fecha_vencimiento.Value.AddMonths(1),
                                actual = 1,
                                recargo = 0,
                                moroso_si = 0,
                                provisoria = 1
                            };
                            db.inscripcion_clase.Add(nuevaInscripcion);
                            db.SaveChanges();

                        }
                        else
                        {
                            throw new Exception("No habia una inscripcion anterior para hacer la reinscripcion");

                        }


                        transaction.Commit();
                        return nuevaInscripcion.id_inscripcion;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 0;
                    }

                }
            }
            catch (Exception ex)
            {
                return 0;
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
                            fecha_desde = pFecha,
                            fecha_vencimiento = pFecha.AddMonths(1),
                            actual = 1,
                            recargo = 0,
                            moroso_si = 0,
                            provisoria = 1
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
                alumno alumno = db.alumno.Find(pIDAlumno);
                var manana = DateTime.Today.AddDays(1);
                return alumno.inscripcion_clase.FirstOrDefault(x => x.id_clase == pIDClase && x.actual == 1 && x.fecha_vencimiento > manana);
            }
        }

        /*
        * Obtener inscripciones de un alumno a una clase
        * Parametros:
        *              pIDAlumno: entero que representa el id del alumno
        *              pIDClase: entero que representa el id de la clase 
        * Retornos:Inscripcion_clase
        *          Inscripcion del alumno a dicha clase pudiendo ser nulo el resultado si no estaba inscripto
        *              
        */
        public List<inscripcion_clase> ObtenerAlumnoInscriptoList(int pIDAlumno, int pIDClase)
        {
            using (var db = new JJSSEntities())
            {
                var inscripcion = from ic in db.inscripcion_clase
                    where ic.id_clase == pIDClase &&
                          ic.id_alumno == pIDAlumno
                          && ic.actual == Constantes.ConstatesBajaLogica.ACTUAL
                    select ic;
                return inscripcion.OrderByDescending(x=>x.fecha).ToList();
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

                return tipoClase.FirstOrDefault() != null;
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
            using (var db = new JJSSEntities())
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
                              where ins.id_clase == pIDClase && alu.baja_logica == 1 && alu.id_estado != Constantes.ConstantesEstado.ALUMNOS_DE_BAJA
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
                    var ins = db.inscripcion_clase.Where(x => x.id_clase == idClase && x.id_alumno == idAlumno && x.actual == ConstatesBajaLogica.ACTUAL).OrderByDescending(x => x.fecha);

                    foreach (var inscripcion in ins)
                    {
                        inscripcion.actual = 0;
                    }

                    db.SaveChanges();

                    var inscripciones = from i in db.inscripcion_clase
                                        where i.id_alumno == idAlumno
                                        && i.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                        select i;
                    if (!inscripciones.Any())
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

        public string DarDeBajaInscripcionPorId(int idInscripcion, int idAlumno)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var ins = db.inscripcion_clase.Find(idInscripcion);
                    ins.actual = Constantes.ConstatesBajaLogica.NO_ACTUAL;
                    db.SaveChanges();

                    var inscripciones = from i in db.inscripcion_clase
                                        where i.id_alumno == idAlumno
                                              && i.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                        select i;
                    if (!inscripciones.Any())
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

        public string ComprobanteInscripcionPago(int pID, string pMail, int formaPago, decimal monto)
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
                                        cla_id = (int)inscr.id_clase,
                                        cla_alumno = (int)inscr.id_alumno,
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
                                        par_tipo_documento = alu.tipo_documento.codigo,
                                        pag_monto = monto.ToString()

                                    };
                List<CompInscripcionClasePago> participantesList = participantes.ToList();
                int mes = DateTime.Today.Month;

                foreach (CompInscripcionClasePago part in participantesList)
                {




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
            md.Msg_Asunto = "Comprobante de Inscripción a Clase de Lotus Club - Equipo Hinojal";
            md.Enviar();

        }

        private String validarAsistenciaAPeriodo(inscripcion_clase inscripcion)
        {
            using (var db = new JJSSEntities())
            {
                List<asistencia_clase> asistencias = db.asistencia_clase.Where(x => x.id_inscripcion_clase == inscripcion.id_inscripcion).ToList();

                if (asistencias != null && asistencias.Count > 0) return "Si";
                return "No";
            }
        }


        public List<AlumnoFajaInscripciones> ObtenerAlumnosInscriptosClase(int idClase, int? idTipoDoc, string dni, string apellido, DateTime? desde, DateTime? hasta)
        {
            using (var db = new JJSSEntities())
            {
                List<AlumnoFajaInscripciones> list = new List<AlumnoFajaInscripciones>();
                var inscripciones = db.inscripcion_clase.Where(x => x.id_clase == idClase);

                if (idTipoDoc != null && idTipoDoc > 0 )
                {
                    inscripciones = inscripciones.Where(x => x.alumno.id_tipo_documento == idTipoDoc);
                }

                if (!string.IsNullOrEmpty(dni))
                {
                    inscripciones = inscripciones.Where(x => x.alumno.dni == dni);
                }

                if (!string.IsNullOrEmpty(apellido))
                {
                    inscripciones = inscripciones.Where(x => x.alumno.apellido == apellido);
                }

                if (desde != null)
                {
                    inscripciones = inscripciones.Where(x => x.fecha_desde >= desde);
                }
                if (hasta != null)
                {
                    inscripciones = inscripciones.Where(x => x.fecha_desde <= hasta);
                }



                foreach (var inscripcion in inscripciones)
                {
                    String asistencias = validarAsistenciaAPeriodo(inscripcion);
                    var alumno = new AlumnoFajaInscripciones
                    {
                        inscr_id = inscripcion.id_inscripcion,
                        inscr_apellido = inscripcion.alumno.apellido,
                        inscr_dni = inscripcion.alumno.dni,
                        inscr_tipoI = inscripcion.alumno.id_tipo_documento,
                        inscr_fecha_nac = inscripcion.alumno.fecha_nacimiento?.ToString("dd/MM/yyyy") ?? "01/01/2000",
                        inscr_fecha_nacD = inscripcion.alumno.fecha_nacimiento,
                        inscr_nombre = inscripcion.alumno.nombre,
                        inscr_tipo = inscripcion.alumno.tipo_documento.codigo,
                        recargo = inscripcion.recargo,
                        inscr_recargo = inscripcion.recargo == 1 ? "Si" : "No",
                        inscr_sexo = inscripcion.alumno.sexo == 1 ? "M" : "F",
                        inscr_id_alumno = inscripcion.alumno.id_alumno,
                        moroso_si = inscripcion.moroso_si,
                        asistio = asistencias,
                    };

                    var faja = db.alumnoxfaja.Where(x => x.id_alumno == inscripcion.alumno.id_alumno && x.actual == 1 && x.faja.id_tipo_clase == inscripcion.clase.id_tipo_clase)
                        .OrderByDescending(x => x.fecha).FirstOrDefault();

                    alumno.inscr_faja = faja != null ? faja.faja.descripcion : "Faja no clasificada";

                  
                    if (inscripcion.fecha_vencimiento == null)
                    {
                        alumno.inscr_pago = "No";
                        alumno.inscr_fecha_vto_mensual = " - ";

                        list.Add(alumno);
                        continue;
                    }
                    if (inscripcion.fecha_desde == null)
                    {
                        alumno.inscr_pago = "No";
                        alumno.inscr_fecha_vto_mensual = " - ";

                        list.Add(alumno);
                        continue;
                    }

                    alumno.inscr_fecha_vto_mensual = inscripcion.fecha_vencimiento.Value.ToString("dd/MM/yyyy");
                    alumno.inscr_fecha_vto = inscripcion.fecha_vencimiento.Value;
                    alumno.inscr_pago = inscripcion.pago_clase.FirstOrDefault() != null ? "Si" : "No";


                    alumno.inscr_fecha_desde_mensual = inscripcion.fecha_desde.Value.ToString("dd/MM/yyyy");
                    alumno.inscr_fecha_desde = inscripcion.fecha_vencimiento.Value;

                    list.Add(alumno);

                }
                return list.OrderByDescending(x => x.inscr_fecha_vto).ToList();
            }




        }

        public List<InscripcionesClase> ObtenerInscripcionesDeAlumno(int pIDAlumno)
        {
            using (var db = new JJSSEntities())
            {
                List<InscripcionesClase> list = new List<InscripcionesClase>();
                var inscripciones = db.inscripcion_clase.Where(x => x.id_alumno == pIDAlumno && x.actual == 1);
                foreach (var inscripcion in inscripciones)
                {
                    var alumno = new InscripcionesClase
                    {
                        recargo = inscripcion.recargo,
                        inscr_recargo = inscripcion.recargo == 1 ? "Si" : "No",

                    };

                    alumno.id_inscripcion = inscripcion.id_inscripcion;

                    var clase = db.clase.Find(inscripcion.id_clase);
                    alumno.nombre = clase.nombre;
                    alumno.id_clase = clase.id_clase;

                    var tipo_clase = db.tipo_clase.Find(clase.id_tipo_clase);
                    alumno.tipo_clase = tipo_clase.nombre;

                    var faja = db.alumnoxfaja.Where(x => x.id_alumno == inscripcion.alumno.id_alumno && x.actual == 1 &&
                    x.faja.id_tipo_clase == tipo_clase.id_tipo_clase)
                        .OrderByDescending(x => x.fecha).FirstOrDefault();

                    alumno.inscr_faja = faja != null ? faja.faja.descripcion : "Faja no clasificada";

                    var hoy = DateTime.Now;

                    if (inscripcion.fecha_vencimiento == null)
                    {
                        alumno.inscr_pago = "No";
                        alumno.inscr_fecha_vto_mensual = " - ";

                        list.Add(alumno);
                        continue;
                    }
                    if (inscripcion.fecha_desde == null)
                    {
                        alumno.inscr_pago = "No";
                        alumno.inscr_fecha_vto_mensual = " - ";

                        list.Add(alumno);
                        continue;
                    }


                    alumno.inscr_fecha_vto_mensual = inscripcion.fecha_vencimiento.Value.ToString("dd/MM/yyyy");
                    alumno.inscr_fecha_vto = inscripcion.fecha_vencimiento.Value;

                    alumno.inscr_fecha_desde_mensual = inscripcion.fecha_desde.Value.ToString("dd/MM/yyyy");
                    alumno.inscr_fecha_desde = inscripcion.fecha_vencimiento.Value;

                    alumno.inscr_pago = inscripcion.pago_clase.FirstOrDefault() != null ? "Si" : "No";

                    list.Add(alumno);

                }
                return list.OrderBy(x => x.inscr_fecha_vto).ToList();
            }
           
        }

        public InscripcionesClase ObtenerInscripcionClaseAlumno(int pIDAlumno, int pIDClase)
        {
            using (var db = new JJSSEntities())
            {

                var inscripcion = db.inscripcion_clase.Where(x => x.id_alumno == pIDAlumno && x.actual == 1 && x.id_clase == pIDClase).OrderByDescending(x => x.fecha).FirstOrDefault();

                var alumno = new InscripcionesClase
                {
                    recargo = inscripcion.recargo,
                    inscr_recargo = inscripcion.recargo == 1 ? "Si" : "No",

                };

                alumno.id_inscripcion = inscripcion.id_inscripcion;

                var clase = db.clase.Find(inscripcion.id_clase);
                alumno.nombre = clase.nombre;
                alumno.id_clase = clase.id_clase;

                var tipo_clase = db.tipo_clase.Find(clase.id_tipo_clase);
                alumno.tipo_clase = tipo_clase.nombre;

                var faja = db.alumnoxfaja.Where(x => x.id_alumno == inscripcion.alumno.id_alumno && x.actual == 1 &&
                                                     x.faja.id_tipo_clase == tipo_clase.id_tipo_clase)
                    .OrderByDescending(x => x.fecha).FirstOrDefault();

                alumno.inscr_faja = faja != null ? faja.faja.descripcion : "Faja no clasificada";

                var hoy = DateTime.Now;



                if (inscripcion.fecha_vencimiento == null)
                {
                    alumno.inscr_pago = "No";
                    alumno.inscr_fecha_vto_mensual = " - ";


                }
                if (inscripcion.fecha_desde == null)
                {
                    alumno.inscr_pago = "No";
                    alumno.inscr_fecha_vto_mensual = " - ";


                }


                alumno.inscr_fecha_vto_mensual = inscripcion.fecha_vencimiento.Value.ToString("dd/MM/yyyy");
                alumno.inscr_fecha_vto = inscripcion.fecha_vencimiento.Value;




                alumno.inscr_pago = inscripcion.pago_clase.FirstOrDefault() != null ? "Si" : "No";

                return alumno;



            }
        }


        public List<AlumnoInscripcionClase> ObtenerAlumnosInscribibles(int idClase, int? idTipoDoc, string dni, string apellido)
        {
            var logger = new Logger("ObtenerAlumnoInscribibles" + DateTime.Today.ToString("ddMMyyyy"));
            try
            {
                logger.AgregarMensaje("Inicio Consulta idClase: " + idClase, "");
                var list = new List<AlumnoInscripcionClase>();
                using (var db = new JJSSEntities())
                {
                    var alumnos = db.alumno.Where(x =>
                        x.baja_logica == 1 && x.id_estado != ConstantesEstado.ALUMNOS_DE_BAJA);
                    if (idTipoDoc != null && idTipoDoc > 0)
                    {
                        alumnos = alumnos.Where(x => x.id_tipo_documento == idTipoDoc);
                    }
                    if (!string.IsNullOrEmpty(dni))
                    {
                        alumnos = alumnos.Where(x => x.dni == dni);
                    }
                    if (!string.IsNullOrEmpty(apellido))
                    {
                        alumnos = alumnos.Where(x => x.apellido.ToUpper().StartsWith(apellido.ToUpper()));
                    }

                    foreach (var alumno in alumnos)
                    {
                        var alumnoInscripcion = new AlumnoInscripcionClase
                        {
                            nombre = alumno.nombre,
                            apellido = alumno.apellido,
                            id_clase = idClase,
                            id_alumno = alumno.id_alumno,
                            id_tipo_documento = alumno.id_tipo_documento,
                            tipo_documento = alumno.tipo_documento.codigo,
                            inscripciones = new List<int>(),
                            inscripto = "N",
                            dni = alumno.dni
                        };

                        var manana = DateTime.Today.AddDays(1);
                        if (alumno.inscripcion_clase.Any(x => x.id_clase == idClase && x.actual == 1 && x.fecha_vencimiento > manana ))
                        {
                            alumnoInscripcion.inscripto = "S";
                            foreach (var inscr in alumno.inscripcion_clase.Where(x => x.id_clase == idClase))
                            {
                                alumnoInscripcion.inscripciones.Add(inscr.id_inscripcion);
                            }

                        }

                        list.Add(alumnoInscripcion);
                    }
                    return list;

                }


            }
            catch (Exception e)
            {
                return new List<AlumnoInscripcionClase>();
            }
        }

        public string HabilitarDeshabilitarMoroso(int idInscripcion)
        {
            try
            {
                using (var db = new JJSSEntities())
                {
                    var inscripcion = db.inscripcion_clase.Find(idInscripcion);
                    if (inscripcion == null)
                        return "No existe inscripción con ese id";
                    if (inscripcion.moroso_si == 0)               
                        inscripcion.moroso_si = 1;
                    else
                        inscripcion.moroso_si = 0;
                    db.SaveChanges();
                }

                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
