using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.WorkOrderStatuses.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

[ApiController]
[Route("work-order-statuses")]
public class WorkOrderStatusesController(
    IWorkOrderStatusQueries queries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkOrderStatusDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(WorkOrderStatusDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<WorkOrderStatusDto>> Create([FromBody] CreateWorkOrderStatusDto request, CancellationToken cancellationToken)
    {
        var input = new CreateWorkOrderStatusCommand { Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderStatusDto>>(
            s => WorkOrderStatusDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<WorkOrderStatusDto>> Update([FromBody] UpdateWorkOrderStatusDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateWorkOrderStatusCommand { WorkOrderStatusId = request.Id, Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderStatusDto>>(
            s => WorkOrderStatusDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<WorkOrderStatusDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteWorkOrderStatusCommand { WorkOrderStatusId = id };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<WorkOrderStatusDto>>(
            s => WorkOrderStatusDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }
}
