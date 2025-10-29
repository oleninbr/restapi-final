using Application.Common.Interfaces.Repositories;
using Domain.ConditionerStatus;
using MediatR;

namespace Application.Commands;

public record CreateConditionerStatusCommand : IRequest<ConditionerStatus>
{
    public required string Name { get; init; }
}

public class CreateConditionerStatusCommandHandler(
    IConditionerStatusRepository conditionerStatusRepository) : IRequestHandler<CreateConditionerStatusCommand, ConditionerStatus>
{
    public async Task<ConditionerStatus> Handle(CreateConditionerStatusCommand request, CancellationToken cancellationToken)
    {
        var status = await conditionerStatusRepository.AddAsync(
            ConditionerStatus.New(Guid.NewGuid(), request.Name),
            cancellationToken);

        return status;
    }
}
