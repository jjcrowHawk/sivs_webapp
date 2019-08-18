namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OpcionesClasificacion")]
    public partial class OpcionesClasificacion
    {
        public int id { get; set; }

        public int clasificacion_relacion { get; set; }

        public int opcion_relacion { get; set; }

        public virtual Clasificacion Clasificacion { get; set; }

        public virtual Opcion Opcion { get; set; }
    }
}
