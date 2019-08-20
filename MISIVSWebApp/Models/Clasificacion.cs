namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clasificacion")]
    public partial class Clasificacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clasificacion()
        {
            ClasificacionEvaluacion = new HashSet<ClasificacionEvaluacion>();
            ItemClasificacion = new HashSet<ItemClasificacion>();
            PuntajeClasificacion = new HashSet<PuntajeClasificacion>();
        }

        public int id { get; set; }

        [StringLength(200)]
        public string nombre { get; set; }

        public int valor_minimo { get; set; }

        public int valor_maximo { get; set; }

        public decimal peso_relativo { get; set; }

        public int parametro_clasificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClasificacionEvaluacion> ClasificacionEvaluacion { get; set; }

        public virtual Parametro Parametro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemClasificacion> ItemClasificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuntajeClasificacion> PuntajeClasificacion { get; set; }
    }
}
