using System;
using System.Collections.Generic;

namespace SurveyForms.Application.Forms.Models
{
    public class FormDto
    {
        public int FormId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] TimeStamp { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public IEnumerable<FormItemDto> FormItems { get; set; }
    }
}
