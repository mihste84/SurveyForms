using FluentValidation;

namespace SurveyForms.Application.Forms.Commands.AddNewForm
{
    public class AddNewFormValidation : AbstractValidator<AddNewFormCommand>
    {
        public AddNewFormValidation()
        {
            RuleFor(_ => _.AreaId).NotEmpty();
            RuleFor(_ => _.Description).MaximumLength(300);
            RuleFor(_ => _.Name).MaximumLength(50).MinimumLength(3).NotEmpty();
        }
    }
}
