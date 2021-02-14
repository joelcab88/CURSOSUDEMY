using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebAPI_M3.Entities
{
    public class Libro
    {
        /// <summary>
        /// Id de libro
        /// </summary>
        [Key]
        public int iIdLibro { get; set; }
        /// <summary>
        /// Titulo del libro
        /// </summary>
        [Required]
        public string cTitulo { get; set; }
        /// <summary>
        /// Id del autor del libro
        /// </summary>
        [Required]
        public int iIdAutor { get; set; }
        /// <summary>
        /// Propiedad de navegación Autor
        /// </summary>
        public Autor Autor { get; set; }
    }
}
