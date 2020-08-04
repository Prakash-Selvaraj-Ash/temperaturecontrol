using System;
using Microsoft.Extensions.DependencyInjection;

namespace eMTE.Common.Authentication
{
    public static class AuthCommonRegistrar
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IAuthenticator, Authenticator>();
            services.AddTransient<IUserResolver, UserResolver>();
        }
    }
}
