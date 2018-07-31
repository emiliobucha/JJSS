using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Negocio.Constantes;

namespace JJSS_Negocio.Resultados.Pagos
{
    public class ObjetoPagable
    {
        public string Nombre { get; set; }
        public ConstantesTipoPago.TipoPago TipoPago { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int Inscripcion { get; set; }
        public int Participante { get; set; }
        public string NombreParticipante { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }

        public bool MesSiguiente { get; set; }

        public int IdObjeto { get; set; }
        public string DescripcionObjeto { get; set; }
        public string MontoString { get; set; }
        public DateTime? FechaPago { get; set; }
        public short? Pago { get; set; }
        public string PagoString { get; set; }

        public string GetDescripcion()
        {
            return TipoPago.Tipo + " " + Nombre + " Fecha:" + Fecha.Date.ToString("dd/MM/yyyy");
        }



    }
}
