using FluentValidation;

namespace Application.WorkOrders.Commands;

public class UpdateWorkOrderCommandValidator : AbstractValidator<UpdateWorkOrderCommand>
{
    public UpdateWorkOrderCommandValidator()
    {
        RuleFor(x => x.WorkOrderId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(3).MaximumLength(1000);
        RuleFor(x => x.PriorityId).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.ScheduledDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Scheduled date cannot be in the past.");
    }
}
