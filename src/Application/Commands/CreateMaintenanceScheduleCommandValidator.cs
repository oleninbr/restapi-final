using FluentValidation;
using System;

namespace Application.Commands;

public class CreateMaintenanceScheduleCommandValidator : AbstractValidator<CreateMaintenanceScheduleCommand>
{
    public CreateMaintenanceScheduleCommandValidator()
    {
        RuleFor(x => x.ConditionerId).NotEmpty();
        RuleFor(x => x.TaskName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(5);
        RuleFor(x => x.FrequencyId).NotEmpty();
        RuleFor(x => x.NextDueDate).GreaterThan(DateTime.UtcNow).WithMessage("Next due date must be in the future");
        RuleFor(x => x.IsActive).NotNull();
    }
}
