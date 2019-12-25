using Microsoft.AspNetCore.Identity;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Application.Exceptions;
using SurveyForms.Core.Domain.Common;
using SurveyForms.Core.Values.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyForms.Database.AuthDb
{
    public class AppUserManagerService : IUserManagerService
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserManagerService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AuthenticatedAppUser> GetAuthenticatedAppUser(string usernameOrMail)
        {
            var user = await _userManager.FindByNameAsync(usernameOrMail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrMail);
            }
            if (user == null) throw new AuthException($"Cannot find user with username/email '{usernameOrMail}'.");

            var claims = await _userManager.GetClaimsAsync(user);

            return new AuthenticatedAppUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                FormAreaPermissionIds = claims?.Where(_ => _.Type.Equals(ClaimKeys.FormAreaPermissions)).Select(_ => int.Parse(_.Value)),
                IsMasterAdmin = claims?.Any(_ => _.Type.Equals(ClaimKeys.MasterAdmin))
            };
        }
    }
}
