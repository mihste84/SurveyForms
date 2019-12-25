using FluentValidation;

namespace SurveyForms.Application.Forms.Commands.DeleteForm
{
    public class DeleteFormValidation : AbstractValidator<DeleteFormCommand>
    {
        public DeleteFormValidation()
        {
            RuleFor(_ => _.FormId).NotEmpty();
        }
    }
}
