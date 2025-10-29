using Application.Common.Interfaces.Repositories;
using Domain.WorkOrderStatuses;
using MediatR;

namespace Application.Commands;

public record CreateWorkOrderStatusCommand : IRequest<WorkOrderStatus>
{
    public required string Name { get; init; }
}

public class CreateWorkOrderStatusCommandHandler(
    IWorkOrderStatusRepository repository) : IRequestHandler<CreateWorkOrderStatusCommand, WorkOrderStatus>
{
    public async Task<WorkOrderStatus> Handle(CreateWorkOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var status = await repository.AddAsync(
            WorkOrderStatus.New(Guid.NewGuid(), request.Name),
            cancellationToken);

        return status;
    }
}
