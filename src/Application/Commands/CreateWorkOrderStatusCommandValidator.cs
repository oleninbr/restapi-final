using FluentValidation;

namespace Application.Commands;

public class CreateWorkOrderStatusCommandValidator : AbstractValidator<CreateWorkOrderStatusCommand>
{
    public CreateWorkOrderStatusCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
    }
}
