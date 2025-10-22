using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateMaintenanceScheduleDtoValidator : AbstractValidator<CreateMaintenanceScheduleDto>
{
    public CreateMaintenanceScheduleDtoValidator()
    {
        RuleFor(x => x.TaskName).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(3);
        RuleFor(x => x.NextDueDate).NotEmpty();
        RuleFor(x => x.ConditionerId).NotEmpty();
        RuleFor(x => x.FrequencyId).NotEmpty();
    }
}
