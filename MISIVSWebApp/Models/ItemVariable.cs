namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemVariable")]
    public partial class ItemVariable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemVariable()
        {
            Opcion = new HashSet<Opcion>();
            Respuesta = new HashSet<Respuesta>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(70)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string tipo { get; set; }

        public int variable { get; set; }

        public bool activo { get; set; }

        public virtual Variable Variable1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opcion> Opcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
