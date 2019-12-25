using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using System;

namespace SurveyForms.Database.FormsAdminDb
{
    public static class FormAdminDbStartup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IFormAdminDbContext, FormAdminDbContext>(options => {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);
        }

        public static void RunDbMigrations(IServiceProvider services)
        {
            var context = services.GetService<IFormAdminDbContext>() as FormAdminDbContext;

            context.Database.Migrate();
        }
    }
}
