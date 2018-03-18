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
    public class GestorProvincias
    {
        /*
         * Método que nos permite obtener todas las provincias registradoas
         * Retorno: List<provincia>
         *          Listado de todas las provincias registradas
         */
        public List<provincia> ObtenerProvincias()
        {
            using (var db = new JJSSEntities())
            {
                var provincias = from prov in db.provincia
                                 orderby prov.nombre
                                 select prov;
                return provincias.ToList();
            }
        }
    }
}
