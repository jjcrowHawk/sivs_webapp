namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RespuestaOpcionMultiple")]
    public partial class RespuestaOpcionMultiple
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RespuestaOpcionMultiple()
        {
            RespuestaOpcion = new HashSet<RespuestaOpcion>();
        }

        public int id { get; set; }

        public int respuesta_opcionmultiple { get; set; }

        [ForeignKey("respuesta_opcionmultiple")]
        public virtual Respuesta Respuesta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcion> RespuestaOpcion { get; set; }
    }
}
