using MediatR;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Application.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.Forms.Commands.DeleteForm
{
    public class DeleteFormCommand : IRequest<int>
    {
        public int? FormId { get; set; }

        public class DeleteFormHandler : IRequestHandler<DeleteFormCommand, int>
        {
            private readonly IAuthService _authService;
            private readonly IFormAdminDbContext _context;

            public DeleteFormHandler(IAuthService authService, IFormAdminDbContext context)
            {
                _authService = authService;
                _context = context;
            }

            public async Task<int> Handle(DeleteFormCommand request, CancellationToken cancellationToken)
            {
                var formToDelete = _context.Forms.FirstOrDefault(_ => _.FormId == request.FormId);
                if (formToDelete == null) throw new NotFoundException($"Could not perform operation. Form not found in database.");

                var user = await _authService.GetAuthenticatedAppUser();
                
                if ((user.IsMasterAdmin.HasValue && user.IsMasterAdmin.Value) || user.HasFormAreaPermission(formToDelete.FormAreaId.Value))
                {
                    _context.Remove(formToDelete);

                    return await _context.SaveChangesAsync();
                }

                throw new AuthException($"User '{_authService.GetUserName()}' is not authorized to delete the form.");
            }
        }
    }
}
