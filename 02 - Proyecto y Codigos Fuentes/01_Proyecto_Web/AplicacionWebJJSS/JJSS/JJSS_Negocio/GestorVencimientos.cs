using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio.Constantes;

namespace JJSS_Negocio
{
    public class GestorVencimientos
    {


        public string ActualizarEstadosInscripcion()
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {

                    foreach (var inscripcionClase in db.inscripcion_clase.Where(x=>x.actual == 1))
                    {
                        if (!inscripcionClase.proximo_vencimiento.HasValue) continue;

                        //Si la fecha de vencimiento es hoy o ya paso
                        if (inscripcionClase.proximo_vencimiento.Value.AddDays(10) <= DateTime.Today)
                        {

                            //Si realizo pagos dentro la fecha que se vencia (10 dias siguientes) se actualiza su fecha de vencimiento
                            var pagos = db.pago_clase.FirstOrDefault(x =>
                                x.id_alumno == inscripcionClase.id_alumno && x.id_clase == inscripcionClase.id_clase);

                           List<detalle_pago_clase> detalles = new List<detalle_pago_clase>();


                            if (pagos != null) detalles = pagos.detalle_pago_clase.ToList(); 
                            
                            ////Los pagos tienen el mes en un string
                            //var mesToday = meses[DateTime.Today.Month - 1];

                            if (detalles.FirstOrDefault(x => x.fecha_vencimiento_cumple == inscripcionClase.proximo_vencimiento) != null)
                            {
                                
                                inscripcionClase.fecha_desde = inscripcionClase.proximo_vencimiento;
                                inscripcionClase.proximo_vencimiento =
                                    CalcularProxVto(inscripcionClase.proximo_vencimiento.Value);
                                inscripcionClase.recargo = 0;
                                inscripcionClase.actual = 1;
                                db.SaveChanges();
                            }
                            else
                            {

                                var asistencias = db.asistencia_clase.Where(x => x.id_clase == inscripcionClase.id_clase && x.id_alumno == inscripcionClase.id_alumno);

                                //Verifico si asistio en ese periodo de tiempo
                                var asistio = asistencias.Any(asistencia => DbFunctions.TruncateTime(asistencia.fecha_hora) > DbFunctions.TruncateTime(inscripcionClase.proximo_vencimiento) &&
                                                                            DbFunctions.TruncateTime(asistencia.fecha_hora) <= DbFunctions.TruncateTime(DbFunctions.AddDays(inscripcionClase.proximo_vencimiento,10)));

                             
                                if (asistio)
                                {
                                    inscripcionClase.recargo = 1;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    //Si no asistio dentro de los dias de vencimiento y 10 dias despues y no pagó se da de baja
                                    inscripcionClase.actual = 0;
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
                    var alumnosActivos = db.alumno.Where(x=>x.baja_logica == 1 && x.id_estado != ConstantesEstado.ALUMNOS_DE_BAJA);
                    foreach (var alumno in alumnosActivos)
                    {
                        var inscripciones = alumno.inscripcion_clase;
                        if (inscripciones.Count == 0) continue;
  
                        if (inscripciones.Any(x=>x.recargo == 1 && x.actual == 1))
                        {
                            alumno.id_estado = ConstantesEstado.ALUMNOS_MOROSO;
                        }
                        if (inscripciones.All(x => x.actual == 0))
                        {
                            alumno.id_estado = ConstantesEstado.ALUMNOS_INACTIVO;
                        }
                        if (inscripciones.Where( x=>x.recargo == 1).All(x=> x.actual == 0) && inscripciones.Where(x=> x.actual ==1).All(x=>x.recargo == 0))
                        {
                            alumno.id_estado = ConstantesEstado.ALUMNOS_ACTIVO;
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
