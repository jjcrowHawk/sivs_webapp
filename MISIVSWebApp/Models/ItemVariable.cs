namespace MISIVSWebApp.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
            ItemClasificacion = new HashSet<ItemClasificacion>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(70)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string tipo { get; set; }

        public int variable_item { get; set; }

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool activo { get; set; }

        public virtual Variable Variable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opcion> Opcion { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemClasificacion> ItemClasificacion { get; set; }
    }
}
