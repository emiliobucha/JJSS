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
    
    public partial class producto_imagen
    {
        public int id_productoimagen { get; set; }
        public byte[] imagen { get; set; }
        public Nullable<int> id_producto { get; set; }
    
        public virtual producto producto { get; set; }
    }
}
