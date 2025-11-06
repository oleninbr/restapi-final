using Application.Common.Interfaces.Repositories;
using Application.WorkOrderStatuses.Exceptions;
using Domain.WorkOrderStatuses;
using LanguageExt;
using MediatR;

namespace Application.WorkOrderStatuses.Commands;

public record UpdateWorkOrderStatusCommand : IRequest<Either<WorkOrderStatusException, WorkOrderStatus>>
{
    public required Guid WorkOrderStatusId { get; init; }
    public required string Name { get; init; }
}

public class UpdateWorkOrderStatusCommandHandler(IWorkOrderStatusRepository repository)
    : IRequestHandler<UpdateWorkOrderStatusCommand, Either<WorkOrderStatusException, WorkOrderStatus>>
{
    public async Task<Either<WorkOrderStatusException, WorkOrderStatus>> Handle(
        UpdateWorkOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        var id = new WorkOrderStatusId(request.WorkOrderStatusId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            s => UpdateEntity(s, request, cancellationToken),
            () => new WorkOrderStatusNotFoundException(id));
    }

    private async Task<Either<WorkOrderStatusException, WorkOrderStatus>> UpdateEntity(
        WorkOrderStatus entity,
        UpdateWorkOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateName(request.Name);
            return await repository.UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderStatusException(entity.Id, ex);
        }
    }
}
