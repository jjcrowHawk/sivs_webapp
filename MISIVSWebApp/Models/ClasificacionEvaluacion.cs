namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClasificacionEvaluacion")]
    public partial class ClasificacionEvaluacion
    {
        public int id { get; set; }

        public int evaluacion { get; set; }

        public int clasificacion_ficha { get; set; }

        public virtual Clasificacion Clasificacion { get; set; }

        public virtual EvaluacionFicha EvaluacionFicha { get; set; }
    }
}
