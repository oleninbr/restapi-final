using WebAPI.Dtos;
using FluentValidation;

namespace WebAPI.Modules.Validators;

public class CreateConditionerDtoValidator : AbstractValidator<CreateConditionerDto>
{
    public CreateConditionerDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Model).NotEmpty().MinimumLength(1);
        RuleFor(x => x.SerialNumber).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.InstallationDate).NotEmpty();
        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.ManufacturerId).NotEmpty();
    }
}
