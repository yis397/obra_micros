using Conceptos.Cors.Models;
using Conceptos.Cors.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Conceptos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrabajadorController : Controller
    {
        private readonly IMongoRepository<Trabajador> _mongo;
        public TrabajadorController(IMongoRepository<Trabajador> mongo) =>_mongo = mongo;

        [HttpPost]
        public async Task<ActionResult> AddTrabajador(Trabajador trabajador)
        {
            await _mongo.AddItem(trabajador);
            return Ok("ok");
        }
    }
}
