using FluentValidation;

namespace Application.WorkOrderStatuses.Commands;

public class CreateWorkOrderStatusCommandValidator : AbstractValidator<CreateWorkOrderStatusCommand>
{
    public CreateWorkOrderStatusCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
