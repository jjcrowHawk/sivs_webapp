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

        public int ficha { get; set; }

        public int seccion { get; set; }

        public virtual Ficha Ficha1 { get; set; }

        public virtual Seccion Seccion1 { get; set; }
    }
}
