using Domain.MaintenanceSchedules;
using System;

namespace WebAPI.Dtos;

public record MaintenanceScheduleDto(Guid Id, string TaskName, string Description, DateTime NextDueDate, bool IsActive, Guid ConditionerId, Guid FrequencyId, DateTime CreatedAt, DateTime? UpdatedAt)
{
    public static MaintenanceScheduleDto FromDomainModel(MaintenanceSchedule schedule)
        => new(schedule.Id, schedule.TaskName, schedule.Description, schedule.NextDueDate, schedule.IsActive, schedule.ConditionerId, schedule.FrequencyId, schedule.CreatedAt, schedule.UpdatedAt);
}

public record CreateMaintenanceScheduleDto(string TaskName, string Description, DateTime NextDueDate, bool IsActive, Guid ConditionerId, Guid FrequencyId);
