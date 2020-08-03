using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using eMTE.Common.Authentication;
using eMTE.Common.DataAccess;
using eMTE.Temperature.BusinessLayer;
using eMTE.Temperature.DataAccess;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Repository.Implementation;
using eMTE.Temperature.Service.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace eMTE.Temperature
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<AppDbContext>(builder =>
                   builder.UseMySql("Server=localhost;user=root;password=17031991AE#;Database=TemperatureControlDB;", sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my temperature security key"));

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

            services.AddTransient<IQueryableConnector, TemperatureDbConnector>();
            services.AddTransient<IEntityService, EntityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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


            //app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
