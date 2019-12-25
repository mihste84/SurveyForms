using System.Collections.Generic;
using System.Linq;

namespace SurveyForms.Core.Values.Entities
{
    public class AuthenticatedAppUser
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<int> FormAreaPermissionIds { get; set; }
        public bool? IsMasterAdmin { get; set; }

        public bool CheckIfMasterAdmin() => IsMasterAdmin.HasValue && IsMasterAdmin.Value;
        public bool HasFormAreaPermission(int formAreaId) => FormAreaPermissionIds.Contains(formAreaId);
    }

}
