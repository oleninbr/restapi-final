using FluentValidation;

namespace Application.ConditionerTypes.Commands;

public class DeleteConditionerTypeCommandValidator : AbstractValidator<DeleteConditionerTypeCommand>
{
    public DeleteConditionerTypeCommandValidator()
    {
        RuleFor(x => x.ConditionerTypeId).NotEmpty();
    }
}
