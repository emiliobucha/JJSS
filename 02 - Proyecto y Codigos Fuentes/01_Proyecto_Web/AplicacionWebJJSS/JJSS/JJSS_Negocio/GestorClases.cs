using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Data;
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
         * Retornos:String 
         *          "" - Todo salió bien
         *          mensaje de excepcion
         *          
         */

        public String GenerarNuevaClase(int pTipo, double pPrecio, DataTable pHorarios, string pNombre)
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
                            nombre = pNombre

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
         * Obtiene un listado de todas las clases
         * Retorno: List<clase> 
         *          Listado de todas las clases
         */
        public List<clase> ObtenerClases()
        {
            using (var db = new JJSSEntities())
            {
                return db.clase.ToList();
            }
        }
    }
}
