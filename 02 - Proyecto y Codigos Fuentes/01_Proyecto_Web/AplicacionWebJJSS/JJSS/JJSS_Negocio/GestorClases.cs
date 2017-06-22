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

        public String GenerarNuevaClase(int pTipo, double pPrecio, DataTable pHorarios, string pNombre, int pUbicacion)
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
                            baja_logica=1 //clase disponible
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
                                nombre_dia = drAux["nombre_dia"].ToString()

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
        public List<clase> ObtenerClases()
        {
            using (var db = new JJSSEntities())
            {
                var clasesDisponibles = from clases in db.clase
                                        where clases.baja_logica == 1
                                        select clases;
                return clasesDisponibles.ToList();
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
        public string modificarClase(int pId, DataTable pHorarios, double pPrecio)
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
                        while (horarioEncontrado.Count()>0)
                        {
                            db.horario.Remove(horarioEncontrado.First());
                            db.SaveChanges();
                            horarioEncontrado = from hor in db.horario
                                                where hor.id_clase == pId
                                                select hor;
                        }

                        //busca la clase y actualiza el precio
                        clase claseEncontrada = db.clase.Find(pId);
                        claseEncontrada.precio = pPrecio;
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
                                dia= int.Parse(drAux["dia"].ToString())
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
    }
}
