using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.ConditionerTypes.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers;

[ApiController]
[Route("conditioner-types")]
public class ConditionerTypesController(
    ISender sender,
    IConditionerTypeQueries conditionerTypesQueries,
    IСonditionerTypesControlerService controllerService) 
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConditionerTypeDto>>> GetCountries(CancellationToken cancellationToken)
    {
        var conditionerTypes = await conditionerTypesQueries.GetAllAsync(cancellationToken);
        return conditionerTypes.Select(ConditionerTypeDto.FromDomainModel).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ConditionerTypeDto>> Get(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var entity = await controllerService.Get(id, cancellationToken);

        return entity.Match<ActionResult<ConditionerTypeDto>>(
            e => e,
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<ConditionerTypeDto>> Create([FromBody] CreateConditionerTypeDto request, CancellationToken cancellationToken)
    {
        var input = new CreateConditionerTypeCommand { Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerTypeDto>>(
            s => ConditionerTypeDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<ConditionerTypeDto>> Update([FromBody] UpdateConditionerTypeDto request, CancellationToken cancellationToken)
    {
          
        var input = new UpdateConditionerTypeCommand { ConditionerTypeId = request.Id, Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerTypeDto>>(
            s => ConditionerTypeDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ConditionerTypeDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
         
        var input = new DeleteConditionerTypeCommand { ConditionerTypeId = id };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerTypeDto>>(
            s => ConditionerTypeDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }
}
