using System;
using AutoMapper;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.BusinessLayer.Profiles
{
    public class HealthMeasureProfile : Profile
    {
        public HealthMeasureProfile()
        {
            CreateMap<CreateDayMeasure, DayMeasure>()
                .ForMember(dest => dest.UserId, opts => opts.Ignore())
                .ForMember(dest => dest.OrganizationId, opts => opts.Ignore())
                .ForMember(dest => dest.Id, opts => opts.Ignore());

            CreateMap<CreateHealthMeasure, HealthMeasure>()
                .ForMember(dest => dest.DayMeasureId, opts => opts.Ignore());

            CreateMap<HealthMeasure, GetHealthMeasure>();
        }
    }
}
