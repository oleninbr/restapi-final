using FluentValidation;

namespace Application.Conditioners.Commands;

public class CreateConditionerCommandValidator : AbstractValidator<CreateConditionerCommand>
{
    public CreateConditionerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(x => x.Model).NotEmpty().MinimumLength(2).MaximumLength(255);
        RuleFor(x => x.SerialNumber).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(255);
        RuleFor(x => x.InstallationDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Installation date cannot be in the future");

        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.ManufacturerId).NotEmpty();
    }
}
