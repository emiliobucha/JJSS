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
    
    public partial class alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public alumno()
        {
            this.participante = new HashSet<participante>();
            this.inscripcion_clase = new HashSet<inscripcion_clase>();
            this.alumno_imagen = new HashSet<alumno_imagen>();
            this.pago_clase = new HashSet<pago_clase>();
            this.asistencia_clase = new HashSet<asistencia_clase>();
            this.alumnoxfaja = new HashSet<alumnoxfaja>();
        }
    
        public int id_alumno { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public Nullable<short> sexo { get; set; }
        public int dni { get; set; }
        public int telefono { get; set; }
        public string mail { get; set; }
        public Nullable<int> id_direccion { get; set; }
        public System.DateTime fecha_ingreso { get; set; }
        public int telefono_emergencia { get; set; }
        public Nullable<int> id_usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<participante> participante { get; set; }
        public virtual direccion direccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inscripcion_clase> inscripcion_clase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alumno_imagen> alumno_imagen { get; set; }
        public virtual seguridad_usuario seguridad_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pago_clase> pago_clase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<asistencia_clase> asistencia_clase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alumnoxfaja> alumnoxfaja { get; set; }
    }
}
