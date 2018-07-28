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

namespace JJSS_Negocio
{
    public class GestorVencimientos
    {


        public string ActualizarEstadosInscripcion()
        {
            using (var db = new JJSSEntities())
            {
                var logger = new Logger("ActualizarEstadoInscripcion-" + DateTime.Today.ToString("ddMMyyyy"));
                var transaction = db.Database.BeginTransaction();
                try
                {
                    logger.AgregarMensaje("Inicio", "Inicio de actualizacion de estado");

                    var inscripcionesActivas = db.inscripcion_clase.Where(x => x.actual == 1);
                    foreach (var inscripcion in inscripcionesActivas)
                    {
                        //Veo si la fecha de vencimiento ya pasó
                        if (inscripcion.fecha_vencimiento < DateTime.Today)
                        {
                            //Si la inscripción vencida estaba paga, genero una nueva inscripción
                            if (inscripcion.pago_clase.Count > 0)
                            {
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

                                    db.inscripcion_clase.Add(newInscripcion);
                                    db.SaveChanges();
                                }


                                //Doy de baja la vencida
                                inscripcion.actual = 0;
                                db.SaveChanges();

                            }
                            //Si la inscripción de este mes le permite ingresar como moroso
                            //NO doy de baja la vencida y genero una nueva
                            else if (inscripcion.moroso_si == 1)
                            {
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
                                
                                db.inscripcion_clase.Add(newInscripcion);
                                db.SaveChanges();
                                }
                            }

                            //No pagó, vencida, era provisoria y sin recargo por asistencia vencida. Baja
                            else if (inscripcion.provisoria == 1 && inscripcion.recargo == 0)
                            {     
                                inscripcion.actual = 0;
                                db.SaveChanges();
                            }
                            //Si no pagó, vencida, provisoria, con recargo y asistencias, se queda como una inscripcion activa para poder luego registrar pago.

                        }
                        //Verifico si es provisoria y si ya cumplió con los 10 dias de vencimiento
                        else if (inscripcion.provisoria == 1 && inscripcion.fecha_desde.Value.AddDays(10)> DateTime.Today)
                        {
                            //Si habia pagado, deja de ser provisoria
                            if (inscripcion.pago_clase.Count > 0)
                            {
                                inscripcion.provisoria = 0;
                                inscripcion.moroso_si = 0;
                                db.SaveChanges();
                            }
                            //Si no habia pagado
                            else
                            {
                                // y tenia asistencias, se le suma el recargo
                                if (inscripcion.asistencia_clase.Count > 0)
                                { 
                                    inscripcion.recargo = 1;
                                    db.SaveChanges();
                                }
                                //Si no puede ser moroso y no asistio nunca se da de baja
                                else if (inscripcion.moroso_si == 0)
                                {
                                    inscripcion.actual = 0;
                                    db.SaveChanges();
                                }
                                
                            }
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


        public DateTime CalcularProxVto(DateTime dt)
        {


            DateTime dtNuevo = dt;
            try
            {
                dtNuevo = dtNuevo.AddMonths(1);
                return dtNuevo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

      

    }
}
