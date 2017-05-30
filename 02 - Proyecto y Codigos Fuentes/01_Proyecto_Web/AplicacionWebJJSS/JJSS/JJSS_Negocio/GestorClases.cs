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
     * Clase que nos permite gestionar Torneos
     */
    public class GestorClases
    {


        /*
         * 
         */

        public String GenerarNuevaClase(int pTipo, double pPrecio, DataTable pHorarios, string pNombre)
        {
            String sReturn = "";
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

        public List<tipo_clase> ObtenerTipos()
        {
            using (var db = new JJSSEntities())
            {
                return db.tipo_clase.ToList();
            }

        }
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
    }
}
