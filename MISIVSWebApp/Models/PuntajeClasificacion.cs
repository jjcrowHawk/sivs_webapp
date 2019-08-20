namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PuntajeClasificacion")]
    public partial class PuntajeClasificacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PuntajeClasificacion()
        {
            OpcionesPuntaje = new HashSet<OpcionesPuntaje>();
            PuntajeClasificacionEvaluacion = new HashSet<PuntajeClasificacionEvaluacion>();
        }

        public int id { get; set; }

        [StringLength(200)]
        public string nombre { get; set; }

        public int puntaje { get; set; }

        [Required]
        [StringLength(10)]
        public string penalizacion { get; set; }

        public int clasificacion_puntaje { get; set; }

        public virtual Clasificacion Clasificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpcionesPuntaje> OpcionesPuntaje { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuntajeClasificacionEvaluacion> PuntajeClasificacionEvaluacion { get; set; }
    }
}
