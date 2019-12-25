namespace SurveyForms.Core.Domain.Entities
{
    public class FormItem : BaseAuditableEntity
    {
        public int FormItemId { get; set; }
        public string Name { get; set; }
        public Form Form { get; set; }
        public int? FormId { get; set; }
        public byte[] TimeStamp { get; set; }
    }
}
