using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Herramientas;
using Newtonsoft.Json;

namespace JJSS_Negocio
{
    public class GestorVencimientos
    {


        public string ActualizarEstadosInscripcion()
        {
            using (var db = new JJSSEntities())
            {
              //  var logger = new Logger("ActualizarEstadoInscripcion-" + DateTime.Today.ToString("ddMMyyyy"));
                var transaction = db.Database.BeginTransaction();
                try
                {
                  //  logger.AgregarMensaje("Inicio", "Inicio de actualizacion de estado");

                    var inscripcionesActivas = db.inscripcion_clase.Where(x => x.actual == 1);
                    foreach (var inscripcion in inscripcionesActivas)
                    {
                      //  logger.AgregarMensaje("Objeto Inscripción", "id: " + inscripcion.id_inscripcion + ", id_clase: " + inscripcion.id_clase + ", id_alumno: " + inscripcion.id_alumno + ", fecha_desde: " + inscripcion.fecha_desde?.ToString("dd/MM/yyyy") + ", fecha_vencimiento: " + inscripcion.fecha_vencimiento?.ToString("dd/MM/yyyy") + ", actual: " + inscripcion.actual + ", provisorio: " + inscripcion.provisoria + ", moroso_si:" + inscripcion.moroso_si + ", recargo:" + inscripcion.recargo);
                        //Veo si la fecha de vencimiento ya pasó
                        if (inscripcion.fecha_vencimiento < DateTime.Today)
                        {
                        //    logger.AgregarMensaje("Es vencida", "");
                            //Si la inscripción vencida estaba paga, genero una nueva inscripción
                            if (inscripcion.pago_clase.Count > 0)
                            {

                          //      logger.AgregarMensaje("Habia pagado", "");
                                var proximoVto = inscripcion.fecha_vencimiento.Value.AddMonths(1);

                                //Si no hay una inscripcion ya en ese periodo
                                var inscripcionFutura = db.inscripcion_clase.Where(
                                    x => x.id_clase == inscripcion.id_clase &&
                                    x.id_alumno == inscripcion.id_alumno &&
                                  DbFunctions.TruncateTime(x.fecha_vencimiento) >= DbFunctions.TruncateTime(proximoVto)
                                );
                                //Si no hay una inscripcion ya en ese periodo creo una nueva
                                if (!inscripcionFutura.Any())
                                {

                             //       logger.AgregarMensaje("No hay una inscripcion futura", "");


                                    var newInscripcion = new inscripcion_clase
                                    {

                                        fecha = DateTime.Now,
                                        hora = DateTime.Now.ToString("HH:mm"),
                                        fecha_desde = inscripcion.fecha_vencimiento,
                                        fecha_vencimiento = proximoVto,
                                        actual = 1,
                                        provisoria = 1,
                                        moroso_si = 0,
                                        recargo = 0,
                                        id_clase = inscripcion.id_clase,
                                        id_alumno = inscripcion.id_alumno
                                    };
                                //    logger.AgregarMensaje("Creo una nueva", JsonConvert.SerializeObject(newInscripcion, Formatting.None));
                                    db.inscripcion_clase.Add(newInscripcion);
                                    db.SaveChanges();
                                }

                           //     logger.AgregarMensaje("Doy de baja la inscripcion anterior", "");

                                //Doy de baja la vencida
                                inscripcion.actual = 0;
                                db.SaveChanges();

                            }
                            //Si la inscripción de este mes le permite ingresar como moroso
                            //NO doy de baja la vencida y genero una nueva
                            else if (inscripcion.moroso_si == 1)
                            {
                         //       logger.AgregarMensaje("Tiene permiso de pasar como moroso", "");
                                var proximoVto = inscripcion.fecha_vencimiento.Value.AddMonths(1);

                                //Si no hay una inscripcion ya en ese periodo
                                var inscripcionFutura = db.inscripcion_clase.Where(
                                    x => x.id_clase == inscripcion.id_clase &&
                                         x.id_alumno == inscripcion.id_alumno &&
                                         DbFunctions.TruncateTime(x.fecha_vencimiento) >= DbFunctions.TruncateTime(proximoVto)
                                );
                                //Si no hay una inscripcion ya en ese periodo creo una nueva
                                if (!inscripcionFutura.Any())
                                {
                         //           logger.AgregarMensaje("No hay una inscripcion futura", "");
                                    var newInscripcion = new inscripcion_clase
                                    {

                                        fecha = DateTime.Now,
                                        hora = DateTime.Now.TimeOfDay.ToString("HH:mm"),
                                        fecha_desde = inscripcion.fecha_vencimiento,
                                        fecha_vencimiento = proximoVto,
                                        actual = 1,
                                        provisoria = 1,
                                        moroso_si = 0,
                                        recargo = 0,
                                        id_clase = inscripcion.id_clase,
                                        id_alumno = inscripcion.id_alumno
                                    };
                         //           logger.AgregarMensaje("Creo una nueva", JsonConvert.SerializeObject(newInscripcion, Formatting.None));
                                    db.inscripcion_clase.Add(newInscripcion);
                                    db.SaveChanges();
                                }
                            }

                            //No pagó, vencida, era provisoria y sin recargo por asistencia vencida. Baja
                            else if (inscripcion.provisoria == 1 && inscripcion.recargo == 0)
                            {
                        //        logger.AgregarMensaje("Era provisoria y no tenia recargo, doy de baja", "");
                                inscripcion.actual = 0;
                                db.SaveChanges();
                            }
                            //Si no pagó, vencida, provisoria, con recargo y asistencias, se queda como una inscripcion activa para poder luego registrar pago.

                        }
                        //Verifico si es provisoria y si ya cumplió con los 10 dias de vencimiento
                        else if (inscripcion.provisoria == 1 && inscripcion.fecha_desde.Value.AddDays(10) > DateTime.Today)
                        {
                         //   logger.AgregarMensaje("Era provisoria y se vencieron los 10 dias", "");

                            //Si habia pagado, deja de ser provisoria
                            if (inscripcion.pago_clase.Count > 0)
                            {
                        //        logger.AgregarMensaje("Habia pagado, deja de ser moroso si, tambien deja de ser provisoria", "");

                                inscripcion.provisoria = 0;
                                inscripcion.moroso_si = 0;
                                db.SaveChanges();
                            }
                            //Si no habia pagado
                            else
                            {
                         //       logger.AgregarMensaje("No Habia pagado", "");

                                // y tenia asistencias, se le suma el recargo
                                if (inscripcion.asistencia_clase.Count > 0)
                                {
                         //           logger.AgregarMensaje("No habia pagado pero asistió, cobro recargo", "");

                                    inscripcion.recargo = 1;
                                    db.SaveChanges();
                                }
                                //Si no puede ser moroso y no asistio nunca se da de baja
                                else if (inscripcion.moroso_si == 0)
                                {
                          //          logger.AgregarMensaje("No asistio y no puede ser moroso, da de baja", "");
                                    inscripcion.actual = 0;
                                    db.SaveChanges();
                                }

                            }
                        }

                      //  logger.AgregarMensaje("----------------------", "---------------------------------------");
                    }

                    //logger.AgregarMensaje("Fin", "Fin de actualizacion de estado de inscripciones");
                    //logger.EscribirLog();

                    transaction.Commit();
                    return "OK";
                }
                catch (Exception e)
                {
                    //logger.AgregarMensaje("Exception", JsonConvert.SerializeObject(e, Formatting.None));
                    //logger.EscribirLog();
                    transaction.Rollback();
                    return e.Message;
                }

            }

        }


