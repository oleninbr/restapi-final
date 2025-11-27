using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.WorkOrderPriorities.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers;

[ApiController]
[Route("work-order-priorities")]
public class WorkOrderPrioritiesController(
    IWorkOrderPriorityQueries queries,
    IWorkOrderPrioritiesControllerService controllerService,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkOrderPriorityDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(WorkOrderPriorityDto.FromDomainModel).ToList();
    }


    
    //added Get by id method
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkOrderPriorityDto>> Get(
       [FromRoute] Guid id,
       CancellationToken cancellationToken)
    {
        var entity = await controllerService.Get(id, cancellationToken);

        return entity.Match<ActionResult<WorkOrderPriorityDto>>(
            e => e,
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<WorkOrderPriorityDto>> Create(
        [FromBody] CreateWorkOrderPriorityDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateWorkOrderPriorityCommand { Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderPriorityDto>>(
            p => WorkOrderPriorityDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<WorkOrderPriorityDto>> Update(
        [FromBody] UpdateWorkOrderPriorityDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateWorkOrderPriorityCommand
        {
            WorkOrderPriorityId = request.Id, 
            Name = request.Name
        };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderPriorityDto>>(
            p => WorkOrderPriorityDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<WorkOrderPriorityDto>> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var input = new DeleteWorkOrderPriorityCommand
        {
            WorkOrderPriorityId = id 
        };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderPriorityDto>>(
            p => WorkOrderPriorityDto.FromDomainModel(p),
            e => e.ToObjectResult());
    }
}
