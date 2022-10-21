using Microsoft.AspNetCore.Identity;

namespace Trabajadores.Cors.Entity
{
    public class Usuario: IdentityUser
    {
        public string Nombre { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Cargo { get; set; } = "";
        public string Rol { get; set; } = "";
        public int Edad { get; set; } = 0;

    }
}
