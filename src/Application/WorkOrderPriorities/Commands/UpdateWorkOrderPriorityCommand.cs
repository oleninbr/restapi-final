using Application.Common.Interfaces.Repositories;
using Application.WorkOrderPriorities.Exceptions;
using Domain.WorkOrderPriorities;
using LanguageExt;
using MediatR;

namespace Application.WorkOrderPriorities.Commands;

public record UpdateWorkOrderPriorityCommand : IRequest<Either<WorkOrderPriorityException, WorkOrderPriority>>
{
    public required Guid WorkOrderPriorityId { get; init; }
    public required string Name { get; init; }
}

public class UpdateWorkOrderPriorityCommandHandler(IWorkOrderPriorityRepository repository)
    : IRequestHandler<UpdateWorkOrderPriorityCommand, Either<WorkOrderPriorityException, WorkOrderPriority>>
{
    public async Task<Either<WorkOrderPriorityException, WorkOrderPriority>> Handle(
        UpdateWorkOrderPriorityCommand request,
        CancellationToken cancellationToken)
    {
        var id = new WorkOrderPriorityId(request.WorkOrderPriorityId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            e => UpdateEntity(e, request, cancellationToken),
            () => new WorkOrderPriorityNotFoundException(id));
    }

    private async Task<Either<WorkOrderPriorityException, WorkOrderPriority>> UpdateEntity(
        WorkOrderPriority entity,
        UpdateWorkOrderPriorityCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateName(request.Name);
            return await repository.UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderPriorityException(entity.Id, ex);
        }
    }
}
