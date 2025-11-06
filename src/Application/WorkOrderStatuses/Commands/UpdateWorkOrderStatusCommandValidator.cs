using FluentValidation;

namespace Application.WorkOrderStatuses.Commands;

public class UpdateWorkOrderStatusCommandValidator : AbstractValidator<UpdateWorkOrderStatusCommand>
{
    public UpdateWorkOrderStatusCommandValidator()
    {
        RuleFor(x => x.WorkOrderStatusId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
    }
}
