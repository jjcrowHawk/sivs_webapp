namespace MISIVSWebApp.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Respuesta")]
    public partial class Respuesta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Respuesta()
        {
            RespuestaTexto = new HashSet<RespuestaTexto>();
            RespuestaOpcionSimple = new HashSet<RespuestaOpcionSimple>();
            RespuestaOpcionMultiple = new HashSet<RespuestaOpcionMultiple>();
        }

        public int id { get; set; }

        [StringLength(200)]
        public string nota { get; set; }

        public int ficha_respuesta { get; set; }

        public int item_respuesta { get; set; }

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool activo { get; set; }

        [ForeignKey("ficha_respuesta")]
        public virtual Ficha Ficha { get; set; }

        [ForeignKey("item_respuesta")]
        public virtual ItemVariable ItemVariable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaTexto> RespuestaTexto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcionSimple> RespuestaOpcionSimple { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcionMultiple> RespuestaOpcionMultiple { get; set; }
    }
}
