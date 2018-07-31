using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class AlumnoFajaInscripciones
    {
        public string inscr_apellido { get; set; }
        public string inscr_nombre { get; set; }
        public string inscr_sexo { get; set; }

        public string inscr_faja { get; set; }

        public DateTime? inscr_fecha_nacD { get; set; }
        public string inscr_fecha_nac { get; set; }

        public int inscr_id_alumno { get; set; }

        public int? inscr_tipoI { get; set; }
        public string inscr_tipo { get; set; }

        public string inscr_dni { get; set; }

        public int? pago { get; set; }
        public string inscr_pago { get; set; }

        public int? recargo { get; set; }
        public string inscr_recargo { get; set; }

        public DateTime? inscr_fecha_vto { get; set; }
        public string inscr_fecha_vto_mensual { get; set; }


        public DateTime? inscr_fecha_desde { get; set; }
        public string inscr_fecha_desde_mensual { get; set; }


    }
}
