using FluentValidation;

namespace Application.WorkOrderPriorities.Commands;

public class UpdateWorkOrderPriorityCommandValidator : AbstractValidator<UpdateWorkOrderPriorityCommand>
{
    public UpdateWorkOrderPriorityCommandValidator()
    {
        RuleFor(x => x.WorkOrderPriorityId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
