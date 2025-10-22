using FluentValidation;
using System;

namespace Application.Commands;

public class CreateWorkOrderCommandValidator : AbstractValidator<CreateWorkOrderCommand>
{
    public CreateWorkOrderCommandValidator()
    {
        RuleFor(x => x.WorkOrderNumber).NotEmpty().MinimumLength(3);
        RuleFor(x => x.ConditionerId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(5);
        RuleFor(x => x.PriorityId).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.ScheduledDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Scheduled date must be today or in the future");
    }
}
