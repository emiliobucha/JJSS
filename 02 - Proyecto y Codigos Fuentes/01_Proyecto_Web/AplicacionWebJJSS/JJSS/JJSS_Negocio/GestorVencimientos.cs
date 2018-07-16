using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorVencimientos
    {


        private static string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public string DarBajaInscripcion()
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    ////Alumnos que se pasan de los 10 dias sin pagar y sin asistir.
                    //var inscripcionesPotenciales = db.inscripcion_clase.Where(x => EntityFunctions.AddMonths(, 1))




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

        public string CalcularProximaFechaVencimientoInscripcion()
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {

                    foreach (var inscripcionClase in db.inscripcion_clase)
                    {
                        if (!inscripcionClase.proximo_vencimiento.HasValue) continue;

                        if (inscripcionClase.proximo_vencimiento.Value.AddDays(10) <= DateTime.Today)
                        {

                            //Si realizo pagos dentro la fecha que se vencia (10 dias siguientes) se actualiza su fecha de vencimiento
                            var pagos = db.pago_clase.FirstOrDefault(x =>
                                    x.id_alumno == inscripcionClase.id_alumno &&
                                    x.id_clase == inscripcionClase.id_clase)
                                .detalle_pago_clase.ToList();

                            var mesToday = meses[DateTime.Today.Month - 1];

                            if (pagos.FirstOrDefault(x => x.mes == mesToday) != null)
                            {
                                inscripcionClase.proximo_vencimiento =
                                    CalcularProxVto(inscripcionClase.proximo_vencimiento.Value);
                                db.SaveChanges();
                            }
                            else
                            {
                                //Si no asistio dentro de los dias de vencimiento y 10 dias despues
                                var asistencias = db.asistencia_clase.Where(x =>
                                    x.id_clase == inscripcionClase.id_clase &&
                                    x.id_alumno == inscripcionClase.id_alumno);

                                List<asistencia_clase> lista = new List<asistencia_clase>();


                                foreach (var asistencia in asistencias)
                                {
                                    if (asistencia.fecha_hora.Date > inscripcionClase.proximo_vencimiento.Value.Date && asistencia.fecha_hora.Date <= inscripcionClase.proximo_vencimiento.Value.AddDays(10).Date)
                                    {
                                        lista.Add(asistencia);
                                    }  
                                    
                                }

                                if (lista.Count == 0)
                                {
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
