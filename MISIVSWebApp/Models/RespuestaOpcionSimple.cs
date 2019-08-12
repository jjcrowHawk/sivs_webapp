namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RespuestaOpcionSimple")]
    public partial class RespuestaOpcionSimple
    {
        public int id { get; set; }

        public int respuesta { get; set; }

        public int opcion_respuestasimple { get; set; }

        public virtual Opcion Opcion { get; set; }

        public virtual Respuesta Respuesta1 { get; set; }
    }
}
