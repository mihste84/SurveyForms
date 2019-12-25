using Microsoft.AspNetCore.Identity;

namespace SurveyForms.Database.AuthDb
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
