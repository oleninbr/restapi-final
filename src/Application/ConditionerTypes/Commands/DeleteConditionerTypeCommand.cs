using Application.Common.Interfaces.Repositories;
using Application.ConditionerTypes.Exceptions;
using Domain.ConditionerTypes;
using LanguageExt;
using MediatR;

namespace Application.ConditionerTypes.Commands;

public record DeleteConditionerTypeCommand : IRequest<Either<ConditionerTypeException, ConditionerType>>
{
    public required Guid ConditionerTypeId { get; init; }
}

public class DeleteConditionerTypeCommandHandler(IConditionerTypeRepository repository)
    : IRequestHandler<DeleteConditionerTypeCommand, Either<ConditionerTypeException, ConditionerType>>
{
    public async Task<Either<ConditionerTypeException, ConditionerType>> Handle(
        DeleteConditionerTypeCommand request,
        CancellationToken cancellationToken)
    {
        var id = new ConditionerTypeId(request.ConditionerTypeId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            t => DeleteEntity(t, cancellationToken),
            () => new ConditionerTypeNotFoundException(id));
    }

    private async Task<Either<ConditionerTypeException, ConditionerType>> DeleteEntity(
        ConditionerType entity,
        CancellationToken cancellationToken)
    {
        try
        {
            return await repository.DeleteAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledConditionerTypeException(entity.Id, ex);
        }
    }
}
