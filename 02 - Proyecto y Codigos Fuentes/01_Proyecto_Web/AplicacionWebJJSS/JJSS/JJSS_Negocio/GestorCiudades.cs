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
    public class GestorCiudades
    {

        /*
         * Método que devuelve un listado de todas las ciudades cargadas
         * Retorno: List<ciudad>
         *          Retorna toda lista de ciudades
         */
        public List<ciudad> ObtenerCiudades()
        {
            using (var db = new JJSSEntities())
            {
                return db.ciudad.ToList();
            }
        }

        /*
         * Metodo que nos permite encontrar todas las ciudades que pertenecen a una provincia en particular
         * Parámetros;
         *              pProvincia: entero que indica el id de provincia a cual pertenece esa ciudad
         * Retorno: List<ciudad>
         *          Retorna una lista de todas las ciudades que pertenecen a esa provincia
         */
        public List<ciudad> ObtenerCiudadesPorProvincia(int pProvincia)
        {
            using (var db=new JJSSEntities())
            {
                var ciudadesPorProvincia = from ciudad in db.ciudad
                                           where ciudad.id_provincia == pProvincia
                                           select ciudad;

                return ciudadesPorProvincia.ToList();
            }
        }
    }
}
