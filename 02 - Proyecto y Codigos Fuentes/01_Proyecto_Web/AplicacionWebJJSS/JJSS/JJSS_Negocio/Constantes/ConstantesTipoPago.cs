using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Constantes
{
    public static class ConstantesTipoPago
    {
        public static TipoPago TORNEO()
        {
            return new TipoPago {Id = 1, Tipo = "Torneo"};
        }
        public static TipoPago EVENTO()
        {
            return new TipoPago { Id = 2, Tipo = "Evento" };
        }
        public static TipoPago CLASE()
        {
            return new TipoPago { Id = 3, Tipo = "Clase" };
        }

        public class TipoPago
        {
            public string Tipo { get; set; }
            public int Id { get; set; }
        }
    }
}
