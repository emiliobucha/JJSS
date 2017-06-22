using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JJSS_Entidad;

using System.Data;


namespace JJSS_Negocio
{
   
    public class Sesion
    {

        public String estado { get; set; }

        public seguridad_usuario usuario { get; set; }

        public DataTable permisos { get; set; }

        public void New()
        {
            estado = "CERRADA";
            permisos = null;
            usuario = null;
        }

        




    }
}
