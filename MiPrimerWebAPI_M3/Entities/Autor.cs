using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebAPI_M3.Entities
{
    public class Autor
    {
        /// <summary>
        /// Id del autor
        /// </summary>
        [Key]
        public int iIdAutor { get; set; }
        /// <summary>
        /// Nombre del autor
        /// </summary>
        [Required(ErrorMessage = "El nombre del autor es requerido.")]
        public string cNombre { get; set; }
    }
}
