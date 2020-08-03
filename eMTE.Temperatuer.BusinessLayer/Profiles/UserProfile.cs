using AutoMapper;
using eMTE.Temperature.Domain;
using eMTE.Temperature.BusinessLayer.DTO.User.Request;
using System;
using eMTE.Temperature.BusinessLayer.DTO.Organization.Request;

namespace eMTE.Temperature.BusinessLayer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUser, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
            CreateMap<CreateOrganization, User>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
        }
    }
}
