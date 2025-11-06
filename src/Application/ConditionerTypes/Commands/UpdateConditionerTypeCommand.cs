using Application.Common.Interfaces.Repositories;
using Application.ConditionerTypes.Exceptions;
using Domain.ConditionerTypes;
using LanguageExt;
using MediatR;

namespace Application.ConditionerTypes.Commands;

public record UpdateConditionerTypeCommand : IRequest<Either<ConditionerTypeException, ConditionerType>>
{
    public required Guid ConditionerTypeId { get; init; }
    public required string Name { get; init; }
}

public class UpdateConditionerTypeCommandHandler(IConditionerTypeRepository repository)
    : IRequestHandler<UpdateConditionerTypeCommand, Either<ConditionerTypeException, ConditionerType>>
{
    public async Task<Either<ConditionerTypeException, ConditionerType>> Handle(
        UpdateConditionerTypeCommand request,
        CancellationToken cancellationToken)
    {
        var id = new ConditionerTypeId(request.ConditionerTypeId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            t => UpdateEntity(t, request, cancellationToken),
            () => new ConditionerTypeNotFoundException(id));
    }

    private async Task<Either<ConditionerTypeException, ConditionerType>> UpdateEntity(
        ConditionerType entity,
        UpdateConditionerTypeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateName(request.Name);
            return await repository.UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledConditionerTypeException(entity.Id, ex);
        }
    }
}
