using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ServicioAPI.Entities; 
using ServicioAPI.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServicioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tb_DetalleBoletaController : ControllerBase
    {
        private readonly AppDbContext context;

        public tb_DetalleBoletaController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<tb_DetalleBoleta> Get()
        {
            return context.tb_DetalleBoleta.ToList();
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
