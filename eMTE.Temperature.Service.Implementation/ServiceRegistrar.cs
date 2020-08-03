using System;
using eMTE.Temperature.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace eMTE.Temperature.Service.Implementation
{
    public static class ServiceRegistrar
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<ITeamService, TeamService>();
        }
    }
}
