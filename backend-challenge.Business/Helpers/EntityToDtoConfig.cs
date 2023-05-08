using AutoMapper;
using backend_challenge.Business.Dtos;
using backend_challenge.Domain.Entities;

namespace backend_challenge.Business.Helpers
{
    public class EntityToDtoConfig : Profile
    {
        public EntityToDtoConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, AuthDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}
