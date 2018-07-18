using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class AlumnoFaja
    {
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string faja { get; set; }
        public DateTime? fecha { get; set; }
        public string tipo { get; set; }
        public int idAlu { get; set; }
        public string fechaParaMostrar { get; set; }
        public int? idTipo { get; set; }
        public string dni { get; set; }
    }
}
