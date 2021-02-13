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
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autores.ToList();
        }

        /// <summary>
        /// OBTIENE UN SOLO AUTOR
        /// </summary>
        /// <param name="_id">Id del autor que deseamos consultar</param>
        /// <returns>Autor solicitado por URI</returns>
        [HttpGet("{_id}", Name = "ObtenerAutor")]
        public ActionResult<Autor> GetAutorById(int _id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.iIdAutor == _id);
            if (autor == null)
            {
                return NotFound();
            }
            else
            {
                return autor;
            }
        }

        /// <summary>
        /// REALIZA UN INSERT A LA TABLA DE AUTORES
        /// </summary>
        /// <param name="autor">Objeto autores</param>
        /// <returns>Location del nuevo registro</returns>
        /// <Update>JFCABY - Se soluciona error 500 se modifica parametro id por_id que recibe el metodo GetAutorById</Update>
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            context.Autores.Add(autor);
            context.SaveChanges();
            var RouteResult = new CreatedAtRouteResult("ObtenerAutor", new { _id = autor.iIdAutor }, autor);
            return RouteResult;
        }

        /// <summary>
        /// ACTUALIZA UN INFORMACIÓN DEL AUTOR
        /// </summary>
        /// <param name="_id">Id del autor al que se desea módificar la información</param>
        /// <param name="value">Objeto Autor</param>
        /// <returns>Retorna status HTTP segun sea el caso.</returns>
        [HttpPut("{_id}")]
        public ActionResult Put(int _id, [FromBody] Autor value)
        {
            if (_id != value.iIdAutor)
            {
                return BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();

        }

        /// <summary>
        /// ELIMINA UN AUTOR DE LA BASE DE DATOS
        /// </summary>
        /// <param name="_id">Id del autor que se desea elimnanar</param>
        /// <returns>Retorna autor</returns>
        [HttpDelete("{_id}")]
        public ActionResult<Autor> Delete(int _id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.iIdAutor == _id);

            if (autor == null)
            {
                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }

    }
}
