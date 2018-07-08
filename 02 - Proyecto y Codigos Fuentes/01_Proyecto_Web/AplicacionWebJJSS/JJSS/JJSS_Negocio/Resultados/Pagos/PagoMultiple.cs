using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Negocio.Constantes;

namespace JJSS_Negocio.Resultados.Pagos
{
    public class PagoMultiple
    {
        public DateTime Fecha { get; set; }
        public decimal MontoTotal { get; set; }
        public string Descripcion { get; set; }
        public int FormaPago { get; set; }
        public string FormaPagoString { get; set; }
        public int? IdUsuario { get; set; }
        public int Id { get; set; }
        public IList<ObjetoPagable> ObjetosPagables { get; set; }
        public int? TipoDocumento { get; set; }
        public string Dni { get; set; }
        public string NombreCompleto { get; set; }
        public string EstadoMP { get; set; }

        public PagoMultiple(IList<ObjetoPagable> objetosPagables, int formaPago, int? usuario, int? tipoDocumento, string dni, string nombre)
        {
            decimal total = 0;
            var descripcion = "";
            foreach (var objetoPagable in objetosPagables)
            {
                total += objetoPagable.Monto;
                descripcion += objetoPagable.GetDescripcion() + Environment.NewLine;
            }

            ObjetosPagables = objetosPagables;
            FormaPago = formaPago;
            IdUsuario = usuario;
            Fecha = DateTime.Now;
            TipoDocumento = tipoDocumento;
            Dni = dni;
            NombreCompleto = nombre;
            MontoTotal = total;
            Descripcion = descripcion;
        }
    }
}
