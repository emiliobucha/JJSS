﻿using System;
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
         * Retornos:String  
         *          "" - Transacción completada correctamente
         *          Mensaje de Excepcion de transaccion
         * Excepciones:
         *              Alumno no Registrado
         *              Ya esta inscripto el alumno
         *  
         */
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


        /*
         * Obtener inscripcion de un alumno a una clase
         * Parametros:
         *              pID: entero que representa el id del alumno
         *              pIDClase: entero que representa el id de la clase 
         * Retornos:Inscripcion_clase
         *          Inscripcion del alumno a dicha clase pudiendo ser nulo el resultado si no estaba inscripto
         *              
         */
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
