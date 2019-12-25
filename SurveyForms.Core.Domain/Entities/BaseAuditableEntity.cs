using System;

namespace SurveyForms.Core.Domain.Entities
{
    public abstract class BaseAuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
