using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebAPI_M3.Contexts;
using MiPrimerWebAPI_M3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebAPI_M3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            var context2 = context.Libros.Include(x => x.Autor).ToList();
            return context2;
        }

        /// <summary>
        /// OBTIENE UN SOLO LIBRO DEL AUTOR
        /// </summary>
        /// <param name="_id">Id del libro que deseamos consultar</param>
        /// <returns>Libro solicitado por URI</returns>
        [HttpGet("{_id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> GetLibroById(int _id)
        {
            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.iIdLibro == _id);
            if (libro == null)
            {
                return NotFound();
            }
            else
            {
                return libro;
            }
        }

        /// <summary>
        /// REALIZA UN INSERT A LA TABLA DE LIBROS
        /// </summary>
        /// <param name="autor">Objeto Libros</param>
        /// <returns>Location del nuevo registro</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            var RouteResult = new CreatedAtRouteResult("ObtenerAutor", new { _id = libro.iIdLibro }, libro);
            return RouteResult;
        }

        /// <summary>
        /// ACTUALIZA INFO. DE UN LIBRO
        /// </summary>
        /// <param name="_id">Id del libro al cual deseamos actualizar su información</param>
        /// <param name="value">Objeto libro</param>
        /// <returns>Retorna estatus HTTP  segun sea el caso.</returns>
        [HttpPut("{_id}")]
        public ActionResult Put(int _id, [FromBody] Libro value)
        {
            if (_id != value.iIdLibro)
            {
                return BadRequest();
            }
            else
            {
                context.Entry(value).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("{_id}")]
        public ActionResult<Libro> Delete(int _id)
        {
            var libro = context.Libros.FirstOrDefault(x => x.iIdLibro == _id);

            if (libro == null)
            {
                return NotFound();
            }
            else
            {
                context.Libros.Remove(libro);
                context.SaveChanges();
                return libro;
            }
        }
    }
}
