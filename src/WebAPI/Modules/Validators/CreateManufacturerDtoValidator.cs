using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateManufacturerDtoValidator : AbstractValidator<CreateManufacturerDto>
{
    public CreateManufacturerDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Country).NotEmpty().MinimumLength(2);
    }
}
