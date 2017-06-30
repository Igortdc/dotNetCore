using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Service;
using ApiDotNetCoreConcept.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using ApiDotNetCore.Exceptions;

namespace ApiDotNetCoreConcept
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPrint, Print>();
            services.AddSingleton<IConfiguration>(Configuration);
                     
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddMvc();

            // ASP.NET API Versioning - Componente de Versionamento
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.Conventions.Controller<ValuesController>().HasApiVersion(GetMajorVersion(), GetMinorVersion());
            });

            // SWAGGER - Componente de Documentação dos Endpoints
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseApiVersioning();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        /// <summary>
        /// Recupera a MinorVersion da API
        /// </summary>
        /// <returns></returns>
        private int GetMinorVersion()
        {
            return Int32.Parse(Configuration.GetSection("AppSettings").GetSection("ApiVersion").GetSection("MinorVersion").Value);
        }

        /// <summary>
        /// Recupera a MajorVersion da API
        /// </summary>
        /// <returns></returns>
        private int GetMajorVersion()
        {
            return Int32.Parse(Configuration.GetSection("AppSettings").GetSection("ApiVersion").GetSection("MajorVersion").Value);
        }
    }
}
