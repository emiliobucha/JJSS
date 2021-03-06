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
            this.alumno_imagen = new HashSet<alumno_imagen>();
            this.alumnoxfaja = new HashSet<alumnoxfaja>();
            this.asistencia_clase = new HashSet<asistencia_clase>();
            this.inscripcion_clase = new HashSet<inscripcion_clase>();
            this.participante_evento = new HashSet<participante_evento>();
            this.participante = new HashSet<participante>();
        }
    
        public int id_alumno { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public Nullable<short> sexo { get; set; }
        public string dni { get; set; }
        public long telefono { get; set; }
        public string mail { get; set; }
        public Nullable<int> id_direccion { get; set; }
        public System.DateTime fecha_ingreso { get; set; }
        public long telefono_emergencia { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public Nullable<short> baja_logica { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<int> id_tipo_documento { get; set; }
        public Nullable<int> id_pais { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alumno_imagen> alumno_imagen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alumnoxfaja> alumnoxfaja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<asistencia_clase> asistencia_clase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inscripcion_clase> inscripcion_clase { get; set; }
        public virtual direccion direccion { get; set; }
        public virtual estado estado { get; set; }
        public virtual pais pais { get; set; }
        public virtual tipo_documento tipo_documento { get; set; }
        public virtual seguridad_usuario seguridad_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<participante_evento> participante_evento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<participante> participante { get; set; }
    }
}
