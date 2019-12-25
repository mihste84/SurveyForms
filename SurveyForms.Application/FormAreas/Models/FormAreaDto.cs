using System;

namespace SurveyForms.Application.FormAreas.Models
{
    public class FormAreaDto
    {
        public int FormAreaId { get; set; }
        public string Name { get; set; }
        public int FormCount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
    }
}
