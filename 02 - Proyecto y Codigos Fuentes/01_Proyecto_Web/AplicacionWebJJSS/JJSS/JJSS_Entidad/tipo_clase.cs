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
    
    public partial class tipo_clase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipo_clase()
        {
            this.asistencia_clase = new HashSet<asistencia_clase>();
            this.categoria = new HashSet<categoria>();
<<<<<<< HEAD
            this.clase = new HashSet<clase>();
            this.faja = new HashSet<faja>();
=======
>>>>>>> 521db55afd69e5169dac970eda717997e2ecfa67
            this.torneo = new HashSet<torneo>();
            this.faja = new HashSet<faja>();
            this.clase = new HashSet<clase>();
        }
    
        public int id_tipo_clase { get; set; }
        public string nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<asistencia_clase> asistencia_clase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<categoria> categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
<<<<<<< HEAD
        public virtual ICollection<clase> clase { get; set; }
=======
        public virtual ICollection<torneo> torneo { get; set; }
>>>>>>> 521db55afd69e5169dac970eda717997e2ecfa67
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<faja> faja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<clase> clase { get; set; }
    }
}
