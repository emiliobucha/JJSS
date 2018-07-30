using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class AlumnoInscripcionClase
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int? id_tipo_documento { get; set; }
        public string tipo_documento { get; set; }
        public string dni { get; set; }
        public int id_clase { get; set; }
        public int id_alumno { get; set; }
        public List<int> inscripciones { get; set; }
        public string inscripto { get; set; }
    }
}
