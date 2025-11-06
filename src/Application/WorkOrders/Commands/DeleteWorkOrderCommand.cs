using Application.Common.Interfaces.Repositories;
using Application.WorkOrders.Exceptions;
using Domain.WorkOrders;
using LanguageExt;
using MediatR;

namespace Application.WorkOrders.Commands;

public record DeleteWorkOrderCommand : IRequest<Either<WorkOrderException, WorkOrder>>
{
    public required Guid WorkOrderId { get; init; }
}

public class DeleteWorkOrderCommandHandler(
    IWorkOrderRepository workOrderRepository)
    : IRequestHandler<DeleteWorkOrderCommand, Either<WorkOrderException, WorkOrder>>
{
    public async Task<Either<WorkOrderException, WorkOrder>> Handle(
        DeleteWorkOrderCommand request,
        CancellationToken cancellationToken)
    {
        var id = new WorkOrderId(request.WorkOrderId);
        var entity = await workOrderRepository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            e => DeleteEntity(e, cancellationToken),
            () => new WorkOrderNotFoundException(id));
    }

    private async Task<Either<WorkOrderException, WorkOrder>> DeleteEntity(
        WorkOrder entity,
        CancellationToken cancellationToken)
    {
        try
        {
            return await workOrderRepository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderException(entity.Id, ex);
        }
    }
}
