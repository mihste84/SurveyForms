using MediatR;
using SurveyForms.Application.Common.Classes;
using SurveyForms.Application.FormAreas.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.FormAreas.Queries
{
    public class SearchFormAreaQuery : BasePagedQuery, IRequest<BasePagedSearchResult<FormAreaDto>>
    {

        public class SearchFormAreaHandler : IRequestHandler<SearchFormAreaQuery, BasePagedSearchResult<FormAreaDto>>
        {
            public Task<BasePagedSearchResult<FormAreaDto>> Handle(SearchFormAreaQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
