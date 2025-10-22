using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateWorkOrderPriorityDtoValidator : AbstractValidator<CreateWorkOrderPriorityDto>
{
    public CreateWorkOrderPriorityDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    }
}
