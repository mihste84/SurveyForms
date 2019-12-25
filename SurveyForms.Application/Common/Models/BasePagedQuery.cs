using System.Collections.Generic;

namespace SurveyForms.Application.Common.Classes
{
    public class BasePagedQuery
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public IEnumerable<object> Filters { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}
