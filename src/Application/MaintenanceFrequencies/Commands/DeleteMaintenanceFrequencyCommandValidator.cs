using FluentValidation;

namespace Application.MaintenanceFrequencies.Commands;

public class DeleteMaintenanceFrequencyCommandValidator : AbstractValidator<DeleteMaintenanceFrequencyCommand>
{
    public DeleteMaintenanceFrequencyCommandValidator()
    {
        RuleFor(x => x.MaintenanceFrequencyId).NotEmpty();
    }
}
