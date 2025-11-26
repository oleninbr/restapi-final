using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.ConditionerStatuses.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers;

[ApiController]
[Route("conditioner-statuses")]
public class ConditionerStatusesController(
    IConditionerStatusQueries conditionerStatusesQueries,
    IСonditionerStatusesControllerService controllerService ,
    ISender sender) : ControllerBase
{

    //get all method chenged to get conditioner statuses
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConditionerStatusDto>>> GetCountries(CancellationToken cancellationToken)
    {
        var conditionerStatuses = await conditionerStatusesQueries.GetAllAsync(cancellationToken);
        return conditionerStatuses.Select(ConditionerStatusDto.FromDomainModel).ToList();
    }

    //added Get by id method
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ConditionerStatusDto>> Get(
       [FromRoute] Guid id,
       CancellationToken cancellationToken)
    {
        var entity = await controllerService.Get(id, cancellationToken);

        return entity.Match<ActionResult<ConditionerStatusDto>>(
            e => e,
            () => NotFound());
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
