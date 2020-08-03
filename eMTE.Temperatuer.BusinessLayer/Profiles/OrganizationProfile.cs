using System;
using AutoMapper;
using eMTE.Temperature.BusinessLayer.DTO.Organization.Request;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.BusinessLayer.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<CreateOrganization, Organization>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.OrganizationName))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()));
        }
    }
}
