using FluentValidation;
using SurveyForms.Application.Common.Classes;

namespace SurveyForms.Application.Common.Validators
{
    public class BasePagedQueryValidator : AbstractValidator<BasePagedQuery>
    {
        public BasePagedQueryValidator()
        {
            RuleFor(_ => _.Page).NotEmpty();
            RuleFor(_ => _.PageSize).NotEmpty();
            RuleFor(_ => _.SortColumn).MaximumLength(50);
            RuleFor(_ => _.SortOrder).MaximumLength(50).Must(_ => _.Equals("asc") || _.Equals("desc"));
        }
    }
}
