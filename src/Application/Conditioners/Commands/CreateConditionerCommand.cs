using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Conditioners.Exceptions;
using Domain.Conditioners;
using Domain.Manufacturers;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Conditioners.Commands;

public record CreateConditionerCommand : IRequest<Either<ConditionerException, Conditioner>>
{
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required string SerialNumber { get; init; }
    public required string Location { get; init; }
    public required DateTime InstallationDate { get; init; }

    public required Guid StatusId { get; init; }
    public required Guid TypeId { get; init; }
    public required Guid ManufacturerId { get; init; }
}

public class CreateConditionerCommandHandler(
    IConditionerRepository conditionerRepository,
    IConditionerStatusRepository statusRepository,
    IConditionerTypeRepository typeRepository,
    IManufacturerRepository manufacturerRepository,
    IEmailSendingService emailSendingService)
    : IRequestHandler<CreateConditionerCommand, Either<ConditionerException, Conditioner>>
{
    public async Task<Either<ConditionerException, Conditioner>> Handle(
        CreateConditionerCommand request,
        CancellationToken cancellationToken)
    {
        var existingConditioner = await conditionerRepository.GetBySerialNumberAsync(request.SerialNumber, cancellationToken);

        return await existingConditioner.MatchAsync(
            c => new ConditionerAlreadyExistException(c.Id),
            () => ValidateReferences(request, cancellationToken)
                .BindAsync(_ => CreateEntity(request, cancellationToken)
                    .BindAsync(newConditioner => SendNotification(newConditioner, cancellationToken))));
    }

    private async Task<Either<ConditionerException, Unit>> ValidateReferences(
        CreateConditionerCommand request,
        CancellationToken cancellationToken)
    {
        var status = await statusRepository.GetByIdAsync(new ConditionerStatusId(request.StatusId), cancellationToken);
        var type = await typeRepository.GetByIdAsync(new ConditionerTypeId(request.TypeId), cancellationToken);
        var manufacturer = await manufacturerRepository.GetByIdAsync(new ManufacturerId(request.ManufacturerId), cancellationToken);

        if (status.IsNone) return new ConditionerStatusNotFoundException(new ConditionerId(Guid.Empty));
        if (type.IsNone) return new ConditionerTypeNotFoundException(new ConditionerId(Guid.Empty));
        if (manufacturer.IsNone) return new ManufacturerNotFoundException(new ConditionerId(Guid.Empty));

        return Unit.Default;
    }

    private async Task<Either<ConditionerException, Conditioner>> CreateEntity(
        CreateConditionerCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var conditioner = Conditioner.New(
                ConditionerId.New(),
                request.Name,
                request.Model,
                request.SerialNumber,
                request.Location,
                request.InstallationDate,
                new ConditionerStatusId(request.StatusId), //value objects
                new ConditionerTypeId(request.TypeId),       
                new ManufacturerId(request.ManufacturerId)); 

            var result = await conditionerRepository.AddAsync(conditioner, cancellationToken);
            return result;
        }
        catch (Exception exception)
        {
            return new UnhandledConditionerException(ConditionerId.Empty(), exception);
        }
    }


    private async Task<Either<ConditionerException, Conditioner>> SendNotification(
        Conditioner conditioner,
        CancellationToken cancellationToken)
    {
        try
        {
            await emailSendingService.SendEmailAsync(
                "manager.manager@manager.com",
                $"New conditioner added: {conditioner.Name}, model {conditioner.Model}, serial {conditioner.SerialNumber}");

            return conditioner;
        }
        catch (Exception exception)
        {
            return new UnhandledConditionerException(conditioner.Id, exception);
        }
    }
}
