using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.MaintenanceSchedules.Exceptions;
using Domain.MaintenanceSchedules;
using Domain.MaintenanceFrequencies;
using Domain.Conditioners;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;


namespace Application.MaintenanceSchedules.Commands;

public record UpdateMaintenanceScheduleCommand : IRequest<Either<MaintenanceScheduleException, MaintenanceSchedule>>
{
    public required Guid MaintenanceScheduleId { get; init; }
    public required Guid ConditionerId { get; init; }
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required Guid FrequencyId { get; init; }
    public required DateTime NextDueDate { get; init; }
    public required bool IsActive { get; init; }
}

public class UpdateMaintenanceScheduleCommandHandler(
    IApplicationDbContext applicationDbContext,
    IMaintenanceScheduleRepository scheduleRepository,
    IConditionerRepository conditionerRepository,
    IMaintenanceFrequencyRepository frequencyRepository)
    : IRequestHandler<UpdateMaintenanceScheduleCommand, Either<MaintenanceScheduleException, MaintenanceSchedule>>
{
    public async Task<Either<MaintenanceScheduleException, MaintenanceSchedule>> Handle(
        UpdateMaintenanceScheduleCommand request,
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
            return new UnhandledMaintenanceScheduleException(MaintenanceScheduleId.Empty(), ex);
        }
    }

    private async Task<Either<MaintenanceScheduleException, MaintenanceSchedule>> HandleAsync(
        UpdateMaintenanceScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var scheduleId = new MaintenanceScheduleId(request.MaintenanceScheduleId);
        var schedule = await scheduleRepository.GetByIdAsync(scheduleId, cancellationToken);

        return await schedule.MatchAsync(
            s => ValidateReferences(request, cancellationToken)
                .BindAsync(_ => UpdateEntity(request, s, cancellationToken)),
            () => new MaintenanceScheduleNotFoundException(scheduleId));
    }

    private async Task<Either<MaintenanceScheduleException, Unit>> ValidateReferences(
    UpdateMaintenanceScheduleCommand request,
    CancellationToken cancellationToken)
    {
        var scheduleId = new MaintenanceScheduleId(request.MaintenanceScheduleId);
        var conditioner = await conditionerRepository.GetByIdAsync(new ConditionerId(request.ConditionerId), cancellationToken);
        var frequency = await frequencyRepository.GetByIdAsync(new MaintenanceFrequencyId(request.FrequencyId), cancellationToken);

        if (conditioner.IsNone)
            return new MaintenanceScheduleConditionerNotFoundException(scheduleId);

        if (frequency.IsNone)
            return new MaintenanceScheduleFrequencyNotFoundException(scheduleId);

        return Unit.Default;
    }


    private async Task<Either<MaintenanceScheduleException, MaintenanceSchedule>> UpdateEntity(
        UpdateMaintenanceScheduleCommand request,
        MaintenanceSchedule schedule,
        CancellationToken cancellationToken)
    {
        try
        {
            schedule.UpdateDetails(
                request.TaskName,
                request.Description,
                new MaintenanceFrequencyId(request.FrequencyId),
                request.NextDueDate,
                request.IsActive);

            return await scheduleRepository.UpdateAsync(schedule, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledMaintenanceScheduleException(schedule.Id, ex);
        }
    }
}
