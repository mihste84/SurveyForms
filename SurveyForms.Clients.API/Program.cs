using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SurveyForms.Database.AuthDb;
using SurveyForms.Database.FormAdminDb;
using SurveyForms.Database.FormsAdminDb;

namespace SurveyForms.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetService<IWebHostEnvironment>();

                FormAdminDbStartup.RunDbMigrations(services);
                AuthDbStartup.RunDbMigrations(services);

                if (env.IsDevelopment())
                {
                    FormAdminSeedData.SeedDevFormAreas(services).Wait();
                    IdentitySeedData.SeedDevUser(services).Wait();
                }

                IdentitySeedData.SeedMasterAdmin(services).Wait();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
