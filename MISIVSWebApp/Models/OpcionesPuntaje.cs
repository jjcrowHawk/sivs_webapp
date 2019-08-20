namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OpcionesPuntaje")]
    public partial class OpcionesPuntaje
    {
        public int id { get; set; }

        public int puntaje { get; set; }

        public int opcion_puntaje { get; set; }

        public virtual PuntajeClasificacion PuntajeClasificacion { get; set; }
        
        public virtual Opcion Opcion { get; set; }
    }
}
