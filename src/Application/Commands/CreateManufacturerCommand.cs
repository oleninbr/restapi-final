using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public record CreateManufacturerCommand : IRequest<Manufacturer>
{
    public required string Name { get; init; }
    public required string Country { get; init; }
}

public class CreateManufacturerCommandHandler(
    IManufacturerRepository manufacturerRepository) : IRequestHandler<CreateManufacturerCommand, Manufacturer>
{
    public async Task<Manufacturer> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await manufacturerRepository.AddAsync(
            Manufacturer.New(Guid.NewGuid(), request.Name, request.Country),
            cancellationToken);

        return manufacturer;
    }
}
