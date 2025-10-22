using FluentValidation;

namespace Application.Commands;

public class CreateMaintenanceFrequencyCommandValidator : AbstractValidator<CreateMaintenanceFrequencyCommand>
{
    public CreateMaintenanceFrequencyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
    }
}
