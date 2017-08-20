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
    
    public partial class torneo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public torneo()
        {
            this.inscripcion = new HashSet<inscripcion>();
            this.lucha = new HashSet<lucha>();
            this.torneo_imagen = new HashSet<torneo_imagen>();
        }
    
        public int id_torneo { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string nombre { get; set; }
        public string hora { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<int> id_sede { get; set; }
        public Nullable<System.DateTime> fecha_cierre { get; set; }
        public string hora_cierre { get; set; }
        public Nullable<decimal> precio_absoluto { get; set; }
        public Nullable<decimal> precio_categoria { get; set; }
        public Nullable<int> id_tipo_clase { get; set; }
    
        public virtual estado estado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inscripcion> inscripcion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lucha> lucha { get; set; }
        public virtual sede sede { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<torneo_imagen> torneo_imagen { get; set; }
        public virtual tipo_clase tipo_clase { get; set; }
    }
}
