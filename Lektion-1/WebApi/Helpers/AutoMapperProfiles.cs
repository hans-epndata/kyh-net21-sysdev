using AutoMapper;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserEntity, User>().ReverseMap();
        }
    }
}
