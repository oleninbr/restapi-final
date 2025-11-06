using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.WorkOrders.Exceptions;
using Domain.WorkOrders;
using Domain.Conditioners;
using Domain.WorkOrderPriorities;
using Domain.WorkOrderStatuses;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.WorkOrders.Commands;

public record CreateWorkOrderCommand : IRequest<Either<WorkOrderException, WorkOrder>>
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
    IWorkOrderRepository workOrderRepository,
    IConditionerRepository conditionerRepository,
    IWorkOrderPriorityRepository priorityRepository,
    IWorkOrderStatusRepository statusRepository,
    IEmailSendingService emailSendingService)
    : IRequestHandler<CreateWorkOrderCommand, Either<WorkOrderException, WorkOrder>>
{
    public async Task<Either<WorkOrderException, WorkOrder>> Handle(
        CreateWorkOrderCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await workOrderRepository.GetByNumberAsync(request.WorkOrderNumber, cancellationToken);

        return await existing.MatchAsync(
            _ => new WorkOrderAlreadyExistException(_!.Id),
            () => ValidateReferences(request, cancellationToken)
                .BindAsync(_ => CreateEntity(request, cancellationToken)
                    .BindAsync(newEntity => SendNotification(newEntity, cancellationToken))));
    }

    private async Task<Either<WorkOrderException, Unit>> ValidateReferences(
        CreateWorkOrderCommand request,
        CancellationToken cancellationToken)
    {
        var conditioner = await conditionerRepository.GetByIdAsync(new ConditionerId(request.ConditionerId), cancellationToken);
        var priority = await priorityRepository.GetByIdAsync(new WorkOrderPriorityId(request.PriorityId), cancellationToken);
        var status = await statusRepository.GetByIdAsync(new WorkOrderStatusId(request.StatusId), cancellationToken);

       
        if (conditioner.IsNone)
            return new ConditionerNotFoundForWorkOrderException(WorkOrderId.Empty());

        if (priority.IsNone)
            return new WorkOrderPriorityNotFoundException(WorkOrderId.Empty());

        if (status.IsNone)
            return new WorkOrderStatusNotFoundException(WorkOrderId.Empty());

        return Unit.Default;
    }

    private async Task<Either<WorkOrderException, WorkOrder>> CreateEntity(
        CreateWorkOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = WorkOrder.New(
                WorkOrderId.New(),
                request.WorkOrderNumber,
                new ConditionerId(request.ConditionerId),
                request.Title,
                request.Description,
                new WorkOrderPriorityId(request.PriorityId),
                new WorkOrderStatusId(request.StatusId),
                request.ScheduledDate);

            var result = await workOrderRepository.AddAsync(entity, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderException(WorkOrderId.Empty(), ex);
        }
    }

    private async Task<Either<WorkOrderException, WorkOrder>> SendNotification(
        WorkOrder workOrder,
        CancellationToken cancellationToken)
    {
        try
        {
            await emailSendingService.SendEmailAsync(
                "manager.manager@manager.com",
                $"New work order created: {workOrder.WorkOrderNumber} for conditioner {workOrder.ConditionerId}");

            return workOrder;
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderException(workOrder.Id, ex);
        }
    }
}
