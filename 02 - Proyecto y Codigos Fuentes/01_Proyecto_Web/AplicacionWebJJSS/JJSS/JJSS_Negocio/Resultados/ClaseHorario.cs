using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class ClaseHorario
    {
        public string desde { get; set; }
        public string hasta { get; set; }
        public int id { get; set; }
        public string nombreClase { get; set; }
        public string academia { get; set; }
        public int idClase { get; set; }
        public int? dia { get; set; }
        public string tipoClase { get; set; }
    }
}
