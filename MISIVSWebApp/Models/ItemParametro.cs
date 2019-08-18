namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemParametro")]
    public partial class ItemParametro
    {
        public int id { get; set; }

        public int parametro_relacion { get; set; }

        public int item_relacion { get; set; }

        public virtual Parametro Parametro { get; set; }

        public virtual ItemVariable ItemVariable { get; set; }
    }
}
