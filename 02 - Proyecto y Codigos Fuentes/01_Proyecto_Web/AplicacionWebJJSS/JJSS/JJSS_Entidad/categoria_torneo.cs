//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JJSS_Entidad
{
    using System;
    using System.Collections.Generic;
    
    public partial class categoria_torneo
    {
        public int id_categoria_torneo { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public Nullable<short> sexo { get; set; }
        public Nullable<int> id_faja { get; set; }
        public Nullable<int> id_resultado { get; set; }
    
        public virtual categoria categoria { get; set; }
        public virtual resultado resultado { get; set; }
    }
}
