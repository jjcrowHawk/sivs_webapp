    namespace MISIVSWebApp.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Opcion")]
    public partial class Opcion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Opcion()
        {
            RespuestaOpcionSimple = new HashSet<RespuestaOpcionSimple>();
            RespuestaOpcion = new HashSet<RespuestaOpcion>();
            OpcionPuntaje = new HashSet<OpcionesPuntaje>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public int item_opcion { get; set; }

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool activo { get; set; }

        [ForeignKey("item_opcion")]
        public virtual ItemVariable ItemVariable { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcionSimple> RespuestaOpcionSimple { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcion> RespuestaOpcion { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpcionesPuntaje> OpcionPuntaje { get; set; }

        public override bool Equals(object obj)
        {
            var variable = obj as Opcion;
            if (variable == null)
                return false;
            if (variable == this)
                return true;
            return id == variable.id;
        }

        public override int GetHashCode()
        {
            return 1877310955 + id.GetHashCode();
        }

    }
}
