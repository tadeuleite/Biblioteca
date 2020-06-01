using BibliotecaSenac.Business;
using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Repository;
using BibliotecaSenac.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace BibliotecaSenac
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca API", Version = "v1" });
            });

            services = InjecaoDependencia(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();


            app.UseMvc();
        }

        private IServiceCollection InjecaoDependencia(IServiceCollection services)
        {
            #region Business
            services.AddScoped<IAlunoBusiness, AlunoBusiness>();

            #endregion

            #region Repository
            services.AddScoped<IAlunoRepository, AlunoRepository>();

            #endregion
            return services;
        }
    }
}
