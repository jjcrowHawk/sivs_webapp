namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vivienda")]
    public partial class Vivienda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vivienda()
        {
            Ficha = new HashSet<Ficha>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string inspeccion_id { get; set; }

        public int? edad_construcción { get; set; }

        public decimal? elevacion { get; set; }

        [Required]
        [StringLength(100)]
        public string sector { get; set; }

        [StringLength(200)]
        public string direccion { get; set; }

        [StringLength(200)]
        public string ubicacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ficha> Ficha { get; set; }
    }
}
