using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.ConditionerStatuses.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("conditioner-statuses")]
public class ConditionerStatusesController(
    IConditionerStatusQueries queries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConditionerStatusDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(ConditionerStatusDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ConditionerStatusDto>> Create([FromBody] CreateConditionerStatusDto request, CancellationToken cancellationToken)
    {
        var input = new CreateConditionerStatusCommand { Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerStatusDto>>(
            s => ConditionerStatusDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<ConditionerStatusDto>> Update([FromBody] UpdateConditionerStatusDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateConditionerStatusCommand { ConditionerStatusId = request.Id, Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerStatusDto>>(
            s => ConditionerStatusDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ConditionerStatusDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteConditionerStatusCommand { ConditionerStatusId = id };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerStatusDto>>(
            s => ConditionerStatusDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }
}
