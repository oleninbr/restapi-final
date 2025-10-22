using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateWorkOrderStatusDtoValidator : AbstractValidator<CreateWorkOrderStatusDto>
{
    public CreateWorkOrderStatusDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    }
}
