using Application.Common.Interfaces.Repositories;
using Domain.ConditionerType;
using MediatR;

namespace Application.Commands;

public record CreateConditionerTypeCommand : IRequest<ConditionerType>
{
    public required string Name { get; init; }
}

public class CreateConditionerTypeCommandHandler(
    IConditionerTypeRepository conditionerTypeRepository) : IRequestHandler<CreateConditionerTypeCommand, ConditionerType>
{
    public async Task<ConditionerType> Handle(CreateConditionerTypeCommand request, CancellationToken cancellationToken)
    {
        var type = await conditionerTypeRepository.AddAsync(
            ConditionerType.New(Guid.NewGuid(), request.Name),
            cancellationToken);

        return type;
    }
}
