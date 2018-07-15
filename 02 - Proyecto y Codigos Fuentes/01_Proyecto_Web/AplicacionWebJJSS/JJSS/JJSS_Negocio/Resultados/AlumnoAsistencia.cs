using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class AlumnoAsistencia
    {
        public string alu_nombre { get; set; }
        public string alu_apellido { get; set; }
        public string alu_documento { get; set; }
        public int alu_id { get; set; }
        public string alu_tipoDocumento { get; set; }
        public Boolean asistio { get; set; }
    }
}
