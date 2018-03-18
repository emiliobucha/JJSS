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

        private GestorClases gestorClase;

        /*
         * Método que devuelve un listado de todas los tipos de clases cargadas
         * Retorno: List<tipo_clase>
         *          Retorna lista de tipos de clases
         */
        public List<tipo_clase> ObtenerTipoClase()
        {
            using (var db = new JJSSEntities())
            {
                return db.tipo_clase.ToList();
            }
        }


        /*
         * Método que devuelve un tipo de clase segun su id
         * Parametros: pIDTipoClase: entero que representa el id del tipo de clase a buscar
         * Retorno: tipoclase
         *          null
         */
        public tipo_clase ObtenerTipoClasePorID(int pIDTipoClase)
        {
            using (var db = new JJSSEntities())
            {
                return db.tipo_clase.Find(pIDTipoClase);
            }
        }

        /*
         * Método que devuelve un listado de las fajas
         * Parametro: pIdClase: entero - Id de la clase
         * Retorno: List<faja>
         *          Retorna lista de fajas
         */
        public List<faja> ObtenerFajasSegunTipoClase(int pIdClase)
        {
            using (var db = new JJSSEntities())
            {
                gestorClase = new GestorClases();
                clase claseSeleccionada = gestorClase.ObtenerClasePorId(pIdClase);

                var fajas = from faj in db.faja
                            where faj.id_tipo_clase==claseSeleccionada.id_tipo_clase
                            select faj;
                return fajas.ToList();
            }
        }
    }
}
