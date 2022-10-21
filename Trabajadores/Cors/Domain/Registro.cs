using AuthenticacionManger;
using AuthenticacionManger.Models;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using servicios_identificacion.Cors.Persistence;
using Trabajadores.Cors.Domain.Helper;
using Trabajadores.Cors.Dto;
using Trabajadores.Cors.Entity;

namespace Trabajadores.Cors.Domain
{
    public class Registro
    {
        public class UsuarioRegistroCommand : IRequest<AutenticacionResp>
        {
            public string Nombre   { get; set; } = "";
            public string Apellido { get; set; } = "";
            public string Email    { get; set; } = "";
            public string Cargo    { get; set; } = "";
            public string UserName    { get; set; } = "";
            public int Edad    { get; set; } = 0;
            public string Password { get; set; } = "";
            public string Rol { get; set; } = "";
        }
        public class UserValidation : AbstractValidator<UsuarioRegistroCommand>
        {
            public UserValidation()
            {
                RuleFor(c => c.Apellido).NotEmpty();
                RuleFor(c => c.Nombre).NotEmpty();
                RuleFor(c => c.Email).NotEmpty().EmailAddress();
                RuleFor(c => c.Cargo).NotEmpty();
                RuleFor(c => c.Rol).NotEmpty();
                RuleFor(c => c.UserName).NotEmpty();
                RuleFor(c => c.Password).NotEmpty().MinimumLength(6);
            }
        }
        public class UsuarioRegistroHandler : IRequestHandler<UsuarioRegistroCommand, AutenticacionResp>
        {
            private readonly SeguridadContexto _seguridadContexto;
            private readonly UserManager<Usuario> _mangerUser;
            private readonly IMapper _mapper;
            //private readonly IJwtGenerator jwt;
            private readonly JwTokenHandler _jsonWebToken;



            public UsuarioRegistroHandler(SeguridadContexto seguridadContexto, UserManager<Usuario> mangerUser, IMapper mapper, JwTokenHandler jsonWebToken)
            {
                _seguridadContexto = seguridadContexto;
                _mangerUser = mangerUser;
                _mapper = mapper;
                //this.jwt = jwt;
                _jsonWebToken = jsonWebToken;
            }

            public async Task<AutenticacionResp> Handle(UsuarioRegistroCommand request, CancellationToken cancellationToken)
            {
                var exist= _seguridadContexto.Users.Where(x => x.Email == request.Email);

                if (exist.Any()) throw new Exception("Usuario existente");
                var newUser=new Usuario { Nombre=request.Nombre,Apellidos=request.Apellido,Email=request.Email,Cargo=request.Cargo,UserName=request.UserName,Rol=request.Rol};
                var result = await _mangerUser.CreateAsync(newUser, request.Password);
                if(!result.Succeeded) throw new Exception("Error en Usuario");
                //var userMap=_mapper.Map<Usuario,UsuarioDto>(newUser);
                //userMap.Token=jwt.CreateToken(newUser);
                
                return _jsonWebToken.GenerarToke(new AutenticacionReq { UserName = newUser.UserName, Role = newUser.Rol, Password = request.Password }); ;
            }
        }
    }
}
