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
    
    public partial class profesor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public profesor()
        {
            this.profesor_imagen = new HashSet<profesor_imagen>();
        }
    
        public int id_profesor { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public Nullable<int> id_faja { get; set; }
        public Nullable<short> sexo { get; set; }
        public int dni { get; set; }
        public long telefono { get; set; }
        public string mail { get; set; }
        public long telefono_emergencia { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public System.DateTime fecha_ingreso { get; set; }
        public Nullable<int> id_direccion { get; set; }
    
        public virtual direccion direccion { get; set; }
        public virtual faja faja { get; set; }
        public virtual seguridad_usuario seguridad_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<profesor_imagen> profesor_imagen { get; set; }
    }
}
