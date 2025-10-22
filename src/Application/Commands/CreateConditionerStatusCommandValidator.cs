using FluentValidation;

namespace Application.Commands;

public class CreateConditionerStatusCommandValidator : AbstractValidator<CreateConditionerStatusCommand>
{
    public CreateConditionerStatusCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
    }
}
