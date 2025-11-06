using Application.Common.Interfaces.Repositories;
using Application.Manufacturers.Exceptions;
using Domain.Manufacturers;
using LanguageExt;
using MediatR;

namespace Application.Manufacturers.Commands;

public record CreateManufacturerCommand : IRequest<Either<ManufacturerException, Manufacturer>>
{
    public required string Name { get; init; }
    public required string Country { get; init; }
}

public class CreateManufacturerCommandHandler(IManufacturerRepository repository)
    : IRequestHandler<CreateManufacturerCommand, Either<ManufacturerException, Manufacturer>>
{
    public async Task<Either<ManufacturerException, Manufacturer>> Handle(
        CreateManufacturerCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await repository.GetByNameAsync(request.Name, cancellationToken);

        return await existing.MatchAsync(
            m => new ManufacturerAlreadyExistException(m.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<ManufacturerException, Manufacturer>> CreateEntity(
        CreateManufacturerCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Manufacturer.New(ManufacturerId.New(), request.Name, request.Country);
            return await repository.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledManufacturerException(ManufacturerId.Empty(), ex);
        }
    }
}
