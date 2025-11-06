using Application.Common.Interfaces.Repositories;
using Application.MaintenanceSchedules.Exceptions; 
using Domain.MaintenanceSchedules;
using Domain.MaintenanceFrequencies;
using Domain.Conditioners;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.MaintenanceSchedules.Commands;

public record CreateMaintenanceScheduleCommand : IRequest<Either<MaintenanceScheduleException, MaintenanceSchedule>>
{
    public required Guid ConditionerId { get; init; }
    public required string TaskName { get; init; }
    public required string Description { get; init; }
    public required Guid FrequencyId { get; init; }
    public required DateTime NextDueDate { get; init; }
    public required bool IsActive { get; init; }
}

public class CreateMaintenanceScheduleCommandHandler(
    IMaintenanceScheduleRepository scheduleRepository,
    IConditionerRepository conditionerRepository,
    IMaintenanceFrequencyRepository frequencyRepository)
    : IRequestHandler<CreateMaintenanceScheduleCommand, Either<MaintenanceScheduleException, MaintenanceSchedule>>
{
    public async Task<Either<MaintenanceScheduleException, MaintenanceSchedule>> Handle(
        CreateMaintenanceScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var validation = await ValidateReferences(request, cancellationToken);
        if (validation.IsLeft)
            return validation.LeftToSeq().First();

        return await CreateEntity(request, cancellationToken);
    }

    private async Task<Either<MaintenanceScheduleException, Unit>> ValidateReferences(
     CreateMaintenanceScheduleCommand request,
     CancellationToken cancellationToken)
    {
        var conditioner = await conditionerRepository.GetByIdAsync(new ConditionerId(request.ConditionerId), cancellationToken);
        var frequency = await frequencyRepository.GetByIdAsync(new MaintenanceFrequencyId(request.FrequencyId), cancellationToken);

        if (conditioner.IsNone)
            return new MaintenanceScheduleConditionerNotFoundException(MaintenanceScheduleId.Empty());

        if (frequency.IsNone)
            return new MaintenanceScheduleFrequencyNotFoundException(MaintenanceScheduleId.Empty());

        return Unit.Default;
    }


    private async Task<Either<MaintenanceScheduleException, MaintenanceSchedule>> CreateEntity(
        CreateMaintenanceScheduleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = MaintenanceSchedule.New(
                MaintenanceScheduleId.New(),
                new ConditionerId(request.ConditionerId),
                request.TaskName,
                request.Description,
                new MaintenanceFrequencyId(request.FrequencyId),
                request.NextDueDate,
                request.IsActive
            );

            var result = await scheduleRepository.AddAsync(entity, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            return new UnhandledMaintenanceScheduleException(MaintenanceScheduleId.Empty(), ex);
        }
    }
}
