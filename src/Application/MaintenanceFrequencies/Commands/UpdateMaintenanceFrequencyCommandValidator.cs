using FluentValidation;

namespace Application.MaintenanceFrequencies.Commands;

public class UpdateMaintenanceFrequencyCommandValidator : AbstractValidator<UpdateMaintenanceFrequencyCommand>
{
    public UpdateMaintenanceFrequencyCommandValidator()
    {
        RuleFor(x => x.MaintenanceFrequencyId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
