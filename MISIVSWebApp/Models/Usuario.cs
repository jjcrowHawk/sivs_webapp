namespace MISIVSWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string correo { get; set; }

        [Required]
        [StringLength(200)]
        public string nombres { get; set; }

        [Required]
        [StringLength(200)]
        public string apellidos { get; set; }

        [Required]
        [StringLength(200)]
        public string contrasena { get; set; }

        public bool activo { get; set; }

        public int rol_usuario { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
