using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorTipoClase
    {

        public List<tipo_clase> ObtenerTipoClase()
        {
            using (var db = new JJSSEntities())
            {
                return db.tipo_clase.ToList();
            }
        }
    }
}
