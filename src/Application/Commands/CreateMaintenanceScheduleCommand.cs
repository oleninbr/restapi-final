using Application.Common.Interfaces.Repositories;
using Domain.MaintenanceSchedules;
using MediatR;

namespace Application.Commands;

public record CreateMaintenanceScheduleCommand : IRequest<MaintenanceSchedule>
{
    public required Guid ConditionerId { get; init; }
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required Guid FrequencyId { get; init; }
    public required DateTime NextDueDate { get; init; }
    public required bool IsActive { get; init; }
}

public class CreateMaintenanceScheduleCommandHandler(
    IMaintenanceScheduleRepository maintenanceScheduleRepository)
    : IRequestHandler<CreateMaintenanceScheduleCommand, MaintenanceSchedule>
{
    public async Task<MaintenanceSchedule> Handle(CreateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await maintenanceScheduleRepository.AddAsync(
            MaintenanceSchedule.New(
                Guid.NewGuid(),
                request.ConditionerId,
                request.TaskName,
                request.Description,
                request.FrequencyId,
                request.NextDueDate,
                request.IsActive),
            cancellationToken);

        return schedule;
    }
}
