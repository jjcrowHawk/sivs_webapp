namespace MISIVSWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBMathModelHelper : DbContext
    {
        public DBMathModelHelper()
            : base("name=SIVSMathModelDB")
        {
        }

        public virtual DbSet<Clasificacion> Clasificacion { get; set; }
        public virtual DbSet<ClasificacionEvaluacion> ClasificacionEvaluacion { get; set; }
        public virtual DbSet<EvaluacionFicha> EvaluacionFicha { get; set; }
        public virtual DbSet<ItemParametro> ItemParametro { get; set; }
        public virtual DbSet<ModeloMatematico> ModeloMatematico { get; set; }
        public virtual DbSet<OpcionesClasificacion> OpcionesClasificacion { get; set; }
        public virtual DbSet<Parametro> Parametro { get; set; }
        public virtual DbSet<ParametroEvaluacion> ParametroEvaluacion { get; set; }
        public virtual DbSet<ParametroModelo> ParametroModelo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clasificacion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Clasificacion>()
                .Property(e => e.categoria)
                .IsUnicode(false);

            modelBuilder.Entity<Clasificacion>()
                .HasMany(e => e.ClasificacionEvaluacion)
                .WithRequired(e => e.Clasificacion)
                .HasForeignKey(e => e.clasificacion_ficha)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clasificacion>()
                .HasMany(e => e.OpcionesClasificacion)
                .WithRequired(e => e.Clasificacion)
                .HasForeignKey(e => e.clasificacion_relacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EvaluacionFicha>()
                .Property(e => e.puntaje)
                .HasPrecision(6, 2);

            modelBuilder.Entity<EvaluacionFicha>()
                .Property(e => e.puntaje_relativo)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EvaluacionFicha>()
                .HasMany(e => e.ClasificacionEvaluacion)
                .WithRequired(e => e.EvaluacionFicha)
                .HasForeignKey(e => e.evaluacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EvaluacionFicha>()
                .HasMany(e => e.ParametroEvaluacion)
                .WithRequired(e => e.EvaluacionFicha)
                .HasForeignKey(e => e.evaluacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModeloMatematico>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ModeloMatematico>()
                .Property(e => e.valor_min)
                .HasPrecision(6, 2);

            modelBuilder.Entity<ModeloMatematico>()
                .Property(e => e.valor_max)
                .HasPrecision(6, 2);

            modelBuilder.Entity<ModeloMatematico>()
                .HasMany(e => e.EvaluacionFicha)
                .WithRequired(e => e.ModeloMatematico)
                .HasForeignKey(e => e.modelo_evaluado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModeloMatematico>()
                .HasMany(e => e.ParametroModelo)
                .WithRequired(e => e.ModeloMatematico)
                .HasForeignKey(e => e.modelo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parametro>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Parametro>()
                .HasMany(e => e.Clasificacion)
                .WithRequired(e => e.Parametro)
                .HasForeignKey(e => e.parametro_clasificacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parametro>()
                .HasMany(e => e.ItemParametro)
                .WithRequired(e => e.Parametro)
                .HasForeignKey(e => e.parametro_relacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parametro>()
                .HasMany(e => e.ParametroModelo)
                .WithRequired(e => e.Parametro)
                .HasForeignKey(e => e.parametro_asignado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parametro>()
                .HasMany(e => e.ParametroEvaluacion)
                .WithRequired(e => e.Parametro)
                .HasForeignKey(e => e.parametro_evaluado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParametroEvaluacion>()
                .Property(e => e.puntaje_relativo)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ParametroModelo>()
                .Property(e => e.peso_relativo)
                .HasPrecision(5, 2);
        }
    }
}
