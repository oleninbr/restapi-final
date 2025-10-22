using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateConditionerTypeDtoValidator : AbstractValidator<CreateConditionerTypeDto>
{
    public CreateConditionerTypeDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    }
}
