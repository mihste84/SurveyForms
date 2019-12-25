using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SurveyForms.Application.Common.Interfaces.Services;

namespace SurveyForms.Security.DevAuth
{
    public static class DevAuthStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, DevAuthService>();
        }

        public static IApplicationBuilder UseDevAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DevAuthenticationMiddleware>();
        }
    }
}
