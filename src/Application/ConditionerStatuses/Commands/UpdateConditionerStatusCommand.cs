using Application.Common.Interfaces.Repositories;
using Application.ConditionerStatuses.Exceptions;
using Domain.ConditionerStatuses;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace Application.ConditionerStatuses.Commands;

public record UpdateConditionerStatusCommand : IRequest<Either<ConditionerStatusException, ConditionerStatus>>
{
    public required Guid ConditionerStatusId { get; init; }
    public required string Name { get; init; }
}

public class UpdateConditionerStatusCommandHandler(IConditionerStatusRepository repository)
    : IRequestHandler<UpdateConditionerStatusCommand, Either<ConditionerStatusException, ConditionerStatus>>
{
    public async Task<Either<ConditionerStatusException, ConditionerStatus>> Handle(
        UpdateConditionerStatusCommand request,
        CancellationToken cancellationToken)
    {
        var id = new ConditionerStatusId(request.ConditionerStatusId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return await entity.MatchAsync(
            s => UpdateEntity(s, request, cancellationToken),
            () => new ConditionerStatusNotFoundException(id));
    }

    private async Task<Either<ConditionerStatusException, ConditionerStatus>> UpdateEntity(
        ConditionerStatus entity,
        UpdateConditionerStatusCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateName(request.Name);
            return await repository.UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new UnhandledConditionerStatusException(entity.Id, ex);
        }
    }
}
