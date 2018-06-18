using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorTipoDocumento
    {
        public List<tipo_documento> ObtenerTiposDocumentos()
        {
            try
            {
                using (var db = new JJSSEntities())
                {

                    var list = db.tipo_documento.ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                return new List<tipo_documento>();
            }
        }

    }
}
