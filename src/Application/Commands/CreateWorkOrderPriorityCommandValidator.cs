using FluentValidation;

namespace Application.Commands;

public class CreateWorkOrderPriorityCommandValidator : AbstractValidator<CreateWorkOrderPriorityCommand>
{
    public CreateWorkOrderPriorityCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
    }
}
