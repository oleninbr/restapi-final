using FluentValidation;

namespace Application.WorkOrderPriorities.Commands;

public class CreateWorkOrderPriorityCommandValidator : AbstractValidator<CreateWorkOrderPriorityCommand>
{
    public CreateWorkOrderPriorityCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
