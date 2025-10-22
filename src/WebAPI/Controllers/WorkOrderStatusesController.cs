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

[Route("work-order-statuses")]
[ApiController]
public class WorkOrderStatusesController(
    IWorkOrderStatusQueries workOrderStatusQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkOrderStatusDto>>> GetWorkOrderStatuses(CancellationToken cancellationToken)
    {
        var statuses = await workOrderStatusQueries.GetAllAsync(cancellationToken);
        return statuses.Select(WorkOrderStatusDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<WorkOrderStatusDto>> CreateWorkOrderStatus(
        [FromBody] CreateWorkOrderStatusDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateWorkOrderStatusCommand
        {
            Name = request.Name
        };

        var newStatus = await sender.Send(command, cancellationToken);

        return WorkOrderStatusDto.FromDomainModel(newStatus);
    }
}
