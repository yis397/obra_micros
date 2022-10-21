using Microsoft.AspNetCore.Identity;
using servicios_identificacion.Cors.Persistence;
using Trabajadores.Cors.Entity;

namespace Trabajadores.Cors.Persistence
{
    public class SeguridadData
    {
        public static async Task InsertarUsuario(SeguridadContexto contexto,UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user=new Usuario { Nombre="jose",Cargo="Arquitecto",Edad=2,Apellidos="Hernandez",Email="correo@hot.com"};
                await userManager.CreateAsync(user,"Password123@");
            }
        }
    }
}
