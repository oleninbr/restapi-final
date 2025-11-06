using Application.Common.Interfaces.Repositories;
using Application.ConditionerStatuses.Exceptions;
using Domain.ConditionerStatuses;
using LanguageExt;
using MediatR;

namespace Application.ConditionerStatuses.Commands;

public record DeleteConditionerStatusCommand : IRequest<Either<ConditionerStatusException, ConditionerStatus>>
{
    public required Guid ConditionerStatusId { get; init; }
}

public class DeleteConditionerStatusCommandHandler(
   IConditionerStatusRepository repository)
   : IRequestHandler<DeleteConditionerStatusCommand, Either<ConditionerStatusException, ConditionerStatus>>
{
    public async Task<Either<ConditionerStatusException, ConditionerStatus>> Handle(
        DeleteConditionerStatusCommand request,
        CancellationToken cancellationToken)
    {
        var id = new ConditionerStatusId(request.ConditionerStatusId);
        var entity = await repository.GetByIdAsync(id, cancellationToken);

        return entity.Match<Either<ConditionerStatusException, ConditionerStatus>>(
            s => repository.DeleteAsync(s, cancellationToken).Result,
            () => new ConditionerStatusNotFoundException(id)
        );
    }
}
