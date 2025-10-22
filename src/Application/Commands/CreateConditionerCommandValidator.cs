using FluentValidation;

namespace Application.Commands;

public class CreateConditionerCommandValidator : AbstractValidator<CreateConditionerCommand>
{
    public CreateConditionerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Model).NotEmpty().MinimumLength(2);
        RuleFor(x => x.SerialNumber).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.InstallationDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Installation date cannot be in the future");
        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.ManufacturerId).NotEmpty();
    }
}
