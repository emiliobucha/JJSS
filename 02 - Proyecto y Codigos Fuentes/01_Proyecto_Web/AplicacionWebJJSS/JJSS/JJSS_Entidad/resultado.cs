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
            this.categoria_torneo = new HashSet<categoria_torneo>();
        }
    
        public int id_resultado { get; set; }
        public Nullable<short> tipo_finalizacion { get; set; }
        public string tiempo { get; set; }
        public Nullable<int> punto_participante_1 { get; set; }
        public Nullable<int> punto_participante_2 { get; set; }
        public Nullable<int> id_ganador { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lucha> lucha { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<categoria_torneo> categoria_torneo { get; set; }
    }
}
