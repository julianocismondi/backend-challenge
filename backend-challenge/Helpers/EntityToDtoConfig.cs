using AutoMapper;
using backend_challenge.Dto;
using backend_challenge.Models;

namespace backend_challenge.Helpers
{
    public class EntityToDtoConfig : Profile
    {
        public EntityToDtoConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
