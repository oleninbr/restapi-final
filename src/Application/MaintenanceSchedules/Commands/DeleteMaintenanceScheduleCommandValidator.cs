using FluentValidation;

namespace Application.MaintenanceSchedules.Commands;

public class DeleteMaintenanceScheduleCommandValidator : AbstractValidator<DeleteMaintenanceScheduleCommand>
{
    public DeleteMaintenanceScheduleCommandValidator()
    {
        RuleFor(x => x.MaintenanceScheduleId).NotEmpty();
    }
}
