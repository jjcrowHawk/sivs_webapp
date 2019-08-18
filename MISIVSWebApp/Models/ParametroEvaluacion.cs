namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParametroEvaluacion")]
    public partial class ParametroEvaluacion
    {
        public int id { get; set; }

        public int puntaje { get; set; }

        public decimal puntaje_relativo { get; set; }

        public int parametro_evaluado { get; set; }

        public int evaluacion { get; set; }

        public virtual EvaluacionFicha EvaluacionFicha { get; set; }

        public virtual Parametro Parametro { get; set; }
    }
}
