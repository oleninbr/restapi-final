using Application.Common.Interfaces.Repositories;
using Application.MaintenanceFrequencies.Exceptions;
using Domain.MaintenanceFrequencies;
using LanguageExt;
using MediatR;

namespace Application.MaintenanceFrequencies.Commands;

public record CreateMaintenanceFrequencyCommand : IRequest<Either<MaintenanceFrequencyException, MaintenanceFrequency>>
{
    public required string Name { get; init; }
}

public class CreateMaintenanceFrequencyCommandHandler(IMaintenanceFrequencyRepository repository)
    : IRequestHandler<CreateMaintenanceFrequencyCommand, Either<MaintenanceFrequencyException, MaintenanceFrequency>>
{
    public async Task<Either<MaintenanceFrequencyException, MaintenanceFrequency>> Handle(
        CreateMaintenanceFrequencyCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await repository.GetByNameAsync(request.Name, cancellationToken);

        return await existing.MatchAsync(
            f => new MaintenanceFrequencyAlreadyExistException(f.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<MaintenanceFrequencyException, MaintenanceFrequency>> CreateEntity(
        CreateMaintenanceFrequencyCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = MaintenanceFrequency.New(MaintenanceFrequencyId.New(), request.Name);
            return await repository.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledMaintenanceFrequencyException(MaintenanceFrequencyId.Empty(), ex);
        }
    }
}
