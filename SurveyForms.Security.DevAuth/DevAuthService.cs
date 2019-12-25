using Microsoft.AspNetCore.Http;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Application.Exceptions;
using SurveyForms.Core.Values.Entities;
using System.Threading.Tasks;

namespace SurveyForms.Security.DevAuth
{
    public class DevAuthService : IAuthService
    {
        private readonly IHttpContextAccessor _context;
        private readonly IUserManagerService _userManager;

        public DevAuthService(IHttpContextAccessor context, IUserManagerService userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AuthenticatedAppUser> GetAuthenticatedAppUser()
        {
            var username = GetUserName();
            if (string.IsNullOrEmpty(username))
                throw new AuthException("Cannot get app user. Current user is not authenticated.");

            var user = await _userManager.GetAuthenticatedAppUser(username);
            if (user == null) throw new AuthException($"User '{GetUserName()}' does is not registered.");

            return user;
        }

        public async Task<bool> IsMasterAdmin() {
            var user = await GetAuthenticatedAppUser();
            if (user == null) throw new AuthException("Authenticated user does not exist in the user database.");

            return user.IsMasterAdmin.HasValue && user.IsMasterAdmin.Value;
        }
        

        public string GetUserName() => _context.HttpContext.User.Identity.Name;
    }
}
