using MediatR;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Application.Common.Interfaces.Services;
using SurveyForms.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyForms.Application.FormAreas.Commands.CreateFormArea
{
    public class CreateFormAreaCommand : IRequest<int>
    {
        public string Name { get; set; }


        public class CreateFormAreaHandler : IRequestHandler<CreateFormAreaCommand, int>
        {
            private readonly IAuthService _authService;
            private readonly IFormAdminDbContext _context;

            public CreateFormAreaHandler(IAuthService authService, IFormAdminDbContext context)
            {
                _authService = authService;
                _context = context;
            }

            public async Task<int> Handle(CreateFormAreaCommand request, CancellationToken cancellationToken)
            {
                var newFromArea = new FormArea
                {
                    Name = request.Name
                };

                await _context.FormAreas.AddAsync(newFromArea);

                return await _context.SaveAuditableChangesAsync(_authService.GetUserName());
            }
        }
    }
}
