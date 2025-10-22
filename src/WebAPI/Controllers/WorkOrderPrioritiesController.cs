using WebAPI.Dtos;
using Application.Common.Interfaces.Queries;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers;

[Route("work-order-priorities")]
[ApiController]
public class WorkOrderPrioritiesController(
    IWorkOrderPriorityQueries workOrderPriorityQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkOrderPriorityDto>>> GetWorkOrderPriorities(CancellationToken cancellationToken)
    {
        var priorities = await workOrderPriorityQueries.GetAllAsync(cancellationToken);
        return priorities.Select(WorkOrderPriorityDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<WorkOrderPriorityDto>> CreateWorkOrderPriority(
        [FromBody] CreateWorkOrderPriorityDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateWorkOrderPriorityCommand
        {
            Name = request.Name
        };

        var newPriority = await sender.Send(command, cancellationToken);

        return WorkOrderPriorityDto.FromDomainModel(newPriority);
    }
}
