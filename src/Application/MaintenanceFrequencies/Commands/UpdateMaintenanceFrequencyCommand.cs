using Application.Common.Interfaces.Repositories;
using Application.MaintenanceFrequencies.Exceptions;
using Domain.MaintenanceFrequencies;
using LanguageExt;
using MediatR;

namespace Application.MaintenanceFrequencies.Commands;

public record UpdateMaintenanceFrequencyCommand : IRequest<Either<MaintenanceFrequencyException, MaintenanceFrequency>>
{
    public required Guid MaintenanceFrequencyId { get; init; }
    public required string Name { get; init; }
}

public class UpdateMaintenanceFrequencyCommandHandler(IMaintenanceFrequencyRepository repository)
    : IRequestHandler<UpdateMaintenanceFrequencyCommand, Either<MaintenanceFrequencyException, MaintenanceFrequency>>
{
    public async Task<Either<MaintenanceFrequencyException, MaintenanceFrequency>> Handle(
        UpdateMaintenanceFrequencyCommand request,
        CancellationToken cancellationToken)
    {
        var id = new MaintenanceFrequencyId(request.MaintenanceFrequencyId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            f => UpdateEntity(f, request, cancellationToken),
            () => new MaintenanceFrequencyNotFoundException(id));
    }

    private async Task<Either<MaintenanceFrequencyException, MaintenanceFrequency>> UpdateEntity(
        MaintenanceFrequency entity,
        UpdateMaintenanceFrequencyCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateName(request.Name);
            return await repository.UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledMaintenanceFrequencyException(entity.Id, ex);
        }
    }
}
