using FluentValidation;

namespace Application.Commands;

public class CreateConditionerTypeCommandValidator : AbstractValidator<CreateConditionerTypeCommand>
{
    public CreateConditionerTypeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
    }
}
