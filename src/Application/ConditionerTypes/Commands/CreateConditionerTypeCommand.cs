using Application.Common.Interfaces.Repositories;
using Application.ConditionerTypes.Exceptions;
using Domain.ConditionerTypes;
using LanguageExt;
using MediatR;

namespace Application.ConditionerTypes.Commands;

public record CreateConditionerTypeCommand : IRequest<Either<ConditionerTypeException, ConditionerType>>
{
    public required string Name { get; init; }
}

public class CreateConditionerTypeCommandHandler(IConditionerTypeRepository repository)
    : IRequestHandler<CreateConditionerTypeCommand, Either<ConditionerTypeException, ConditionerType>>
{
    public async Task<Either<ConditionerTypeException, ConditionerType>> Handle(
        CreateConditionerTypeCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await repository.GetByNameAsync(request.Name, cancellationToken);

        return await existing.MatchAsync(
            t => new ConditionerTypeAlreadyExistException(t.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<ConditionerTypeException, ConditionerType>> CreateEntity(
        CreateConditionerTypeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = ConditionerType.New(ConditionerTypeId.New(), request.Name);
            return await repository.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledConditionerTypeException(ConditionerTypeId.Empty(), ex);
        }
    }
}
