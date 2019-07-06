namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Anexo")]
    public partial class Anexo
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string url_anexo { get; set; }

        [Required]
        [StringLength(15)]
        public string tipo { get; set; }

        public int ficha { get; set; }

        public virtual Ficha Ficha1 { get; set; }
    }
}
