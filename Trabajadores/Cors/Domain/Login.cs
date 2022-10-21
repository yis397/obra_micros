using AuthenticacionManger;
using AuthenticacionManger.Models;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using servicios_identificacion.Cors.Persistence;
using Trabajadores.Cors.Domain.Helper;
using Trabajadores.Cors.Dto;
using Trabajadores.Cors.Entity;
using static Trabajadores.Cors.Domain.Registro;

namespace Trabajadores.Cors.Domain
{
    public class Login
    {
        public class UsuarioLoginCommand : IRequest<AutenticacionResp>
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }
        public class UsuarioValidation : AbstractValidator<UsuarioLoginCommand>
        {
            public UsuarioValidation()
            {
                RuleFor(c => c.Email).NotEmpty();
                RuleFor(c => c.Password).NotEmpty();
            }
        }
        public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginCommand, AutenticacionResp>
        {
            private readonly SeguridadContexto _seguridadContexto;
            private readonly UserManager<Usuario> _mangerUser;
            private readonly IMapper _mapper;
            //private readonly IJwtGenerator jwt;
            private readonly JwTokenHandler _jsonWebToken;

            private readonly SignInManager<Usuario> signInManager;

            public UsuarioLoginHandler(SeguridadContexto seguridadContexto, UserManager<Usuario> mangerUser, IMapper mapper, SignInManager<Usuario> signInManager, JwTokenHandler jsonWebToken)
            {
                _seguridadContexto = seguridadContexto;
                _mangerUser = mangerUser;
                _mapper = mapper;
                //this.jwt = jwt;
                this.signInManager = signInManager;
                _jsonWebToken = jsonWebToken;
            }

            public async Task<AutenticacionResp> Handle(UsuarioLoginCommand request, CancellationToken cancellationToken)
            {
                var usuario=await _mangerUser.FindByEmailAsync(request.Email);
                if (usuario==null) throw new Exception("Error en email");
               var result=await signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                if (!result.Succeeded) throw new Exception("Error en email");
                //var userDto = _mapper.Map<Usuario, UsuarioDto>(usuario);
                //userDto.Token = jwt.CreateToken(usuario);
                return _jsonWebToken.GenerarToke(new AutenticacionReq { UserName = usuario.UserName, Role = usuario.Rol, Password = request.Password });
            }
        }

        }
 }
