﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JJSSEntities : DbContext
    {
        public JJSSEntities()
            : base("name=JJSSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<academia> academia { get; set; }
        public virtual DbSet<categoria_torneo> categoria_torneo { get; set; }
        public virtual DbSet<direccion> direccion { get; set; }
        public virtual DbSet<estado> estado { get; set; }
        public virtual DbSet<faja> faja { get; set; }
        public virtual DbSet<inscripcion> inscripcion { get; set; }
        public virtual DbSet<lucha> lucha { get; set; }
        public virtual DbSet<pais> pais { get; set; }
        public virtual DbSet<resultado> resultado { get; set; }
        public virtual DbSet<sede> sede { get; set; }
        public virtual DbSet<torneo> torneo { get; set; }
        public virtual DbSet<alumno> alumno { get; set; }
        public virtual DbSet<participante> participante { get; set; }
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<clase> clase { get; set; }
        public virtual DbSet<horario> horario { get; set; }
        public virtual DbSet<inscripcion_clase> inscripcion_clase { get; set; }
        public virtual DbSet<tipo_clase> tipo_clase { get; set; }
        public virtual DbSet<alumno_imagen> alumno_imagen { get; set; }
        public virtual DbSet<torneo_imagen> torneo_imagen { get; set; }
        public virtual DbSet<provincia> provincia { get; set; }
        public virtual DbSet<ciudad> ciudad { get; set; }
        public virtual DbSet<seguridad_grupo> seguridad_grupo { get; set; }
        public virtual DbSet<seguridad_grupoxopcion> seguridad_grupoxopcion { get; set; }
        public virtual DbSet<seguridad_opcion> seguridad_opcion { get; set; }
        public virtual DbSet<seguridad_usuario> seguridad_usuario { get; set; }
        public virtual DbSet<seguridad_usuarioxgrupo> seguridad_usuarioxgrupo { get; set; }
        public virtual DbSet<profesor> profesor { get; set; }
        public virtual DbSet<profesor_imagen> profesor_imagen { get; set; }
    }
}
