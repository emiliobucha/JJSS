using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Data;
using System.Data.Linq;
using System.Configuration;

namespace JJSS_Negocio
{
    /*
     * Clase que nos permite gestionar Clases
     */
    public class GestorClases
    {


        /*
         * Genera una nueva clase
         * Parametros:
         *              pTipo: Entero Indica el tipo de clase a la que pertenece
         *              pPrecio: Double Indica el precio que cuesta inscribirse a esa clase en particular
         *              pHorarios: DataTable los horarios que han sido cargados en la grilla de horarios de clase
         *               pNombre: string nombre indentificatorio para la clase
         *              pUbicacion: Entero indica la ubicacion donde se da la clase
         * Retornos:String 
         *          "" - Todo salió bien
         *          mensaje de excepcion
         *          
         */

        public String GenerarNuevaClase(int pTipo, double pPrecio, DataTable pHorarios, string pNombre, int pUbicacion, int pProfe)
        {
            String sReturn = "";
            try
            {
                using (var db = new JJSSEntities())
                {

                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        clase nuevaClase = new clase()
                        {
                            id_tipo_clase = pTipo,
                            precio = pPrecio,
                            nombre = pNombre,
                            id_ubicacion = pUbicacion,
                            id_profe = pProfe,
                            baja_logica = 1 //clase disponible
                        };

                        db.clase.Add(nuevaClase);
                        db.SaveChanges();

                        foreach (DataRow drAux in pHorarios.Rows)
                        {
                            horario nuevoHorario = new horario()
                            {
                                hora_desde = drAux["hora_desde"].ToString(),
                                hora_hasta = drAux["hora_hasta"].ToString(),
                                id_clase = nuevaClase.id_clase,
                                nombre_dia = drAux["nombre_dia"].ToString(),
                                dia = int.Parse(drAux["dia"].ToString())

                            };

                            db.horario.Add(nuevoHorario);

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
         *Método que devuelve los tipos de clases 
         *Retornos:List <tipo_clase>
         *          Listado de todos los tipos de clases
         *
      */
        public List<tipo_clase> ObtenerTipos()
        {
            using (var db = new JJSSEntities())
            {
                return db.tipo_clase.ToList();
            }

        }

        /*
         * Método que devuelve los horarios disponibles de una clase, si esta no existe devuelve una tabla vacia que nos permite ver el formato de dicha tabla
         * Parametros:
         *              int pID indica el id de clase a la que se quiere ver los horarios
         * Retorno:
         *          Datatable con todos los horarios
         */
        public DataTable ObtenerTablaHorarios(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var horarios = from hor in db.horario
                               where hor.id_clase == pID
                               select hor;

                return modUtilidadesTablas.ToDataTable(horarios.ToList());
            }
        }


        /*
         * Obtiene un listado de todas las clases que están disponibles
         * Retorno: List<clase> 
         *          Listado de todas las clases
         */
        public List<Object> ObtenerClasesDisponibles()
        {
            using (var db = new JJSSEntities())
            {
                var clasesDisponibles = from clases in db.clase
                                        join ubic in db.academia on clases.id_ubicacion equals ubic.id_academia
                                        join tipo in db.tipo_clase on clases.id_tipo_clase equals tipo.id_tipo_clase
                                        where clases.baja_logica == 1
                                        select new
                                        {
                                            id_clase = clases.id_clase,
                                            nombre = clases.nombre,
                                            precio = clases.precio,
                                            tipo_clase = tipo.nombre,
                                            ubicacion = ubic.nombre,
                                        };
                List<Object> claseLista = clasesDisponibles.ToList<Object>();

                return claseLista;
            }
        }


        /*
         * Obtiene los datos de una clase dado un id
         * Parametros: 
         *              pId : id de la clase a buscar
         * Retorno
         *              la clase encontrada
         *              null si no encuentra
         * */
        public clase ObtenerClasePorId(int pId)
        {
            using (var db = new JJSSEntities())
            {
                return db.clase.Find(pId);
            }
        }

        /*
         * Actualiza el precio y los horarios de una clase
         * Parametros: 
         *              pId : Entero id de la clase que se va a actualizar
         *              pHorarios : DataTable tabla con los nuevos horarios
         *              pPrecio : Double precio a actualizar
         * Retorno: 
         *          string con el mensaje de error de la BD
         *          "" si esta correcto
         * 
         * */
        public string modificarClase(int pId, DataTable pHorarios, double pPrecio, int pProfe)
        {
            string sReturn = "";
            try
            {
                using (var db = new JJSSEntities())
                {
                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        //eliminar los horarios de esa clase
                        var horarioEncontrado = from hor in db.horario
                                                where hor.id_clase == pId
                                                select hor;
                        while (horarioEncontrado.Count() > 0)
                        {
                            db.horario.Remove(horarioEncontrado.First());
                            db.SaveChanges();
                            horarioEncontrado = from hor in db.horario
                                                where hor.id_clase == pId
                                                select hor;
                        }

                        //busca la clase y actualiza el precio y profe
                        clase claseEncontrada = db.clase.Find(pId);
                        claseEncontrada.precio = pPrecio;
                        claseEncontrada.id_profe = pProfe;
                        db.SaveChanges();

                        //agrega los nuevos horarios
                        foreach (DataRow drAux in pHorarios.Rows)
                        {
                            horario nuevoHorario = new horario()
                            {
                                hora_desde = drAux["hora_desde"].ToString(),
                                hora_hasta = drAux["hora_hasta"].ToString(),
                                id_clase = pId,
                                nombre_dia = drAux["nombre_dia"].ToString(),
                                dia = int.Parse(drAux["dia"].ToString())
                            };

                            db.horario.Add(nuevoHorario);

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


        public string eliminarClase(int pIdClase)
        {
            string sReturn = "";

            try
            {
                using (var db = new JJSSEntities())
                {
                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        clase claseEncontrada = db.clase.Find(pIdClase);
                        claseEncontrada.baja_logica = 0;

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


        public List<tipo_clase> ObtenerTipoClases()
        {
            GestorTipoClase gestorTipoClase = new GestorTipoClase();
            return gestorTipoClase.ObtenerTipoClase();
        }

        public List<academia> ObtenerAcademias()
        {
            GestorAcademias gestorAcademias = new GestorAcademias();
            return gestorAcademias.ObtenerAcademias();
        }

        public List<profesor> ObtenerProfesores()
        {
            GestorProfesores gestorProfesores = new GestorProfesores();
            return gestorProfesores.ObtenerProfesores();
        }

        /*
         * Metodo que valida si existe una clase en el mismo horario que se quiere agregar
         * Parametros:  pDTHorarios: DataTable contiene los dias y horarios que se quieren agregar
         *              pIdUbicacion: entero que representa el ID de la ubicacion de la clase
         * Retornos: boolean    false: ya existe una clase en ese horario
         *                      true: los horarios estan todos disponibles
         * 
         */
        public Boolean validarDisponibilidadHorario(DataTable pDTHorarios, int pIdUbicacion)
        {
            using (var db = new JJSSEntities())
            {

                for (int j = 0; j < pDTHorarios.Rows.Count; j++)
                {
                    int dia = int.Parse(pDTHorarios.Rows[j]["dia"].ToString());
                    var claseEncontrada = from clase in db.clase
                                          join hora in db.horario on clase.id_clase equals hora.id_clase
                                          where clase.id_ubicacion == pIdUbicacion && hora.dia == dia
                                          select new
                                          {
                                              desde = hora.hora_desde,
                                              hasta = hora.hora_hasta
                                          };

                    DataTable dtClases = modUtilidadesTablas.ToDataTable(claseEncontrada.ToList());
                    for (int i = 0; i < dtClases.Rows.Count; i++)
                    {
                        DataRow dr = dtClases.Rows[i];
                        if (dr["desde"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) == 0) return false;
                        if (dr["desde"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) < 0 && dr["hasta"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) > 0) return false;
                        if (dr["hasta"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) == 0) return false;
                        if (dr["desde"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) < 0 && dr["hasta"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) > 0) return false;
                        if (dr["desde"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) > 0 && dr["hasta"].ToString().CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) < 0) return false;

                    }

                }
            }
            return true;
        }

        /*
         * Metodo que devuelve el valor de recargo a pagar segun el dia de inscripcion
         * Parametros:  pIdClase : entero - id de la clase a la que esta inscripto el alumno
         *              pIdAlumno : entero - id del alumno
         * Retorno: double >=0 - representa el valor del recargo 
         *                  <= no esta inscripto a esa clase
         * 
         */
        public double calcularRecargo(int pIdClase, int pIdAlumno)
        {
            inscripcion_clase inscripcionDelAlumno;
            int diaMaximo = 0;
            using (var db = new JJSSEntities())
            {
                GestorInscripcionesClase ins = new GestorInscripcionesClase();
                inscripcionDelAlumno = ins.ObtenerAlumnoInscripto(pIdAlumno, pIdClase);
            }
            if (inscripcionDelAlumno == null)
            {
                return -1;
            }
            else
            {
                DateTime fechaInscripcion = (DateTime)inscripcionDelAlumno.fecha;
                
                if ((fechaInscripcion.Month == 1 || fechaInscripcion.Month == 3 || fechaInscripcion.Month == 5 || fechaInscripcion.Month == 7 || fechaInscripcion.Month == 8 ||
                    fechaInscripcion.Month == 10 || fechaInscripcion.Month == 12) && fechaInscripcion.Day > 26) //mes tiene 31 dias
                {
                    diaMaximo = 5 - (31 - fechaInscripcion.Day);
                }
                else if ((fechaInscripcion.Month == 4 || fechaInscripcion.Month == 6 || fechaInscripcion.Month == 9 || fechaInscripcion.Month == 11) && fechaInscripcion.Day > 25)//mes tiene 30 dias
                {
                    diaMaximo = 5 - (30 - fechaInscripcion.Day);
                }
                else if (fechaInscripcion.Month == 2 && fechaInscripcion.Day > 23) //mes tiene 28 dias
                {
                    diaMaximo = 5 - (28 - fechaInscripcion.Day);
                }
                else
                {
                    diaMaximo = fechaInscripcion.Day + 5;
                }

                if (diaMaximo < DateTime.Today.Day) // se cobra recargo
                {
                    //+de donde saco el monto recargo?
                    return 100;
                }
            }

            return 0;
        }

    }
}
