using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateMaintenanceFrequencyDtoValidator : AbstractValidator<CreateMaintenanceFrequencyDto>
{
    public CreateMaintenanceFrequencyDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    }
}
