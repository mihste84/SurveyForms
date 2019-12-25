using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SurveyForms.Application;
using SurveyForms.Clients.API.Filters;
using SurveyForms.Database.AuthDb;
using SurveyForms.Database.FormsAdminDb;
using SurveyForms.Security.DevAuth;
using System;

namespace SurveyForms.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("FormsAdminDbContext");
            var authConnectionString = Configuration.GetConnectionString("AuthDbContext");

            Action<MvcOptions> mvcOptions;
            if (_env.IsDevelopment())
            {
                DevAuthStartup.ConfigureServices(services);
                mvcOptions = (opts) => {
                    opts.Filters.Add<GlobalExceptionFilter>();
                    opts.Filters.Add<AllowAnonymousFilter>();
                };
            } else
            {
                mvcOptions = (opts) => {
                    opts.Filters.Add<GlobalExceptionFilter>();
                };
            }

            AuthDbStartup.ConfigureServices(services, authConnectionString);
            FormAdminDbStartup.ConfigureServices(services, connectionString);
            ApplicationStartup.ConfigureServices(services);

            services.AddHttpContextAccessor();
            services.AddControllers()
                .AddMvcOptions(mvcOptions);

            services.AddCors();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDevAuth();
            } else
            {
                app.UseHsts();
            }

            app.UseCors(_ => _.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
