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
    
    public partial class detalle_compra
    {
        public int id_detalle { get; set; }
        public Nullable<int> id_compra { get; set; }
        public Nullable<int> id_producto { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<int> cantidad { get; set; }
    
        public virtual compra compra { get; set; }
        public virtual producto producto { get; set; }
    }
}
