using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class InscripcionesClase
    {
        public string inscr_faja { get; set; }

        public int? pago { get; set; }
        public string inscr_pago { get; set; }

        public int? recargo { get; set; }
        public string inscr_recargo { get; set; }

        public DateTime? inscr_fecha_vto { get; set; }
        public string inscr_fecha_vto_mensual { get; set; }

        public DateTime? inscr_fecha_desde { get; set; }
        public string inscr_fecha_desde_mensual { get; set; }

        public int id_inscripcion { get; set; }
        public string nombre { get; set; }
        public string tipo_clase { get; set; }
        public int actual { get; set; }
        public int id_clase { get; set; }
    }
}
