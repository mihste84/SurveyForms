using System.Collections.Generic;

namespace SurveyForms.Core.Domain.Entities
{
    public class FormArea : BaseAuditableEntity
    {
        public int FormAreaId { get; set; }
        public string Name { get; set; }
        public ICollection<Form> Forms { get; set; } = new HashSet<Form>();
    }
}
