using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateWorkOrderDtoValidator : AbstractValidator<CreateWorkOrderDto>
{
    public CreateWorkOrderDtoValidator()
    {
        RuleFor(x => x.WorkOrderNumber).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(3);
        RuleFor(x => x.ScheduledDate).NotEmpty();
        RuleFor(x => x.ConditionerId).NotEmpty();
        RuleFor(x => x.PriorityId).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
    }
}
