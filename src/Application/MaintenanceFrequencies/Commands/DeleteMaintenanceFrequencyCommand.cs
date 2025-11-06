using Application.Common.Interfaces.Repositories;
using Application.MaintenanceFrequencies.Exceptions;
using Domain.MaintenanceFrequencies;
using LanguageExt;
using MediatR;

namespace Application.MaintenanceFrequencies.Commands;

public record DeleteMaintenanceFrequencyCommand : IRequest<Either<MaintenanceFrequencyException, MaintenanceFrequency>>
{
    public required Guid MaintenanceFrequencyId { get; init; }
}

public class DeleteMaintenanceFrequencyCommandHandler(IMaintenanceFrequencyRepository repository)
    : IRequestHandler<DeleteMaintenanceFrequencyCommand, Either<MaintenanceFrequencyException, MaintenanceFrequency>>
{
    public async Task<Either<MaintenanceFrequencyException, MaintenanceFrequency>> Handle(
        DeleteMaintenanceFrequencyCommand request,
        CancellationToken cancellationToken)
    {
        var id = new MaintenanceFrequencyId(request.MaintenanceFrequencyId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            f => DeleteEntity(f, cancellationToken),
            () => new MaintenanceFrequencyNotFoundException(id));
    }

    private async Task<Either<MaintenanceFrequencyException, MaintenanceFrequency>> DeleteEntity(
        MaintenanceFrequency entity,
        CancellationToken cancellationToken)
    {
        try
        {
            return await repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledMaintenanceFrequencyException(entity.Id, ex);
        }
    }
}
