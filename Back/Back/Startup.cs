using Back.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using Microsoft.EntityFrameworkCore;
using Back.Domain.IRepositories;
using Back.Persistence.Repository;
using Back.Domain.IService;
using Back.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Back
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
         services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(Configuration.GetConnectionString("Conexion")));
         services.AddControllers();
         //Repositorios
         services.AddScoped<IUsuarioRepository, UsuarioRepository>();
         services.AddScoped<ILoginRepository, LoginRepository>();
         services.AddScoped<ICuestionarioRepository, CuestionarioRepository>();

         //Servicios
         services.AddScoped<IUsuarioService, UsuarioService>();
         services.AddScoped<ILoginService, LoginService>();
         services.AddScoped<ICuestionarioService, CuestionarioService>();
         //Cors
         services.AddCors(options => options.AddPolicy("Allowebapp",
            builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            ));
         //Add Authentication
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
             
               options.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuer = true,
                  ValidateAudience = false,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = Configuration["Jwt:Issuer"],
                  ValidAudience = Configuration["Jwt: Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                  ClockSkew = TimeSpan.Zero
               

      });
         services.AddControllers().AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);

                  services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Back", Version = "v1" });
         });

      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Back v1"));
         }
         app.UseCors("Allowebapp");

         app.UseRouting();
         app.UseAuthentication();
         app.UseAuthorization();
        
         app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
