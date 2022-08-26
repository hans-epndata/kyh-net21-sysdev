using AutoMapper;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AccountEntity, Account>().ReverseMap();
            CreateMap<AccountEntity, AccountTokenHandler>();

            CreateMap<SignUp, AccountEntity>();
            CreateMap<SignIn, AccountEntity>();
        }
    }
}
