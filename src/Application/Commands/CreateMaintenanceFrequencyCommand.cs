using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public record CreateMaintenanceFrequencyCommand : IRequest<MaintenanceFrequency>
{
    public required string Name { get; init; }
}

public class CreateMaintenanceFrequencyCommandHandler(
    IMaintenanceFrequencyRepository maintenanceFrequencyRepository)
    : IRequestHandler<CreateMaintenanceFrequencyCommand, MaintenanceFrequency>
{
    public async Task<MaintenanceFrequency> Handle(CreateMaintenanceFrequencyCommand request, CancellationToken cancellationToken)
    {
        var frequency = await maintenanceFrequencyRepository.AddAsync(
            MaintenanceFrequency.New(Guid.NewGuid(), request.Name),
            cancellationToken);

        return frequency;
    }
}
