using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio
{
    public class ParticipantesTorneoResultado
    {
        public string tor_nombre { get; set; }
        public string tor_sede { get; set; }
        public string tor_direccion { get; set; }
        public DateTime? tor_fechaD { get; set; }
        public string tor_fecha { get; set; }
        public string tor_hora { get; set; }
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
        public string par_peso { get; set; }
        public short? inscr_pagoI { get; set; }
        public int? id_alumno { get; set; }
        public string inscr_pago { get; set; }
        public string par_alumno { get; set; }
    }
}
