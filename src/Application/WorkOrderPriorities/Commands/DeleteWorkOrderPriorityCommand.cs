using Application.Common.Interfaces.Repositories;
using Application.WorkOrderPriorities.Exceptions;
using Domain.WorkOrderPriorities;
using LanguageExt;
using MediatR;

namespace Application.WorkOrderPriorities.Commands;

public record DeleteWorkOrderPriorityCommand : IRequest<Either<WorkOrderPriorityException, WorkOrderPriority>>
{
    public required Guid WorkOrderPriorityId { get; init; }
}

public class DeleteWorkOrderPriorityCommandHandler(IWorkOrderPriorityRepository repository)
    : IRequestHandler<DeleteWorkOrderPriorityCommand, Either<WorkOrderPriorityException, WorkOrderPriority>>
{
    public async Task<Either<WorkOrderPriorityException, WorkOrderPriority>> Handle(
        DeleteWorkOrderPriorityCommand request,
        CancellationToken cancellationToken)
    {
        var id = new WorkOrderPriorityId(request.WorkOrderPriorityId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            e => DeleteEntity(e, cancellationToken),
            () => new WorkOrderPriorityNotFoundException(id));
    }

    private async Task<Either<WorkOrderPriorityException, WorkOrderPriority>> DeleteEntity(
        WorkOrderPriority entity,
        CancellationToken cancellationToken)
    {
        try
        {
            return await repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderPriorityException(entity.Id, ex);
        }
    }
}
