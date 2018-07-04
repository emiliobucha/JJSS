using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
   public  class PersonaResultado
    {
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string dni { get; set; }
        public int id_tipo_documento { get; set; }
        public string tipo_documento { get; set; }

        public class ProfesorResultado: PersonaResultado
        {
            public int id_profesor { get; set; }
        }
        public class AlumnoResultado : PersonaResultado
        {
            public int id_alumno { get; set; }
            public string estado { get; set; }
            public int id_estado { get; set; }
            public string inscripto { get; set; }
        }

    }
}
