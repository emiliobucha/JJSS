using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class HorariosResultado
    {
        public int id { get; set; }
        public string nombre_horario { get; set; }
        public int? id_clase { get; set; }
    }
}
