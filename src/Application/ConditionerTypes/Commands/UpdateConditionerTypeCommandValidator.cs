using FluentValidation;

namespace Application.ConditionerTypes.Commands;

public class UpdateConditionerTypeCommandValidator : AbstractValidator<UpdateConditionerTypeCommand>
{
    public UpdateConditionerTypeCommandValidator()
    {
        RuleFor(x => x.ConditionerTypeId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
