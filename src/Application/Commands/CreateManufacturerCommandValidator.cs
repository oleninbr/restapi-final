using FluentValidation;

namespace Application.Commands;

public class CreateManufacturerCommandValidator : AbstractValidator<CreateManufacturerCommand>
{
    public CreateManufacturerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Country).NotEmpty().MinimumLength(2);
    }
}
