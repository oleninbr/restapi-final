using Domain.Conditioners;
using Domain.MaintenanceFrequencies;

namespace Domain.MaintenanceSchedules;

public class MaintenanceSchedule
{
    public MaintenanceScheduleId Id { get; }
    public ConditionerId ConditionerId { get; private set; }
    public Conditioner? Conditioner { get; private set; }

    public string TaskName { get; private set; }
    public string Description { get; private set; }

    public MaintenanceFrequencyId FrequencyId { get; private set; }
    public MaintenanceFrequency? Frequency { get; private set; }

    public DateTime NextDueDate { get; private set; }
    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private MaintenanceSchedule(
        MaintenanceScheduleId id,
        ConditionerId conditionerId,
        string taskName,
        string description,
        MaintenanceFrequencyId frequencyId,
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
        MaintenanceScheduleId id,
        ConditionerId conditionerId,
        string taskName,
        string description,
        MaintenanceFrequencyId frequencyId,
        DateTime nextDueDate,
        bool isActive)
        => new(id, conditionerId, taskName, description, frequencyId, nextDueDate, isActive, DateTime.UtcNow, null);

    public void UpdateDetails(string taskName, string description, MaintenanceFrequencyId frequencyId, DateTime nextDueDate, bool isActive)
    {
        TaskName = taskName;
        Description = description;
        FrequencyId = frequencyId;
        NextDueDate = nextDueDate;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
