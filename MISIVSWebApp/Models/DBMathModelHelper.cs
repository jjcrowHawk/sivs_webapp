namespace MISIVSWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBMathModelHelper : DbContext
    {
        public DBMathModelHelper()
            : base("name=SivsMathModelDB")
        {
        }

        public virtual DbSet<Clasificacion> Clasificacion { get; set; }
        public virtual DbSet<ClasificacionEvaluacion> ClasificacionEvaluacion { get; set; }
        public virtual DbSet<EvaluacionFicha> EvaluacionFicha { get; set; }
        public virtual DbSet<ItemClasificacion> ItemClasificacion { get; set; }
        public virtual DbSet<ModeloMatematico> ModeloMatematico { get; set; }
        public virtual DbSet<OpcionesPuntaje> OpcionesPuntaje { get; set; }
        public virtual DbSet<Parametro> Parametro { get; set; }
        public virtual DbSet<ParametroModelo> ParametroModelo { get; set; }
        public virtual DbSet<PuntajeClasificacion> PuntajeClasificacion { get; set; }
        public virtual DbSet<PuntajeClasificacionEvaluacion> PuntajeClasificacionEvaluacion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clasificacion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Clasificacion>()
                .Property(e => e.peso_relativo)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Clasificacion>()
                .HasMany(e => e.ClasificacionEvaluacion)
                .WithRequired(e => e.Clasificacion)
                .HasForeignKey(e => e.clasificacion_evaluada)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clasificacion>()
                .HasMany(e => e.ItemClasificacion)
                .WithRequired(e => e.Clasificacion)
                .HasForeignKey(e => e.clasificacion_relacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clasificacion>()
                .HasMany(e => e.PuntajeClasificacion)
                .WithRequired(e => e.Clasificacion)
                .HasForeignKey(e => e.clasificacion_puntaje)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClasificacionEvaluacion>()
                .Property(e => e.peso_relativo)
                .HasPrecision(5, 2);

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
                .HasMany(e => e.PuntajeClasificacionEvaluacion)
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
                .Property(e => e.peso_relativo)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Parametro>()
                .HasMany(e => e.Clasificacion)
                .WithRequired(e => e.Parametro)
                .HasForeignKey(e => e.parametro_clasificacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parametro>()
                .HasMany(e => e.ParametroModelo)
                .WithRequired(e => e.Parametro)
                .HasForeignKey(e => e.parametro_asignado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PuntajeClasificacion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<PuntajeClasificacion>()
                .Property(e => e.penalizacion)
                .IsUnicode(false);

            modelBuilder.Entity<PuntajeClasificacion>()
                .HasMany(e => e.OpcionesPuntaje)
                .WithRequired(e => e.PuntajeClasificacion)
                .HasForeignKey(e => e.puntaje)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PuntajeClasificacion>()
                .HasMany(e => e.PuntajeClasificacionEvaluacion)
                .WithRequired(e => e.PuntajeClasificacion)
                .HasForeignKey(e => e.puntaje_respuesta_clasificacion)
                .WillCascadeOnDelete(false);
        }
    }
}
