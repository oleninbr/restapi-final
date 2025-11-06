using Application.Common.Interfaces.Repositories;
using Application.WorkOrderPriorities.Exceptions;
using Domain.WorkOrderPriorities;
using LanguageExt;
using MediatR;

namespace Application.WorkOrderPriorities.Commands;

public record CreateWorkOrderPriorityCommand : IRequest<Either<WorkOrderPriorityException, WorkOrderPriority>>
{
    public required string Name { get; init; }
}

public class CreateWorkOrderPriorityCommandHandler(IWorkOrderPriorityRepository repository)
    : IRequestHandler<CreateWorkOrderPriorityCommand, Either<WorkOrderPriorityException, WorkOrderPriority>>
{
    public async Task<Either<WorkOrderPriorityException, WorkOrderPriority>> Handle(
        CreateWorkOrderPriorityCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await repository.GetByNameAsync(request.Name, cancellationToken);

        return await existing.MatchAsync(
            p => new WorkOrderPriorityAlreadyExistException(p.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<WorkOrderPriorityException, WorkOrderPriority>> CreateEntity(
        CreateWorkOrderPriorityCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = WorkOrderPriority.New(WorkOrderPriorityId.New(), request.Name);
            return await repository.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderPriorityException(WorkOrderPriorityId.Empty(), ex);
        }
    }
}
