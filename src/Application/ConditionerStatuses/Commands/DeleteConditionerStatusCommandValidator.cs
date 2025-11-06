using FluentValidation;

namespace Application.ConditionerStatuses.Commands;

public class DeleteConditionerStatusCommandValidator : AbstractValidator<DeleteConditionerStatusCommand>
{
    public DeleteConditionerStatusCommandValidator()
    {
        RuleFor(x => x.ConditionerStatusId).NotEmpty();
    }
}
