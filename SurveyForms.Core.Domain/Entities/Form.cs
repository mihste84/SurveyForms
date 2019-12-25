using System.Collections.Generic;

namespace SurveyForms.Core.Domain.Entities
{
    public class Form : BaseAuditableEntity
    {
        public int FormId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] TimeStamp { get; set; }
        public int? FormAreaId { get; set; }
        public FormArea FormArea { get; set; }
        public ICollection<FormItem> FormItems { get; set; } = new HashSet<FormItem>();
    }
}
