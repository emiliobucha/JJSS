using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio
{
    public class CompInscripcionClasePago
    {
        public string cla_nombre { get; set; }
        public string cla_academia { get; set; }
        public string cla_direccion { get; set; }
        public string cla_tipo { get; set; }
        public string cla_precio { get; set; }
        public string par_nombre { get; set; }
        public string par_apellido { get; set; }
        public DateTime? par_fecha_nacD { get; set; }
        public string par_fecha_nac { get; set; }
        public short? par_sexo { get; set; }
        public string par_sexo_nombre { get; set; }
        public string par_faja { get; set; }
        public string par_categoria { get; set; }
        public string par_dni { get; set; }
        public string par_tipo_documento { get; set; }

        public string pag_forma_pago { get; set; }
        public string pag_fecha { get; set; }
        public string pag_mes { get; set; }
    }
}
