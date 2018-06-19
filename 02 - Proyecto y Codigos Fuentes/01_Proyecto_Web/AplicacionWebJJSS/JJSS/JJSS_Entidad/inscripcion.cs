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
    
    public partial class inscripcion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inscripcion()
        {
            this.pago_torneo = new HashSet<pago_torneo>();
        }
    
        public int id_inscripcion { get; set; }
        public string hora { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<long> codigo_barra { get; set; }
        public Nullable<int> id_participante { get; set; }
        public Nullable<int> id_torneo { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public Nullable<double> peso { get; set; }
        public Nullable<int> id_faja { get; set; }
        public Nullable<short> tipo_inscripcion { get; set; }
        public Nullable<int> id_absoluto { get; set; }
    
        public virtual categoria_torneo categoria_torneo { get; set; }
        public virtual categoria_torneo categoria_torneo1 { get; set; }
        public virtual faja faja { get; set; }
        public virtual participante participante { get; set; }
        public virtual torneo torneo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pago_torneo> pago_torneo { get; set; }
    }
}
