using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Negocio.Constantes;

namespace JJSS_Negocio.Resultados
{
    public class ObjetoPagable
    {
        public string Nombre { get; set; }
        public ConstantesTipoPago.TipoPago TipoPago { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int Inscripcion { get; set; }
        public int Participante { get; set; }
        public int IdObjeto { get; set; }
    }
}
