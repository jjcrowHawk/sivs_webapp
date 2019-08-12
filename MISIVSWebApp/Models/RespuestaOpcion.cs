namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RespuestaOpcion")]
    public partial class RespuestaOpcion
    {
        public int id { get; set; }

        public int opcion_respuesta { get; set; }

        public int respuesta_opcionmultiple { get; set; }

        public virtual Opcion Opcion { get; set; }

        public virtual RespuestaOpcionMultiple RespuestaOpcionMultiple { get; set; }
    }
}
