using System;

namespace SurveyForms.Application.Forms.Models
{
    public class FormItemDto
    {
        public int FormItemId { get; set; }
        public string Name { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
