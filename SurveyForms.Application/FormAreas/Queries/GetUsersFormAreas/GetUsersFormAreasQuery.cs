using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Application.FormAreas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.FormAreas.Queries
{
    public class GetUsersFormAreasQuery : IRequest<IEnumerable<FormAreaDto>>
    {

        public class GetUsersFormAreasHandler : IRequestHandler<GetUsersFormAreasQuery, IEnumerable<FormAreaDto>>
        {
            private readonly IAuthService _authService;
            private readonly IFormAdminDbContext _context;

            public GetUsersFormAreasHandler(IAuthService authService, IFormAdminDbContext context)
            {
                _authService = authService;
                _context = context;
            }

            public async Task<IEnumerable<FormAreaDto>> Handle(GetUsersFormAreasQuery request, CancellationToken cancellationToken)
            {
                var user = await _authService.GetAuthenticatedAppUser();

                var areas = user.CheckIfMasterAdmin()
                    ? _context.FormAreas
                    : _context.FormAreas.Where(_ => user.FormAreaPermissionIds.Contains(_.FormAreaId));

                return areas.Include(_ => _.Forms)
                    .Select(_ => new FormAreaDto
                    {
                        Name = _.Name,
                        FormCount = _.Forms.Count(),
                        Created = _.Created,
                        CreatedBy = _.CreatedBy,
                        FormAreaId = _.FormAreaId
                    });
            }

        }
    }
}
