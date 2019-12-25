using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Application.Exceptions;
using SurveyForms.Application.Forms.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.Forms.Queries.GetFormsById
{
    public class GetFormsByIdQuery : IRequest<IEnumerable<FormDto>>
    {
        public int? FormAreaId { get; set; }

        public class GetFormsByIdHandler : IRequestHandler<GetFormsByIdQuery, IEnumerable<FormDto>>
        {
            private readonly IAuthService _authService;
            private readonly IFormAdminDbContext _context;

            public GetFormsByIdHandler(IAuthService authService, IFormAdminDbContext context)
            {
                _authService = authService;
                _context = context;
            }

            public async Task<IEnumerable<FormDto>> Handle(GetFormsByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _authService.GetAuthenticatedAppUser();
                if ((user.IsMasterAdmin.HasValue && user.IsMasterAdmin.Value) || user.HasFormAreaPermission(request.FormAreaId.Value))
                {
                    var forms = _context.Forms
                    .Include(_ => _.FormItems)
                    .Where(_ => _.FormAreaId == request.FormAreaId)
                    .Select(_ => new FormDto
                    {
                        FormId = _.FormId,
                        Name = _.Name,
                        Description = _.Description,
                        TimeStamp = _.TimeStamp,
                        FormItems = _.FormItems.Select(x => new FormItemDto
                        {
                            FormItemId = x.FormItemId,
                            Name = x.Name,
                            TimeStamp = x.TimeStamp,
                            Created = x.Created,
                            CreatedBy = x.CreatedBy,
                            Updated = x.Updated,
                            UpdatedBy = x.UpdatedBy
                        }),
                        Created = _.Created,
                        CreatedBy = _.CreatedBy,
                        Updated = _.Updated,
                        UpdatedBy = _.UpdatedBy
                    });

                    return forms;
                }

                throw new AuthException($"User '{_authService.GetUserName()}' is not authorized to view the selected Form area.");
            }          
        }
    }
}
