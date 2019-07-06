namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ficha")]
    public partial class Ficha
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ficha()
        {
            Anexo = new HashSet<Anexo>();
            Respuesta = new HashSet<Respuesta>();
            SeccionFicha = new HashSet<SeccionFicha>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(70)]
        public string inspector { get; set; }

        public DateTime fecha_inspeccion { get; set; }

        public DateTime fecha_sincronizacion { get; set; }

        public int vivienda { get; set; }

        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Anexo> Anexo { get; set; }

        public virtual Vivienda Vivienda1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeccionFicha> SeccionFicha { get; set; }
    }
}
