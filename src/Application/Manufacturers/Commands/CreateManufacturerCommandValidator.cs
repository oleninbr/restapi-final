using FluentValidation;

namespace Application.Manufacturers.Commands;

public class CreateManufacturerCommandValidator : AbstractValidator<CreateManufacturerCommand>
{
    public CreateManufacturerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Country).NotEmpty().MaximumLength(255);
    }
}
