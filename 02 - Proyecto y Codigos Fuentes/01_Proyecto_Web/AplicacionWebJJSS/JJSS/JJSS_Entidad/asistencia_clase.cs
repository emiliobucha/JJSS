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
    
    public partial class asistencia_clase
    {
        public int id_asistencia { get; set; }
        public System.DateTime fecha_hora { get; set; }
        public int id_alumno { get; set; }
        public int id_clase { get; set; }
    
        public virtual alumno alumno { get; set; }
        public virtual clase clase { get; set; }
    }
}
