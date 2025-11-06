using FluentValidation;

namespace Application.ConditionerTypes.Commands;

public class CreateConditionerTypeCommandValidator : AbstractValidator<CreateConditionerTypeCommand>
{
    public CreateConditionerTypeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
