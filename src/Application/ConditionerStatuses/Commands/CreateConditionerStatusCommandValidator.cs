using FluentValidation;

namespace Application.ConditionerStatuses.Commands;

public class CreateConditionerStatusCommandValidator : AbstractValidator<CreateConditionerStatusCommand>
{
    public CreateConditionerStatusCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
