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

[Route("work-orders")]
[ApiController]
public class WorkOrdersController(
    IWorkOrderQueries workOrderQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkOrderDto>>> GetWorkOrders(CancellationToken cancellationToken)
    {
        var orders = await workOrderQueries.GetAllAsync(cancellationToken);
        return orders.Select(WorkOrderDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<WorkOrderDto>> CreateWorkOrder(
        [FromBody] CreateWorkOrderDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateWorkOrderCommand
        {
            WorkOrderNumber = request.WorkOrderNumber,
            Title = request.Title,
            Description = request.Description,
            ScheduledDate = request.ScheduledDate,
            ConditionerId = request.ConditionerId,
            PriorityId = request.PriorityId,
            StatusId = request.StatusId
        };

        var newOrder = await sender.Send(command, cancellationToken);

        return WorkOrderDto.FromDomainModel(newOrder);
    }
}
