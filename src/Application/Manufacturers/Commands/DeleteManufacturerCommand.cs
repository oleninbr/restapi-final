using Application.Common.Interfaces.Repositories;
using Application.Manufacturers.Exceptions;
using Domain.Manufacturers;
using LanguageExt;
using MediatR;

namespace Application.Manufacturers.Commands;

public record DeleteManufacturerCommand : IRequest<Either<ManufacturerException, Manufacturer>>
{
    public required Guid ManufacturerId { get; init; }
}

public class DeleteManufacturerCommandHandler(IManufacturerRepository repository)
    : IRequestHandler<DeleteManufacturerCommand, Either<ManufacturerException, Manufacturer>>
{
    public async Task<Either<ManufacturerException, Manufacturer>> Handle(
        DeleteManufacturerCommand request,
        CancellationToken cancellationToken)
    {
        var id = new ManufacturerId(request.ManufacturerId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            m => DeleteEntity(m, cancellationToken),
            () => new ManufacturerNotFoundException(id));
    }

    private async Task<Either<ManufacturerException, Manufacturer>> DeleteEntity(
        Manufacturer entity,
        CancellationToken cancellationToken)
    {
        try
        {
            return await repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledManufacturerException(entity.Id, ex);
        }
    }
}
