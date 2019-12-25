using SurveyForms.Core.Domain.Common;
using System.Collections.Generic;
using System.Security.Claims;

namespace SurveyForms.Security.DevAuth
{
    public class DevIdentity : ClaimsIdentity
    {
        public DevIdentity(IEnumerable<Claim> claims) : base(claims)
        { }

        public override bool IsAuthenticated => true;
        public override string Name => DevAdmin.Username;
    }
}
