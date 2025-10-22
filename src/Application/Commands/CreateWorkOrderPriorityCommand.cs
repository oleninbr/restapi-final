using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public record CreateWorkOrderPriorityCommand : IRequest<WorkOrderPriority>
{
    public required string Name { get; init; }
}

public class CreateWorkOrderPriorityCommandHandler(
    IWorkOrderPriorityRepository repository) : IRequestHandler<CreateWorkOrderPriorityCommand, WorkOrderPriority>
{
    public async Task<WorkOrderPriority> Handle(CreateWorkOrderPriorityCommand request, CancellationToken cancellationToken)
    {
        var priority = await repository.AddAsync(
            WorkOrderPriority.New(Guid.NewGuid(), request.Name),
            cancellationToken);

        return priority;
    }
}
