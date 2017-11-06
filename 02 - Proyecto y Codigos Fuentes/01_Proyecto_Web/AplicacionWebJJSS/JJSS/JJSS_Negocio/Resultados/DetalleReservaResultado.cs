using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class DetalleReservaResultado
    {
        public int? cantidad { get; set; }
        public decimal? precio_venta { get; set; }
        public string  nombre { get; set; }
        public int id_detalle { get; set; }
        public decimal? total { get; set; }
    }
}
