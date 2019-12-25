using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurveyForms.Application.Common.Interfaces.Services;
using System;

namespace SurveyForms.Database.AuthDb
{
    public static class AuthDbStartup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AuthDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddIdentityCore<AppUser>(opts => {
                opts.Password.RequiredLength = 8;
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthDbContext>();

            services.AddTransient<IUserManagerService, AppUserManagerService>();
        }

        public static void RunDbMigrations(IServiceProvider services)
        {
            var context = services.GetService<AuthDbContext>();

            context.Database.Migrate();
        }
    }
}
