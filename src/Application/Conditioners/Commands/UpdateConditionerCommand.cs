using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Conditioners.Exceptions;
using Domain.Conditioners;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.Manufacturers;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.Conditioners.Commands;

public record UpdateConditionerCommand : IRequest<Either<ConditionerException, Conditioner>>
{
    public required Guid ConditionerId { get; init; }
    public required string Name { get; init; }
    public required string Model { get; init; }
    public required string SerialNumber { get; init; }
    public required string Location { get; init; }
    public required DateTime InstallationDate { get; init; }
    public required Guid StatusId { get; init; }
    public required Guid TypeId { get; init; }
    public required Guid ManufacturerId { get; init; }
}

public class UpdateConditionerCommandHandler(
    IApplicationDbContext applicationDbContext,
    IConditionerRepository conditionerRepository,
    IConditionerStatusRepository statusRepository,
    IConditionerTypeRepository typeRepository,
    IManufacturerRepository manufacturerRepository)
    : IRequestHandler<UpdateConditionerCommand, Either<ConditionerException, Conditioner>>
{
    public async Task<Either<ConditionerException, Conditioner>> Handle(
        UpdateConditionerCommand request,
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
        catch (Exception exception)
        {
            transaction.Rollback();
            return new UnhandledConditionerException(ConditionerId.Empty(), exception);
        }
    }

    private async Task<Either<ConditionerException, Conditioner>> HandleAsync(
        UpdateConditionerCommand request,
        CancellationToken cancellationToken)
    {
        var conditionerId = new ConditionerId(request.ConditionerId);
        var conditioner = await conditionerRepository.GetByIdAsync(conditionerId, cancellationToken);

        return await conditioner.MatchAsync(
            c => ValidateReferences(request, cancellationToken)
                .BindAsync(_ => CheckDuplicateSerial(c.Id, request.SerialNumber, cancellationToken)
                    .BindAsync(_ => UpdateEntity(request, c, cancellationToken))),
            () => new ConditionerNotFoundException(conditionerId));
    }

    private async Task<Either<ConditionerException, Unit>> ValidateReferences(
        UpdateConditionerCommand request,
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

    private async Task<Either<ConditionerException, Unit>> CheckDuplicateSerial(
        ConditionerId currentId,
        string serial,
        CancellationToken cancellationToken)
    {
        var conditioner = await conditionerRepository.GetBySerialNumberAsync(serial, cancellationToken);

        return conditioner.Match<Either<ConditionerException, Unit>>(
            c => c.Id.Equals(currentId) ? Unit.Default : new ConditionerAlreadyExistException(c.Id),
            () => Unit.Default);
    }

    private async Task<Either<ConditionerException, Conditioner>> UpdateEntity(
    UpdateConditionerCommand request,
    Conditioner conditioner,
    CancellationToken cancellationToken)
    {
        try
        {
            conditioner.UpdateDetails(
                request.Name,
                request.Model,
                request.SerialNumber,
                request.Location,
                request.InstallationDate,
                new ConditionerStatusId(request.StatusId),
                new ConditionerTypeId(request.TypeId),
                new ManufacturerId(request.ManufacturerId)); 

            return await conditionerRepository.UpdateAsync(conditioner, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UnhandledConditionerException(conditioner.Id, exception);
        }
    }

}
