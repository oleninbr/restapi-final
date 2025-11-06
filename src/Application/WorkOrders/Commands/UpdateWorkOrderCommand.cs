using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.WorkOrders.Exceptions;
using Domain.WorkOrders;
using Domain.WorkOrderPriorities;
using Domain.WorkOrderStatuses;
using Domain.Conditioners;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.WorkOrders.Commands;

public record UpdateWorkOrderCommand : IRequest<Either<WorkOrderException, WorkOrder>>
{
    public required Guid WorkOrderId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required Guid PriorityId { get; init; }
    public required Guid StatusId { get; init; }
    public required DateTime ScheduledDate { get; init; }
    public DateTime? CompletedAt { get; init; }
    public string? CompletionNotes { get; init; }
}

public class UpdateWorkOrderCommandHandler(
    IApplicationDbContext applicationDbContext,
    IWorkOrderRepository workOrderRepository,
    IWorkOrderPriorityRepository priorityRepository,
    IWorkOrderStatusRepository statusRepository)
    : IRequestHandler<UpdateWorkOrderCommand, Either<WorkOrderException, WorkOrder>>
{
    public async Task<Either<WorkOrderException, WorkOrder>> Handle(
        UpdateWorkOrderCommand request,
        CancellationToken cancellationToken)
    {
        using var transaction = await applicationDbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await HandleAsync(request, cancellationToken);

            if (result.IsLeft)
                transaction.Rollback();
            else
                transaction.Commit();

            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return new UnhandledWorkOrderException(WorkOrderId.Empty(), ex);
        }
    }

    private async Task<Either<WorkOrderException, WorkOrder>> HandleAsync(
        UpdateWorkOrderCommand request,
        CancellationToken cancellationToken)
    {
        var id = new WorkOrderId(request.WorkOrderId);
        var entity = await workOrderRepository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            e => ValidateReferences(request, cancellationToken)
                .BindAsync(_ => UpdateEntity(request, e, cancellationToken)),
            () => new WorkOrderNotFoundException(id));
    }

    private async Task<Either<WorkOrderException, Unit>> ValidateReferences(
        UpdateWorkOrderCommand request,
        CancellationToken cancellationToken)
    {
        var priority = await priorityRepository.GetByIdAsync(new WorkOrderPriorityId(request.PriorityId), cancellationToken);
        var status = await statusRepository.GetByIdAsync(new WorkOrderStatusId(request.StatusId), cancellationToken);

        if (priority.IsNone)
            return new WorkOrderPriorityNotFoundException(new WorkOrderId(Guid.Empty));

        if (status.IsNone)
            return new WorkOrderStatusNotFoundException(new WorkOrderId(Guid.Empty));

        return Unit.Default;
    }

    private async Task<Either<WorkOrderException, WorkOrder>> UpdateEntity(
        UpdateWorkOrderCommand request,
        WorkOrder entity,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateDetails(
                request.Title,
                request.Description,
                new WorkOrderPriorityId(request.PriorityId),
                new WorkOrderStatusId(request.StatusId),
                request.ScheduledDate,
                request.CompletedAt,
                request.CompletionNotes ?? string.Empty);

            return await workOrderRepository.UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledWorkOrderException(entity.Id, ex);
        }
    }
}
