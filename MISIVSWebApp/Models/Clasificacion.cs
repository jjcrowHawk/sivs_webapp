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
            OpcionesClasificacion = new HashSet<OpcionesClasificacion>();
        }

        public int id { get; set; }

        [StringLength(200)]
        public string nombre { get; set; }

        public int puntaje { get; set; }

        [StringLength(1)]
        public string categoria { get; set; }

        public int parametro_clasificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClasificacionEvaluacion> ClasificacionEvaluacion { get; set; }

        public virtual Parametro Parametro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpcionesClasificacion> OpcionesClasificacion { get; set; }
    }
}
