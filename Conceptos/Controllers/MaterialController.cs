using Conceptos.Cors.Models;
using Conceptos.Cors.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conceptos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : Controller
    {
        private readonly IMongoRepository<Material> _materialRepository;
        public MaterialController(IMongoRepository<Material> conceptoRepository)
        {
            _materialRepository = conceptoRepository;
        }
        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> AddMaterial(Material material)
        {
            await _materialRepository.AddItem(material);
            return Ok("ok");
        }
        [HttpGet]
        public IActionResult getMateriales(Material material)
        {
         
            return Ok("desde materiates");
        }
        [HttpGet("{id}")]
        public IActionResult Datos(string id)
        {

            return Ok("desde materiales:" + id);
        }

    }
}
