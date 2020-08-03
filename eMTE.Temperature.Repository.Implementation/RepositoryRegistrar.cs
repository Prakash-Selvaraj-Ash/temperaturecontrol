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
            services.AddTransient<IRepository<TeamUserMap>, TeamUserMapRepository>();
            services.AddTransient<IRepository<Organization>, OrganizationRepository>();
        }
    }
}
