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
    
    public partial class lucha
    {
        public int id_lucha { get; set; }
        public int id_participante1 { get; set; }
        public int id_participante2 { get; set; }
        public Nullable<int> id_resultado { get; set; }
        public Nullable<int> ronda { get; set; }
        public Nullable<int> id_torneo { get; set; }
    
        public virtual participante participante { get; set; }
        public virtual participante participante1 { get; set; }
        public virtual resultado resultado { get; set; }
        public virtual torneo torneo { get; set; }
    }
}