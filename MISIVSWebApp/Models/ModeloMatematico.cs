namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModeloMatematico")]
    public partial class ModeloMatematico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModeloMatematico()
        {
            EvaluacionFicha = new HashSet<EvaluacionFicha>();
            ParametroModelo = new HashSet<ParametroModelo>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string nombre { get; set; }

        public decimal? valor_min { get; set; }

        public decimal? valor_max { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluacionFicha> EvaluacionFicha { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParametroModelo> ParametroModelo { get; set; }
    }
}
