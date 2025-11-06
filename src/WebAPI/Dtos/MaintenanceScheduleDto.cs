using Domain.MaintenanceSchedules;

namespace WebAPI.Dtos;

public record MaintenanceScheduleDto(
    Guid Id,
    string TaskName,
    string Description,
    DateTime NextDueDate,
    bool IsActive,
    Guid ConditionerId,
    Guid FrequencyId,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static MaintenanceScheduleDto FromDomainModel(MaintenanceSchedule schedule)
        => new(
            schedule.Id.Value,
            schedule.TaskName,
            schedule.Description,
            schedule.NextDueDate,
            schedule.IsActive,
            schedule.ConditionerId.Value,
            schedule.FrequencyId.Value,
            schedule.CreatedAt,
            schedule.UpdatedAt);
}

public record CreateMaintenanceScheduleDto(
    string TaskName,
    string Description,
    DateTime NextDueDate,
    bool IsActive,
    Guid ConditionerId,
    Guid FrequencyId);

public record UpdateMaintenanceScheduleDto(
    Guid Id,
    string TaskName,
    string Description,
    DateTime NextDueDate,
    bool IsActive,
    Guid ConditionerId,
    Guid FrequencyId);