using FluentValidation;

namespace SurveyForms.Application.FormAreas.Commands.EditFormArea
{
    public class EditFormAreaValidator : AbstractValidator<EditFormAreaCommand>
    {
        public EditFormAreaValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(50).MinimumLength(3);
            RuleFor(_ => _.AreaId).NotEmpty();
        }
    }
}
