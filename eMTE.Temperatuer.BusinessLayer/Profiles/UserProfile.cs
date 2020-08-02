using AutoMapper;
using eMTE.Temperature.Domain;
using eMTE.Temperature.BusinessLayer.DTO.User.Request;

namespace eMTE.Temperature.BusinessLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUser, User>()
                .ForMember(dest => dest.Id, opts => opts.Ignore());
        }
    }
}
