using AutoMapper;
using eMTE.Temperature.Domain;
using eMTE.Temperature.BusinessLayer.DTO.User.Request;
using System;
using eMTE.Temperature.BusinessLayer.DTO.Organization.Request;
using eMTE.Temperature.BusinessLayer.DTO.User.Response;

namespace eMTE.Temperature.BusinessLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUser, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
            CreateMap<User, GetUserDetailResponse>();
            CreateMap<CreateOrganization, User>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
            CreateMap<User, GetUserResponse>();
            CreateMap<UpdateUser, User>();
        }
    }
}
