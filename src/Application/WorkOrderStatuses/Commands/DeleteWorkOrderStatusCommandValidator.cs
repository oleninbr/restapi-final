using FluentValidation;

namespace Application.WorkOrderStatuses.Commands;

public class DeleteWorkOrderStatusCommandValidator : AbstractValidator<DeleteWorkOrderStatusCommand>
{
    public DeleteWorkOrderStatusCommandValidator()
    {
        RuleFor(x => x.WorkOrderStatusId).NotEmpty();
    }
}
