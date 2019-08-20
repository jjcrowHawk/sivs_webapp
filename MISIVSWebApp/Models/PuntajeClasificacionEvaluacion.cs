namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PuntajeClasificacionEvaluacion")]
    public partial class PuntajeClasificacionEvaluacion
    {
        public int id { get; set; }

        public int evaluacion { get; set; }

        public int puntaje_respuesta_clasificacion { get; set; }

        public virtual EvaluacionFicha EvaluacionFicha { get; set; }

        public virtual PuntajeClasificacion PuntajeClasificacion { get; set; }
    }
}
