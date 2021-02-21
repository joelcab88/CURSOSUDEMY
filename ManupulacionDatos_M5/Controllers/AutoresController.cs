using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManupulacionDatos_M5.Contexts;
using ManupulacionDatos_M5.Entities;
using ManupulacionDatos_M5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManupulacionDatos_M5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> Get()
        {
            var autores = await context.Autores.Include(x => x.Books).ToListAsync();
            var autoresDTO = mapper.Map<List<AutorDTO>>(autores);
            return autoresDTO;
        }

        // GET api/autores/5 
        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return autorDTO;
        }

        // POST api/autores
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion);
            context.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        // PUT api/autores/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorCreacionDTO autorActualizacion)
        {
            var autor = mapper.Map<Autor>(autorActualizacion);
            autor.Id = id;
            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Actualiza parcialmente el recurso AutorCreacionDTO
        /// </summary>
        /// <param name="id">Id del autor que deseamos actualizar</param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <documentacion>El método HTTP PATCH se utuliza para aplizar actualizaciones parciales a un recurso
        /// (Solo actualiza algunos cambios del recurso)
        /// </documentacion>

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AutorCreacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var autorDeLaDB = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autorDeLaDB == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorCreacionDTO>(autorDeLaDB);

            patchDocument.ApplyTo(autorDTO, ModelState);

            mapper.Map(autorDTO, autorDeLaDB);

            var isValid = TryValidateModel(autorDeLaDB);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await context.SaveChangesAsync();

            return NoContent();

        }

        // DELETE api/autores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorId = await context.Autores.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (autorId == default(int))
            {
                return NotFound();
            }

            context.Remove(new Autor { Id = autorId });
            await context.SaveChangesAsync();
            return NoContent();
        }

        ///<summary>
        ///Método patch sin usar el DTO
        ///</summary>
        //[HttpPatch("{id}")]
        //public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Autor> patchDocument)
        //{
        //    if (patchDocument == null)
        //    {
        //        return BadRequest();
        //    }

        //    var autorDeLaDB = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

        //    if (autorDeLaDB == null)
        //    {
        //        return NotFound();
        //    }

        //    patchDocument.ApplyTo(autorDeLaDB, ModelState);

        //    var isValid = TryValidateModel(autorDeLaDB);

        //    if (!isValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    await context.SaveChangesAsync();
        //    return NoContent();

        //}

    }
}
