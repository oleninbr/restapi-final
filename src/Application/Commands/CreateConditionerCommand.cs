using Application.Common.Interfaces.Repositories;
using Domain.Conditioner;
using MediatR;

namespace Application.Commands;

public record CreateConditionerCommand : IRequest<Conditioner>
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
    IConditionerRepository conditionerRepository) : IRequestHandler<CreateConditionerCommand, Conditioner>
{
    public async Task<Conditioner> Handle(CreateConditionerCommand request, CancellationToken cancellationToken)
    {
        var conditioner = await conditionerRepository.AddAsync(
            Conditioner.New(
                Guid.NewGuid(),
                request.Name,
                request.Model,
                request.SerialNumber,
                request.Location,
                request.InstallationDate,
                request.StatusId,
                request.TypeId,
                request.ManufacturerId
            ),
            cancellationToken);

        return conditioner;
    }
}
