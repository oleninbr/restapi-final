namespace Domain.WorkOrder;

public class WorkOrder
{
    public Guid Id { get; }
    public string WorkOrderNumber { get; private set; }
    public Guid ConditionerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid PriorityId { get; private set; }
    public Guid StatusId { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string CompletionNotes { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private WorkOrder(
        Guid id,
        string workOrderNumber,
        Guid conditionerId,
        string title,
        string description,
        Guid priorityId,
        Guid statusId,
        DateTime scheduledDate,
        DateTime? completedAt,
        string completionNotes,
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
        Guid id,
        string workOrderNumber,
        Guid conditionerId,
        string title,
        string description,
        Guid priorityId,
        Guid statusId,
        DateTime scheduledDate)
    {
        return new WorkOrder(
            id,
            workOrderNumber,
            conditionerId,
            title,
            description,
            priorityId,
            statusId,
            scheduledDate,
            null,
            null,
            DateTime.UtcNow,
            null);
    }

    public void UpdateDetails(
        string title,
        string description,
        Guid priorityId,
        Guid statusId,
        DateTime scheduledDate,
        DateTime? completedAt,
        string completionNotes)
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
