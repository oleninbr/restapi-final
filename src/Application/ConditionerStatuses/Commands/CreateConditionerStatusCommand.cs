using Application.Common.Interfaces.Repositories;
using Application.ConditionerStatuses.Exceptions;
using Domain.ConditionerStatuses;
using LanguageExt;
using MediatR;

namespace Application.ConditionerStatuses.Commands;

public record CreateConditionerStatusCommand : IRequest<Either<ConditionerStatusException, ConditionerStatus>>
{
    public required string Name { get; init; }
}

public class CreateConditionerStatusCommandHandler(IConditionerStatusRepository repository)
    : IRequestHandler<CreateConditionerStatusCommand, Either<ConditionerStatusException, ConditionerStatus>>
{
    public async Task<Either<ConditionerStatusException, ConditionerStatus>> Handle(
        CreateConditionerStatusCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await repository.GetByNameAsync(request.Name, cancellationToken);

        return await existing.MatchAsync(
            s => new ConditionerStatusAlreadyExistException(s.Id),
            () => CreateEntity(request, cancellationToken));
    }

    private async Task<Either<ConditionerStatusException, ConditionerStatus>> CreateEntity(
        CreateConditionerStatusCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = ConditionerStatus.New(ConditionerStatusId.New(), request.Name);
            return await repository.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledConditionerStatusException(ConditionerStatusId.Empty(), ex);
        }
    }
}
