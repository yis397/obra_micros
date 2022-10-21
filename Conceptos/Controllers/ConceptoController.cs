using Conceptos.Cors.Models;
using Conceptos.Cors.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Conceptos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoController : Controller
    {
        public IMongoRepository<Concepto> _repository;
        public ConceptoController(IMongoRepository<Concepto> repository)
        {
            this._repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> AddConcepto(Concepto concepto)
        {
            await _repository.AddItem(concepto);
            return Ok("ok");
        }
    }
}
