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

        /*
         * Método que devuelve un listado de todas los tipos de clases cargadas
         * Retorno: List<tipo_clase>
         *          Retorna toda lista de tipos de clases
         */
        public List<tipo_clase> ObtenerTipoClase()
        {
            using (var db = new JJSSEntities())
            {
                return db.tipo_clase.ToList();
            }
        }
    }
}
