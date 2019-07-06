namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
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
            RespuestaOpcion = new HashSet<RespuestaOpcion>();
        }

        public int id { get; set; }

        [StringLength(200)]
        public string nota { get; set; }

        public int ficha { get; set; }

        public int item { get; set; }

        public bool activo { get; set; }

        public virtual Ficha Ficha1 { get; set; }

        public virtual ItemVariable ItemVariable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaTexto> RespuestaTexto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcionSimple> RespuestaOpcionSimple { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcionMultiple> RespuestaOpcionMultiple { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestaOpcion> RespuestaOpcion { get; set; }
    }
}
