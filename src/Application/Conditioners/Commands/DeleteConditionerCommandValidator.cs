using FluentValidation;

namespace Application.Conditioners.Commands;

public class DeleteConditionerCommandValidator : AbstractValidator<DeleteConditionerCommand>
{
    public DeleteConditionerCommandValidator()
    {
        RuleFor(x => x.ConditionerId).NotEmpty();
    }
}
