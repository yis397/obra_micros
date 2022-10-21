using Trabajadores.Cors.Entity;

namespace Trabajadores.Cors.Domain.Helper
{
    public interface IJwtGenerator
    {
        string CreateToken(Usuario usuario);
    }
}
