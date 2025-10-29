using Application.Common.Interfaces.Repositories;
using Domain.WorkOrder;
using MediatR;

namespace Application.Commands;

public record CreateWorkOrderCommand : IRequest<WorkOrder>
{
    public required string WorkOrderNumber { get; init; }
    public required Guid ConditionerId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required Guid PriorityId { get; init; }
    public required Guid StatusId { get; init; }
    public required DateTime ScheduledDate { get; init; }
}

public class CreateWorkOrderCommandHandler(
    IWorkOrderRepository workOrderRepository) : IRequestHandler<CreateWorkOrderCommand, WorkOrder>
{
    public async Task<WorkOrder> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await workOrderRepository.AddAsync(
            WorkOrder.New(
                Guid.NewGuid(),
                request.WorkOrderNumber,
                request.ConditionerId,
                request.Title,
                request.Description,
                request.PriorityId,
                request.StatusId,
                request.ScheduledDate),
            cancellationToken);

        return workOrder;
    }
}
