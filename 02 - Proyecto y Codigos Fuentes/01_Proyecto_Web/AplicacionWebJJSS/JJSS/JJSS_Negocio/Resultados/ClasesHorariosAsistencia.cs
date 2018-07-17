using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
   public class ClasesHorariosAsistencia
    {
        public int idHorario { get; set; }
        public int idClase { get; set; }
        public string dia { get; set; }
        public string desde { get; set; }
        public string hasta { get; set; }
        public int tipoClase { get; set; }
        public string nombreClase { get;set; }
    }
}
