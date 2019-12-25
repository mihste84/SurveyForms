using MediatR;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Application.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.FormAreas.Commands.EditFormArea
{
    public class EditFormAreaCommand : IRequest<int>
    {
        public int? AreaId { get; set; }
        public string Name { get; set; }
        public class EditFormAreaHandler : IRequestHandler<EditFormAreaCommand, int>
        {
            private readonly IAuthService _authService;
            private readonly IFormAdminDbContext _context;

            public EditFormAreaHandler(IAuthService authService, IFormAdminDbContext context)
            {
                _authService = authService;
                _context = context;
            }

            public async Task<int> Handle(EditFormAreaCommand request, CancellationToken cancellationToken)
            {
                var user = await _authService.GetAuthenticatedAppUser();

                if ((user.IsMasterAdmin.HasValue && user.IsMasterAdmin.Value) || user.HasFormAreaPermission(request.AreaId.Value))
                {
                    var area = _context.FormAreas.FirstOrDefault(_ => _.FormAreaId == request.AreaId);
                    if (area == null) throw new NotFoundException($"Could not perform operation. Form area not found in database.");

                    area.Name = request.Name;

                    _context.FormAreas.Update(area);

                    return await _context.SaveAuditableChangesAsync(user.Username);
                }

                throw new AuthException($"User '{_authService.GetUserName()}' is not authorized to edit form area.");
            }
        }
    }
}
