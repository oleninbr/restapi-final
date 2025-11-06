using FluentValidation;

namespace Application.ConditionerStatuses.Commands;

public class UpdateConditionerStatusCommandValidator : AbstractValidator<UpdateConditionerStatusCommand>
{
    public UpdateConditionerStatusCommandValidator()
    {
        RuleFor(x => x.ConditionerStatusId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
