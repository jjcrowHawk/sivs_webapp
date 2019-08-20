namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParametroModelo")]
    public partial class ParametroModelo
    {
        public int id { get; set; }

        public int parametro_asignado { get; set; }

        public int modelo { get; set; }

        public virtual ModeloMatematico ModeloMatematico { get; set; }

        public virtual Parametro Parametro { get; set; }
    }
}
