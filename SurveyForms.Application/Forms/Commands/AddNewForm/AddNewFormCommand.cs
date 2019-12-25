using MediatR;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Application.Exceptions;
using SurveyForms.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.Forms.Commands.AddNewForm
{
    public class AddNewFormCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AreaId { get; set; }

        public class AddNewFormHandler : IRequestHandler<AddNewFormCommand, int>
        {
            private readonly IAuthService _authService;
            private readonly IFormAdminDbContext _context;

            public AddNewFormHandler(IAuthService authService, IFormAdminDbContext context)
            {
                _authService = authService;
                _context = context;
            }

            public async Task<int> Handle(AddNewFormCommand request, CancellationToken cancellationToken)
            {
                var user = await _authService.GetAuthenticatedAppUser();

                if ((user.IsMasterAdmin.HasValue && user.IsMasterAdmin.Value) || user.HasFormAreaPermission(request.AreaId.Value))
                {
                    var newForm = new Form
                    {
                        FormAreaId = request.AreaId.Value,
                        Name = request.Name,
                        Description = request.Description
                    };

                    await _context.Forms.AddAsync(newForm);

                    return await _context.SaveAuditableChangesAsync(user.Username);
                }

                throw new AuthException($"User '{_authService.GetUserName()}' is not authorized to add new forms in area.");
            }
        }
    }
}
