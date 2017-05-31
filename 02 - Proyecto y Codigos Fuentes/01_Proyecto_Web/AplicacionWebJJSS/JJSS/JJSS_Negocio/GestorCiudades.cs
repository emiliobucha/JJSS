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


        public List<ciudad> ObtenerCiudades()
        {
            using (var db = new JJSSEntities())
            {
                return db.ciudad.ToList();
            }
        }

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
