using System;
using AutoMapper;
using eMTE.Temperature.BusinessLayer.Extensions;
using eMTE.Temperature.BusinessLayer.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace eMTE.Temperature.BusinessLayer
{
    public static class BusinessRegistrar
    {
        public static void Register(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new OrganizationProfile());
                mc.AddProfile(new TeamProfile()); 
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new HealthMeasureProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            MapperExtensions.Mapper = mapper;
        }
    }
}
