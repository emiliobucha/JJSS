using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio
{
    public class ListadoAsistencia
    {
        public string horario_nombre { get; set; }
        public string cla_nombre { get; set; }
        public string cla_tipo { get; set; }
        public string cla_profesor { get; set; }
        public DateTime? cla_fechaD { get; set; }
        public string cla_fecha { get; set; }
        public string alu_nombre { get; set; }
        public string alu_apellido { get; set; }
        public string alu_dni { get; set; }
        public short? alu_sexoI { get; set; }
        public string alu_sexo { get; set; }
        public string alu_telefono { get; set; }
        public TimeSpan? alu_horaT { get; set; }
        public string alu_hora { get; set; }
      
    }
}
