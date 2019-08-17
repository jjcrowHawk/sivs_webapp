namespace MISIVSWebApp.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

            fecha_inspeccion = DateTime.Now;
            fecha_sincronizacion = DateTime.Now;
        }

        public int id { get; set; }

        [Required]
        [StringLength(70)]
        public string inspector { get; set; }

        
        [Column(TypeName = "datetime")]
        public DateTime fecha_inspeccion { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime fecha_sincronizacion { get; set; }

        public int vivienda_ficha { get; set; }

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Anexo> Anexo { get; set; }

        public virtual Vivienda Vivienda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respuesta> Respuesta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeccionFicha> SeccionFicha { get; set; }
    }
}
