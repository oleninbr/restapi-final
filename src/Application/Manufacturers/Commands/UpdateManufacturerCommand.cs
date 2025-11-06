using Application.Common.Interfaces.Repositories;
using Application.Manufacturers.Exceptions;
using Domain.Manufacturers;
using LanguageExt;
using MediatR;

namespace Application.Manufacturers.Commands;

public record UpdateManufacturerCommand : IRequest<Either<ManufacturerException, Manufacturer>>
{
    public required Guid ManufacturerId { get; init; }
    public required string Name { get; init; }
    public required string Country { get; init; }
}

public class UpdateManufacturerCommandHandler(IManufacturerRepository repository)
    : IRequestHandler<UpdateManufacturerCommand, Either<ManufacturerException, Manufacturer>>
{
    public async Task<Either<ManufacturerException, Manufacturer>> Handle(
        UpdateManufacturerCommand request,
        CancellationToken cancellationToken)
    {
        var id = new ManufacturerId(request.ManufacturerId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            m => UpdateEntity(m, request, cancellationToken),
            () => new ManufacturerNotFoundException(id));
    }

    private async Task<Either<ManufacturerException, Manufacturer>> UpdateEntity(
        Manufacturer entity,
        UpdateManufacturerCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateDetails(request.Name, request.Country);
            return await repository.UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledManufacturerException(entity.Id, ex);
        }
    }
}

