using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManupulacionDatos_M5.Models
{
    public class AutorCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
