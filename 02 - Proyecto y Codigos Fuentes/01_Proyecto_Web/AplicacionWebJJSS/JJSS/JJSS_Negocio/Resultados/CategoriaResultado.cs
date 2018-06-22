using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class CategoriaResultado
    {
        public string nombre { get; set; }
        public int idCategoria { get; set; }
        public string pesoMin { get; set; }
        public string pesoMax { get; set; }
        public string edadMin { get; set; }
        public string edadMax { get; set; }
        public string disciplina { get; set; }
        public int idDisciplina { get; set; }
        public short? sexo { get; set; }
        public string sexoMostrar { get; set; }
    }
}
