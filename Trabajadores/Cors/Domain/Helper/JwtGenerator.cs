using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trabajadores.Cors.Entity;

namespace Trabajadores.Cors.Domain.Helper
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim> {
            new Claim("username",usuario.UserName),
            new Claim("nombre",usuario.Nombre),
            new Claim("email",usuario.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C7q3FBCJZq0bIRRH0Dq4lxWuBipEBkHX"));
            var credencial=new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credencial
            };
            var tokenHandeler = new JwtSecurityTokenHandler();
            var token = tokenHandeler.CreateToken(tokenDescripcion);
            return tokenHandeler.WriteToken(token);
        }
    }
}
