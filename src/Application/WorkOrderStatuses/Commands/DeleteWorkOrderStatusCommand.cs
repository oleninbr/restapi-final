using Application.Common.Interfaces.Repositories;
using Application.WorkOrderStatuses.Exceptions;
using Domain.WorkOrderStatuses;
using LanguageExt;
using MediatR;

namespace Application.WorkOrderStatuses.Commands;

public record DeleteWorkOrderStatusCommand : IRequest<Either<WorkOrderStatusException, WorkOrderStatus>>
{
    public required Guid WorkOrderStatusId { get; init; }
}

public class DeleteWorkOrderStatusCommandHandler(IWorkOrderStatusRepository repository)
    : IRequestHandler<DeleteWorkOrderStatusCommand, Either<WorkOrderStatusException, WorkOrderStatus>>
{
    public async Task<Either<WorkOrderStatusException, WorkOrderStatus>> Handle(
        DeleteWorkOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        var id = new WorkOrderStatusId(request.WorkOrderStatusId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            s => DeleteEntity(s, cancellationToken),
            () => new WorkOrderStatusNotFoundException(id));
    }

    private async Task<Either<WorkOrderStatusException, WorkOrderStatus>> DeleteEntity(
        WorkOrderStatus entity,
        CancellationToken cancellationToken)
    {
        try
        {
            return await repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderStatusException(entity.Id, ex);
        }
    }
}
