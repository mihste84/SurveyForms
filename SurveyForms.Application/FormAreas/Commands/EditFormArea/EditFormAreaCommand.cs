using MediatR;
using System;
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
            public Task<int> Handle(EditFormAreaCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
