using FluentValidation;

namespace Application.WorkOrders.Commands;

public class CreateWorkOrderCommandValidator : AbstractValidator<CreateWorkOrderCommand>
{
    public CreateWorkOrderCommandValidator()
    {
        RuleFor(x => x.WorkOrderNumber).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ConditionerId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(3).MaximumLength(1000);
        RuleFor(x => x.PriorityId).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.ScheduledDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Scheduled date cannot be in the past.");
    }
}
