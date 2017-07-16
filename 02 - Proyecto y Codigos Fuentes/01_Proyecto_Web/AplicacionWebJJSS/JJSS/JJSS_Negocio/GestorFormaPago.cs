using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorFormaPago
    {
        /**
         * Metodo que devuelve una lista de las formas de pago
         * */
        public List<forma_pago> ObtenerFormasPago()
        {
            using (var db = new JJSSEntities())
            {
                return db.forma_pago.ToList();
            }
        }
    }
}
