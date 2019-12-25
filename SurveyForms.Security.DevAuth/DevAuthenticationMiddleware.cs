using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SurveyForms.Security.DevAuth
{
    public class DevAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public DevAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var claims = new List<Claim>();
            var identity = new DevIdentity(claims);
            context.User = new ClaimsPrincipal(identity);
            await _next(context);
        }
    }
}
