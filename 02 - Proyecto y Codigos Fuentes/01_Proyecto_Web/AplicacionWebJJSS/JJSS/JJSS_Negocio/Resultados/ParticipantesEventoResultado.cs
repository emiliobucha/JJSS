using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio
{
    public class ParticipantesEventoResultado
    {
        public string ev_nombre { get; set; }
        public string ev_sede { get; set; }
        public string ev_direccion { get; set; }
        public DateTime? ev_fechaD { get; set; }
        public string ev_fecha { get; set; }
        public string ev_hora { get; set; }
        public string ev_tipo { get; set; }
        public string par_nombre { get; set; }
        public string par_apellido { get; set; }
        public DateTime? par_fecha_nacD { get; set; }
        public string par_fecha_nac { get; set; }
        public short? par_sexo { get; set; }
        public string par_sexo_nombre { get; set; }
        public string par_dni { get; set; }
        public string par_tipo_documento { get; set; }
    }
}
