﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class CategoriasTorneoResultado
    {
        public string nombreCategoria { get; set; }
        public string nombreFaja { get; set; }
        public int idCategoriaTorneo { get; set; }
        public string nombreParaMostrar { get; set; }
        public short? sexo { get; set; }
    }
}
