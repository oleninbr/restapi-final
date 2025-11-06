using FluentValidation;

namespace Application.MaintenanceSchedules.Commands;

public class CreateMaintenanceScheduleCommandValidator : AbstractValidator<CreateMaintenanceScheduleCommand>
{
    public CreateMaintenanceScheduleCommandValidator()
    {
        RuleFor(x => x.ConditionerId).NotEmpty();
        RuleFor(x => x.TaskName).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(3).MaximumLength(1000);
        RuleFor(x => x.FrequencyId).NotEmpty();
        RuleFor(x => x.NextDueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Next due date cannot be in the past.");
    }
}