        public string ActualizarEstadosAlumno()
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var alumnosActivos = db.alumno.Where(x => x.baja_logica == 1 && x.id_estado != ConstantesEstado.ALUMNOS_DE_BAJA);
                    foreach (var alumno in alumnosActivos)
                    {
                        var inscripciones = alumno.inscripcion_clase;
                        if (inscripciones.Count == 0) continue;

                        if (inscripciones.Any(x => x.recargo == 1 && x.actual == 1))
                        {
                            alumno.id_estado = ConstantesEstado.ALUMNOS_MOROSO;
                            db.SaveChanges();
                        }
                        if (inscripciones.All(x => x.actual == 0))
                        {
                            alumno.id_estado = ConstantesEstado.ALUMNOS_INACTIVO;
                            db.SaveChanges();
                        }
                        if (inscripciones.Where(x => x.recargo == 1).All(x => x.actual == 0) && inscripciones.Where(x => x.actual == 1).All(x => x.recargo == 0))
                        {
                            alumno.id_estado = ConstantesEstado.ALUMNOS_ACTIVO;
                            db.SaveChanges();
                        }
                    }

                    transaction.Commit();
                    return "OK";
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }
            }
        }

        public string ActualizarPeriodo(int idInscripcion, DateTime fecha)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {

                    var inscripcion = db.inscripcion_clase.Find(idInscripcion);

                    if (inscripcion == null) return "Inscripción inválida";

                    inscripcion.actual = 0;

                    var proximo = fecha.AddMonths(1);

                    var nuevaInscripcion = new inscripcion_clase
                    {
                        fecha = DateTime.Now,
                        hora = DateTime.Now.ToString("HH:mm"),
                        fecha_desde = fecha,
                        fecha_vencimiento =proximo ,
                        actual = 1,
                        provisoria = inscripcion.provisoria,
                        moroso_si = inscripcion.moroso_si,
                        recargo = inscripcion.recargo,
                        id_clase = inscripcion.id_clase,
                        id_alumno = inscripcion.id_alumno
                    };
                    db.inscripcion_clase.Add(nuevaInscripcion);
                    db.SaveChanges();


                    if (inscripcion.pago_clase.Any())
                    {
                        var pago = inscripcion.pago_clase.FirstOrDefault();
                        if (pago != null) pago.id_inscripcion_clase = nuevaInscripcion.id_inscripcion;
                    }


                    db.SaveChanges();
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
    }
}
