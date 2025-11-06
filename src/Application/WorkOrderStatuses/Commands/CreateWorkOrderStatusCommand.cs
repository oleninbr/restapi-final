using Application.Common.Interfaces.Repositories;
using Application.WorkOrderStatuses.Exceptions;
using Domain.WorkOrderStatuses;
using LanguageExt;
using MediatR;

namespace Application.WorkOrderStatuses.Commands;

public record CreateWorkOrderStatusCommand : IRequest<Either<WorkOrderStatusException, WorkOrderStatus>>
{
    public required string Name { get; init; }
}

public class CreateWorkOrderStatusCommandHandler(IWorkOrderStatusRepository repository)
    : IRequestHandler<CreateWorkOrderStatusCommand, Either<WorkOrderStatusException, WorkOrderStatus>>
{
    public async Task<Either<WorkOrderStatusException, WorkOrderStatus>> Handle(
        CreateWorkOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await repository.GetByNameAsync(request.Name, cancellationToken);

        return await existing.MatchAsync(
            s => new WorkOrderStatusAlreadyExistException(s.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<WorkOrderStatusException, WorkOrderStatus>> CreateEntity(
        CreateWorkOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = WorkOrderStatus.New(WorkOrderStatusId.New(), request.Name);
            return await repository.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderStatusException(WorkOrderStatusId.Empty(), ex);
        }
    }
}
