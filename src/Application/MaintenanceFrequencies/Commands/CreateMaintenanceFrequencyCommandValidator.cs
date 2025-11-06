using FluentValidation;

namespace Application.MaintenanceFrequencies.Commands;

public class CreateMaintenanceFrequencyCommandValidator : AbstractValidator<CreateMaintenanceFrequencyCommand>
{
    public CreateMaintenanceFrequencyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
