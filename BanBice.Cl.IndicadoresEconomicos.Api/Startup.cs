using BanBice.Cl.IndicadoresEconomicos.Api.Entities;
using BanBice.Cl.IndicadoresEconomicos.Api.Helpers;
using BanBice.Cl.IndicadoresEconomicos.Api.Repositories;
using BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;

namespace BanBice.Cl.IndicadoresEconomicos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddScoped<IIndicadorEconomicoRepository, IndicadorEconomicoRepository>();
            services.AddScoped<IFavoritoRepository, FavoritoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOriginsHeadersAndMethods",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddDbContext<IndicadoresEconomicosContext>( options => {
                options.UseSqlServer(string.Format(@"Server={0};Database={1};Trusted_Connection=true;", AppVariables.ServidorSql, AppVariables.NombreBaseDatos));
            });

            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtHelper.GetSymmetricSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("ocurrio un problema intente luego");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAllOriginsHeadersAndMethods");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {  endpoints.MapControllers();   });
        }
    }
}
