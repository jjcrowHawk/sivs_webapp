namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeccionFicha")]
    public partial class SeccionFicha
    {
        public int id { get; set; }

        public int ficha_rel { get; set; }

        public int seccion_rel { get; set; }

        [ForeignKey("ficha_rel")]
        public virtual Ficha Ficha { get; set; }

        public virtual Seccion Seccion { get; set; }
    }
}
