using System;
using eMTE.Temperature.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace eMTE.Temperature.Service.Implementation
{
    public static class ServiceRegistrar
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IHealthConfigurationService, HealthConfigurationService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IHealthMeasureService, HealthMeasureService>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
