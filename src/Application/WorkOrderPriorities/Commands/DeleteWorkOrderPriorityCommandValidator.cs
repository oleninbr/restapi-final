using FluentValidation;

namespace Application.WorkOrderPriorities.Commands;

public class DeleteWorkOrderPriorityCommandValidator : AbstractValidator<DeleteWorkOrderPriorityCommand>
{
    public DeleteWorkOrderPriorityCommandValidator()
    {
        RuleFor(x => x.WorkOrderPriorityId).NotEmpty();
    }
}
