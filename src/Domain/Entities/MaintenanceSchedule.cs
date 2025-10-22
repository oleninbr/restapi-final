namespace Domain.Entities;

public class MaintenanceSchedule
{
    public Guid Id { get; }
    public Guid ConditionerId { get; private set; }
    public string TaskName { get; private set; }
    public string Description { get; private set; }
    public Guid FrequencyId { get; private set; }
    public DateTime NextDueDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private MaintenanceSchedule(
        Guid id,
        Guid conditionerId,
        string taskName,
        string description,
        Guid frequencyId,
        DateTime nextDueDate,
        bool isActive,
        DateTime createdAt,
        DateTime? updatedAt)
    {
        Id = id;
        ConditionerId = conditionerId;
        TaskName = taskName;
        Description = description;
        FrequencyId = frequencyId;
        NextDueDate = nextDueDate;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static MaintenanceSchedule New(
        Guid id,
        Guid conditionerId,
        string taskName,
        string description,
        Guid frequencyId,
        DateTime nextDueDate,
        bool isActive)
    {
        return new MaintenanceSchedule(
            id,
            conditionerId,
            taskName,
            description,
            frequencyId,
            nextDueDate,
            isActive,
            DateTime.UtcNow,
            null);
    }

    public void UpdateDetails(
        string taskName,
        string description,
        Guid frequencyId,
        DateTime nextDueDate,
        bool isActive)
    {
        TaskName = taskName;
        Description = description;
        FrequencyId = frequencyId;
        NextDueDate = nextDueDate;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
