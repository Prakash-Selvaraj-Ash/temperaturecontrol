using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using eMTE.Common.Authentication;
using eMTE.Common.DataAccess;
using eMTE.Common.Tools.Contract;
using eMTE.Common.Tools.Implementation;
using eMTE.Temperature.BusinessLayer;
using eMTE.Temperature.DataAccess;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Repository.Implementation;
using eMTE.Temperature.Service.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static eMTE.Temperature.BusinessLayer.Constants.Constants;

namespace eMTE.Temperature
{
    public class DbConnectionModel
    {
        public string Host { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
    }

    

    public class Startup
    {
        private readonly string cleverCloudConnSection = "CleverCloud";
        private readonly string localConnSection = "Local";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

#if !DEBUG 
            var dbConn = Configuration.GetValue<string>($"DbConnectionStrings:{cleverCloudConnSection}");
#else
            var dbConn = Configuration.GetValue<string>($"DbConnectionStrings:{localConnSection}");
#endif

            services.AddDbContext<AppDbContext>(builder =>
                   builder.UseMySql(dbConn, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret.SecretKey));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signinKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidateActor = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddControllers();

            AuthCommonRegistrar.Register(services);

            RepositoryRegistrar.Register(services);
            ServiceRegistrar.Register(services);
            BusinessRegistrar.Register(services);


            services.AddTransient<IDateTimeToolService, DateTimeToolService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IQueryableConnector, TemperatureDbConnector>();
            services.AddTransient<IEntityService, EntityService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(c =>
            {
                c.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
