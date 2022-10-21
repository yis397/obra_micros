using AuthenticacionManger.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trabajadores.Cors.Domain;
using Trabajadores.Cors.Dto;

namespace Trabajadores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsuariosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<AutenticacionResp>> Registro(Registro.UsuarioRegistroCommand user)
        {
            return await mediator.Send(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<AutenticacionResp>> Login(Login.UsuarioLoginCommand user)
        {
            return await mediator.Send(user);
        }
        [HttpGet]
        public ActionResult Saluda(Login.UsuarioLoginCommand user)
        {
            return Ok("Desde trabajadores");
        }
        [HttpGet("{id}")]
        public IActionResult Datos(string id)
        {

            return Ok("desde trabajadores:" + id);
        }

    }
}
