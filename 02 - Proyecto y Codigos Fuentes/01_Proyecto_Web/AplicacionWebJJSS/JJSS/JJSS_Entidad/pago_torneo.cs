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
    
    public partial class pago_torneo
    {
        public int id_pago_torneo { get; set; }
        public int id_inscripcion_torneo { get; set; }
        public int id_participante { get; set; }
        public System.DateTime fecha { get; set; }
        public int id_forma_pago { get; set; }
        public int id_usuario { get; set; }
        public decimal pago_monto { get; set; }
        public Nullable<int> id_pago_multiple { get; set; }
    
        public virtual forma_pago forma_pago { get; set; }
        public virtual inscripcion inscripcion { get; set; }
        public virtual pago_multiple pago_multiple { get; set; }
        public virtual participante participante { get; set; }
        public virtual seguridad_usuario seguridad_usuario { get; set; }
    }
}
