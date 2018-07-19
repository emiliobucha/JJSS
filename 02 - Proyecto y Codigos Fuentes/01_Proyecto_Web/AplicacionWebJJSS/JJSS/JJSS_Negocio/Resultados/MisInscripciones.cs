using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class MisInscripciones
    {
        public string nombre { get; set; }
        public string prox_vencimiento { get; set; }
        public int id_inscripcion { get; set; }
        public string tipo_clase { get; set; }
        public string tipo_evento { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string fecha_inscripcion { get; set; }
        public DateTime? dtFechaInscripcion { get; set; }
        public DateTime? dtProxVencimiento { get; set; }
        public DateTime? dtFecha { get; set; }
        public int? pago { get; set; }
        public int? idClase { get; set; }
        public int? insActual { get; set; }
    }
}
