namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RespuestaTexto")]
    public partial class RespuestaTexto
    {
        public int id { get; set; }

        [Required]
        [StringLength(500)]
        public string texto { get; set; }

        public int respuesta_texto { get; set; }

        public virtual Respuesta Respuesta { get; set; }
    }
}
