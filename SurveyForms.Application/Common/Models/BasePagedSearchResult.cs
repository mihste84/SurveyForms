using System.Collections.Generic;

namespace SurveyForms.Application.Common.Classes
{
    public class BasePagedSearchResult<M>
    {
        public IEnumerable<M> PageData { get; set; }
        public int TotalItems { get; set; }
    }
}
