using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class ClasesDisponibles
    {
        public int id_clase { get; set; }
        public string nombre { get; set; }
        public double? precio { get; set; }
        public string tipo_clase { get; set; }
        public string ubicacion { get; set; }
        public string profesor { get; set; }
        public int id_profesor { get; set; }
        public int id_academia { get; set; }

    }
}
