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
           if (pAlumno == null)
            {
                throw new Exception("El alumno no esta registrado");
            }
            if (ObtenerAlumnoInscripto(pAlumno.id_alumno, pClase) !=null)
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

        public inscripcion_clase ObtenerAlumnoInscripto(int pID,int pIDClase)
        {
            using (var db = new JJSSEntities())
            {
                var inscripcion = from ic in db.inscripcion_clase
                                  where ic.id_clase == pIDClase &&
                                         ic.id_alumno == pID
                                  select ic;
                return inscripcion.FirstOrDefault();
            }
        }
    }
}
