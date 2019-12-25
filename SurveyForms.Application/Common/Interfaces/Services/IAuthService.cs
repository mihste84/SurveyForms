using SurveyForms.Core.Values.Entities;
using System.Threading.Tasks;

namespace SurveyForms.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        string GetUserName();
        Task<AuthenticatedAppUser> GetAuthenticatedAppUser();
        Task<bool> IsMasterAdmin();
    }
}
