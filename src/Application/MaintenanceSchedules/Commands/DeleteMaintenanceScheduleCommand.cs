using Application.Common.Interfaces.Repositories;
using Application.MaintenanceSchedules.Exceptions;
using Domain.MaintenanceSchedules;
using LanguageExt;
using MediatR;

namespace Application.MaintenanceSchedules.Commands;

public record DeleteMaintenanceScheduleCommand : IRequest<Either<MaintenanceScheduleException, MaintenanceSchedule>>
{
    public required Guid MaintenanceScheduleId { get; init; }
}

public class DeleteMaintenanceScheduleCommandHandler(
    IMaintenanceScheduleRepository scheduleRepository)
    : IRequestHandler<DeleteMaintenanceScheduleCommand, Either<MaintenanceScheduleException, MaintenanceSchedule>>
{
    public async Task<Either<MaintenanceScheduleException, MaintenanceSchedule>> Handle(
        DeleteMaintenanceScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var id = new MaintenanceScheduleId(request.MaintenanceScheduleId);
        var entity = await scheduleRepository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            s => DeleteEntity(s, cancellationToken),
            () => new MaintenanceScheduleNotFoundException(id));
    }

    private async Task<Either<MaintenanceScheduleException, MaintenanceSchedule>> DeleteEntity(
        MaintenanceSchedule schedule,
        CancellationToken cancellationToken)
    {
        try
        {
            return await scheduleRepository.DeleteAsync(schedule, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledMaintenanceScheduleException(schedule.Id, ex);
        }
    }
}
