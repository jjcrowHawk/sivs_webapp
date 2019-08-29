namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EvaluacionFicha")]
    public partial class EvaluacionFicha
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluacionFicha()
        {
            ClasificacionEvaluacion = new HashSet<ClasificacionEvaluacion>();
            PuntajeClasificacionEvaluacion = new HashSet<PuntajeClasificacionEvaluacion>();
        }

        public int id { get; set; }

        public decimal? puntaje { get; set; }

        public decimal? puntaje_relativo { get; set; }

        public DateTime fecha_generada { get; set; }

        public int ficha_evaluacion { get; set; }

        public int modelo_evaluado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClasificacionEvaluacion> ClasificacionEvaluacion { get; set; }

        [ForeignKey("modelo_evaluado")]
        public virtual ModeloMatematico ModeloMatematico { get; set; }

        [ForeignKey("ficha_evaluacion")]
        public virtual Ficha Ficha { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuntajeClasificacionEvaluacion> PuntajeClasificacionEvaluacion { get; set; }
    }
}
