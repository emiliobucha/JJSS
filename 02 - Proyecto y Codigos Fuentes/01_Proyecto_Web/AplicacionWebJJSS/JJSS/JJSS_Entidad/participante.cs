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
    
    public partial class participante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public participante()
        {
            this.inscripcion = new HashSet<inscripcion>();
            this.lucha = new HashSet<lucha>();
            this.lucha1 = new HashSet<lucha>();
        }
    
        public int id_participante { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public Nullable<int> id_faja { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public Nullable<short> sexo { get; set; }
        public int dni { get; set; }
        public Nullable<int> id_alumno { get; set; }
    
        public virtual faja faja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inscripcion> inscripcion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lucha> lucha { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lucha> lucha1 { get; set; }
        public virtual categoria categoria { get; set; }
    }
}
