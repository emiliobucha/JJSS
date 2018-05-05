using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio
{
    public static class modValidaciones
    {
        public static Boolean validarFormatoDocumento(string numero, int idDni)
        {
            if (idDni == Constantes.TipoDocumento.DOCUMENTO_NACIONAL_IDENTIDAD)
            {
                return int.TryParse(numero, out int a);
            }
            else
            {
                return true;
            }
        }
    }
}
