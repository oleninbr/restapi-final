using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateConditionerStatusDtoValidator : AbstractValidator<CreateConditionerStatusDto>
{
    public CreateConditionerStatusDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    }
}
