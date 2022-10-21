using AutoMapper;
using Trabajadores.Cors.Entity;

namespace Trabajadores.Cors.Dto
{
    public class MapUser : Profile
    {
        public MapUser()
        {
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
