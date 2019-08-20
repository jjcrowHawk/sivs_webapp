namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemClasificacion")]
    public partial class ItemClasificacion
    {
        public int id { get; set; }

        public int clasificacion_relacion { get; set; }

        public int item_relacion { get; set; }

        public virtual Clasificacion Clasificacion { get; set; }
        
        public virtual ItemVariable ItemVariable { get; set; }
    }
}
