using Application.Common.Interfaces.Repositories;
using Application.Conditioners.Exceptions;
using Domain.Conditioners;
using LanguageExt;
using MediatR;

namespace Application.Conditioners.Commands;

public record DeleteConditionerCommand : IRequest<Either<ConditionerException, Conditioner>>
{
    public required Guid ConditionerId { get; init; }
}

public class DeleteConditionerCommandHandler(
    IConditionerRepository conditionerRepository)
    : IRequestHandler<DeleteConditionerCommand, Either<ConditionerException, Conditioner>>
{
    public async Task<Either<ConditionerException, Conditioner>> Handle(
        DeleteConditionerCommand request,
        CancellationToken cancellationToken)
    {
        var conditionerId = new ConditionerId(request.ConditionerId);
        var conditioner = await conditionerRepository.GetByIdAsync(conditionerId, cancellationToken);

        return await conditioner.MatchAsync(
            c => DeleteEntity(c, cancellationToken),
            () => new ConditionerNotFoundException(conditionerId));
    }

    private async Task<Either<ConditionerException, Conditioner>> DeleteEntity(
        Conditioner conditioner,
        CancellationToken cancellationToken)
    {
        try
        {
            return await conditionerRepository.DeleteAsync(conditioner, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UnhandledConditionerException(conditioner.Id, exception);
        }
    }
}
