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
    
    public partial class resultado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public resultado()
        {
            this.lucha = new HashSet<lucha>();
        }
    
        public int id_resultado { get; set; }
        public Nullable<int> id_torneo { get; set; }
        public Nullable<int> id_categoria_torneo { get; set; }
        public Nullable<int> id_primer { get; set; }
        public Nullable<int> id_segundo { get; set; }
        public Nullable<int> id_tercero1 { get; set; }
        public Nullable<int> id_tercero2 { get; set; }
    
        public virtual categoria_torneo categoria_torneo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lucha> lucha { get; set; }
        public virtual participante participante { get; set; }
        public virtual participante participante1 { get; set; }
        public virtual participante participante2 { get; set; }
        public virtual participante participante3 { get; set; }
        public virtual torneo torneo { get; set; }
    }
}
