using Domain.Conditioners;
using Domain.WorkOrderStatuses;
using Domain.WorkOrderPriorities;

namespace Domain.WorkOrders;
public class WorkOrder
{
    public WorkOrderId Id { get; }
    public string WorkOrderNumber { get; private set; }

    public ConditionerId ConditionerId { get; private set; }
    public Conditioner? Conditioner { get; private set; }

    public string Title { get; private set; }
    public string Description { get; private set; }

    public WorkOrderPriorityId PriorityId { get; private set; }
    public WorkOrderPriority? Priority { get; private set; }

    public WorkOrderStatusId StatusId { get; private set; }
    public WorkOrderStatus? Status { get; private set; }

    public DateTime ScheduledDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? CompletionNotes { get; private set; }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private WorkOrder(
        WorkOrderId id,
        string workOrderNumber,
        ConditionerId conditionerId,
        string title,
        string description,
        WorkOrderPriorityId priorityId,
        WorkOrderStatusId statusId,
        DateTime scheduledDate,
        DateTime? completedAt,
        string? completionNotes,
        DateTime createdAt,
        DateTime? updatedAt)
    {
        Id = id;
        WorkOrderNumber = workOrderNumber;
        ConditionerId = conditionerId;
        Title = title;
        Description = description;
        PriorityId = priorityId;
        StatusId = statusId;
        ScheduledDate = scheduledDate;
        CompletedAt = completedAt;
        CompletionNotes = completionNotes;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static WorkOrder New(
        WorkOrderId id,
        string workOrderNumber,
        ConditionerId conditionerId,
        string title,
        string description,
        WorkOrderPriorityId priorityId,
        WorkOrderStatusId statusId,
        DateTime scheduledDate)
        => new(id, workOrderNumber, conditionerId, title, description, priorityId, statusId, scheduledDate, null, null, DateTime.UtcNow, null);

    public void UpdateDetails(string title, string description, WorkOrderPriorityId priorityId, WorkOrderStatusId statusId, DateTime scheduledDate, DateTime? completedAt, string? completionNotes)
    {
        Title = title;
        Description = description;
        PriorityId = priorityId;
        StatusId = statusId;
        ScheduledDate = scheduledDate;
        CompletedAt = completedAt;
        CompletionNotes = completionNotes;
        UpdatedAt = DateTime.UtcNow;
    }
}
