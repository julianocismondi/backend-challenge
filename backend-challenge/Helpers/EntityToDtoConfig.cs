using AutoMapper;
using backend_challenge.Dto;
using backend_challenge.Models;
using backend_challenge.Models.Dto;

namespace backend_challenge.Helpers
{
    public class EntityToDtoConfig : Profile
    {
        public EntityToDtoConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, AuthDto>().ReverseMap();
        }
    }
}
