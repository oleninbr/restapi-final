using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.WorkOrders.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("work-orders")]
public class WorkOrdersController(
    IWorkOrderQueries queries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkOrderDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(WorkOrderDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<WorkOrderDto>> Create(
        [FromBody] CreateWorkOrderDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateWorkOrderCommand
        {
            WorkOrderNumber = request.WorkOrderNumber,
            Title = request.Title,
            Description = request.Description,
            ScheduledDate = request.ScheduledDate,
            ConditionerId = request.ConditionerId,
            PriorityId = request.PriorityId,
            StatusId = request.StatusId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderDto>>(
            o => WorkOrderDto.FromDomainModel(o),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<WorkOrderDto>> Update(
     [FromBody] UpdateWorkOrderDto request,
     CancellationToken cancellationToken)
    {
        var input = new UpdateWorkOrderCommand
        {
            WorkOrderId = request.Id,
            Title = request.Title,
            Description = request.Description,
            PriorityId = request.PriorityId,
            StatusId = request.StatusId,
            ScheduledDate = request.ScheduledDate
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderDto>>(
            o => WorkOrderDto.FromDomainModel(o),
            e => e.ToObjectResult());
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<WorkOrderDto>> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var input = new DeleteWorkOrderCommand
        {
            WorkOrderId = id 
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderDto>>(
            o => WorkOrderDto.FromDomainModel(o),
            e => e.ToObjectResult());
    }
}
