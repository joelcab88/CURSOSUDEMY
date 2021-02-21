using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCORE_STOREDPROCEDURE.Data;
using ASPNETCORE_STOREDPROCEDURE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCORE_STOREDPROCEDURE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ValuesRepository _repository;
        public ValuesController(ValuesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET api/Values
        [HttpGet]
        public async Task<List<Value>> Get()
        {
            return await _repository.GetAll();
        }

        // GET api/Values/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> Get(int id)
        {
            var response = await _repository.GetById(id);
            
            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return response;
            }
        }

        //POST api/values
        [HttpPost]
        public async Task Post([FromBody] Value _value)
        {
            await _repository.Insert(_value);
        }

        //DELETE api/values/{id}
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.DeleteById(id);
        }
    }
}
