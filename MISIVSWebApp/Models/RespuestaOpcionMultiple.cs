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
        public int id { get; set; }

        public int respuesta { get; set; }

        public virtual Respuesta Respuesta1 { get; set; }
    }
}
