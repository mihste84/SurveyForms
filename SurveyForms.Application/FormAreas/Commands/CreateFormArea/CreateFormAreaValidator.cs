using FluentValidation;

namespace SurveyForms.Application.FormAreas.Commands
{
    public class CreateFormAreaValidator : AbstractValidator<CreateFormAreaCommand>
    {
        public CreateFormAreaValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(50).MinimumLength(3);
        }
    }
}
