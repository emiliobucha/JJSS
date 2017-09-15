using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorAcademias
    {
        /*
         * Método que devuelve un listado de todas las academias cargadas
         * Retorno: List<academia>
         *          Retorna toda lista de academias
         */
        public List<academia> ObtenerAcademias()
        {
            using (var db = new JJSSEntities())
            {
                return db.academia.ToList();
            }
        }


        /*
         * Método que devuelve un tipo de clase segun su id
         * Parametros: pIDAcademia: entero que representa el id del tipo de clase a buscar
         * Retorno: tipoclase
         *          null
         */
        public academia ObtenerAcademiasPorID(int pIDAcademia)
        {
            using (var db = new JJSSEntities())
            {

                return db.academia.Find(pIDAcademia);

            }
        }
    }
}
