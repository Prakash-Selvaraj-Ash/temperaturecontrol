using System;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace eMTE.Temperature.Repository.Implementation
{
    public static class RepositoryRegistrar
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Team>, TeamRepository>();
            services.AddTransient<IRepository<DayMeasure>, DayMeasureRepository>();
            services.AddTransient<IRepository<TeamUserMap>, TeamUserMapRepository>();
            services.AddTransient<IRepository<Organization>, OrganizationRepository>();
            services.AddTransient<IRepository<HealthMeasure>, HealthMeasureRepository>();
            services.AddTransient<IRepository<HealthMeasureConfiguration>, HealthMeasureConfigurationRepository>();
        }
    }
}
