using AuthenticacionManger.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AuthenticacionManger
{
    public class JwTokenHandler
    {
        public const string JWT_KEY= "C7q3FBCJZq0bIRRH0Dq4lxWuBipEBkHX";
        private const int JWT_TOKEN_VALID_MIN = 20;
        private readonly List<User> _listUser;
        public JwTokenHandler()
        {
            _listUser = new List<User>() {new User{UserName="jess",Password="123456",Role="User" }, new User { UserName = "joss", Password = "123456", Role = "Administrador" }, };

        }
        public AutenticacionResp GenerarToke(AutenticacionReq existUser)
        {

            var tokenExpired= DateTime.Now.AddMinutes(JWT_TOKEN_VALID_MIN);
            var tokenKey=Encoding.ASCII.GetBytes(JWT_KEY);

            var claims =new ClaimsIdentity (new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Name,existUser.UserName),
            new Claim(ClaimTypes.Role,existUser.Role),
            });

            var signCredencial=new SigningCredentials (new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature);
            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signCredencial
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var security=jwtSecurityTokenHandler.CreateToken(tokenDescripcion);
            var token =jwtSecurityTokenHandler.WriteToken(security);
            return new AutenticacionResp {UserName=existUser.UserName, ExpiresIn=(int)tokenExpired.Subtract(DateTime.Now).TotalSeconds,JwToken=token };
        }
    }
}
