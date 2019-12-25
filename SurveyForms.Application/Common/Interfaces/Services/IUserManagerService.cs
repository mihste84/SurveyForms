using SurveyForms.Core.Values.Entities;
using System.Threading.Tasks;

namespace SurveyForms.Application.Common.Interfaces.Services
{
    public interface IUserManagerService
    {
        Task<AuthenticatedAppUser> GetAuthenticatedAppUser(string usernameOrMail);
    }
}
