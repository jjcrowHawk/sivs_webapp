namespace MISIVSWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBHelper : DbContext
    {
        public DBHelper()
            : base("name=SIVSDB")
        {
        }

        public virtual DbSet<Anexo> Anexo { get; set; }
        public virtual DbSet<Ficha> Ficha { get; set; }
        public virtual DbSet<ItemVariable> ItemVariable { get; set; }
        public virtual DbSet<Opcion> Opcion { get; set; }
        public virtual DbSet<Respuesta> Respuesta { get; set; }
        public virtual DbSet<RespuestaOpcion> RespuestaOpcion { get; set; }
        public virtual DbSet<RespuestaOpcionMultiple> RespuestaOpcionMultiple { get; set; }
        public virtual DbSet<RespuestaOpcionSimple> RespuestaOpcionSimple { get; set; }
        public virtual DbSet<RespuestaTexto> RespuestaTexto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Seccion> Seccion { get; set; }
        public virtual DbSet<SeccionFicha> SeccionFicha { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Variable> Variable { get; set; }
        public virtual DbSet<Vivienda> Vivienda { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anexo>()
                .Property(e => e.url_anexo)
                .IsUnicode(false);

            modelBuilder.Entity<Anexo>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Ficha>()
                .Property(e => e.inspector)
                .IsUnicode(false);

            modelBuilder.Entity<Ficha>()
                .HasMany(e => e.Anexo)
                .WithRequired(e => e.Ficha)
                .HasForeignKey(e => e.ficha_anexo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ficha>()
                .HasMany(e => e.Respuesta)
                .WithRequired(e => e.Ficha)
                .HasForeignKey(e => e.ficha_respuesta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ficha>()
                .HasMany(e => e.SeccionFicha)
                .WithRequired(e => e.Ficha)
                .HasForeignKey(e => e.ficha_rel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ficha>()
                .Property(f => f.fecha_inspeccion)
                .HasColumnType("datetime2");

            modelBuilder.Entity<Ficha>()
                .Property(f => f.fecha_sincronizacion)
                .HasColumnType("datetime2");

            modelBuilder.Entity<ItemVariable>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ItemVariable>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<ItemVariable>()
                .HasMany(e => e.Opcion)
                .WithRequired(e => e.ItemVariable)
                .HasForeignKey(e => e.item_opcion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ItemVariable>()
                .HasMany(e => e.Respuesta)
                .WithRequired(e => e.ItemVariable)
                .HasForeignKey(e => e.item_respuesta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Opcion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Opcion>()
                .HasMany(e => e.RespuestaOpcionSimple)
                .WithRequired(e => e.Opcion)
                .HasForeignKey(e => e.opcion_respuestasimple)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Opcion>()
                .HasMany(e => e.RespuestaOpcion)
                .WithRequired(e => e.Opcion)
                .HasForeignKey(e => e.opcion_respuesta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Respuesta>()
                .Property(e => e.nota)
                .IsUnicode(false);

            modelBuilder.Entity<Respuesta>()
                .HasMany(e => e.RespuestaTexto)
                .WithRequired(e => e.Respuesta)
                .HasForeignKey(e => e.respuesta_texto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Respuesta>()
                .HasMany(e => e.RespuestaOpcionSimple)
                .WithRequired(e => e.Respuesta1)
                .HasForeignKey(e => e.respuesta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Respuesta>()
                .HasMany(e => e.RespuestaOpcionMultiple)
                .WithRequired(e => e.Respuesta)
                .HasForeignKey(e => e.respuesta_opcionmultiple)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RespuestaOpcionMultiple>()
                .HasMany(e => e.RespuestaOpcion)
                .WithRequired(e => e.RespuestaOpcionMultiple)
                .HasForeignKey(e => e.respuesta_opcionmultiple)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RespuestaTexto>()
                .Property(e => e.texto)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Rol)
                .HasForeignKey(e => e.rol_usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seccion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Seccion>()
                .HasMany(e => e.SeccionFicha)
                .WithRequired(e => e.Seccion)
                .HasForeignKey(e => e.seccion_rel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seccion>()
                .HasMany(e => e.Variable)
                .WithOptional(e => e.Seccion)
                .HasForeignKey(e => e.seccion_variable);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.contrasena)
                .IsUnicode(false);

            modelBuilder.Entity<Variable>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Variable>()
                .HasMany(e => e.ItemVariable)
                .WithRequired(e => e.Variable)
                .HasForeignKey(e => e.variable_item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vivienda>()
                .Property(e => e.inspeccion_id)
                .IsUnicode(false);

            modelBuilder.Entity<Vivienda>()
                .Property(e => e.elevacion)
                .HasPrecision(10, 5);

            modelBuilder.Entity<Vivienda>()
                .Property(e => e.sector)
                .IsUnicode(false);

            modelBuilder.Entity<Vivienda>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Vivienda>()
                .Property(e => e.ubicacion)
                .IsUnicode(false);

            modelBuilder.Entity<Vivienda>()
                .HasMany(e => e.Ficha)
                .WithRequired(e => e.Vivienda)
                .HasForeignKey(e => e.vivienda_ficha)
                .WillCascadeOnDelete(false);
        }
    }
}
