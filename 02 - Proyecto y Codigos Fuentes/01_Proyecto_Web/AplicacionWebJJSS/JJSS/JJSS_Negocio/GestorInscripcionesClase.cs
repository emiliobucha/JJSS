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
    public class GestorInscripcionesClase
    {
        public List<inscripcion_clase> ObtenerInscripcionesClase()
        {
            using (var db = new JJSSEntities())
            {
                return db.inscripcion_clase.ToList();
            }
        }

        public String InscribirAlumnoAClase(int pDNIAlumno, int pClase, string pHora, DateTime pFecha)
        {
            String sReturn = "";
            GestorAlumnos gestorAlumnos = new GestorAlumnos();
            alumno pAlumno = gestorAlumnos.ObtenerAlumnoPorDNI(pDNIAlumno);
            if (pAlumno.id_alumno == 0)
            {
                throw new Exception("El alumno no esta registrado");
            }
            if (modUtilidadesTablas.ToDataTable(ObtenerInscripcionesClase()).Select("id_alumno = " + pAlumno.id_alumno + " and id_clase = " + pClase).Length > 0)
            {
                throw new Exception("Ya se ha inscripto a esa clase");
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
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
 
    }
}
