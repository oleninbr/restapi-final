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

[Route("conditioner-statuses")]
[ApiController]
public class ConditionerStatusesController(
    IConditionerStatusQueries conditionerStatusQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConditionerStatusDto>>> GetConditionerStatuses(CancellationToken cancellationToken)
    {
        var statuses = await conditionerStatusQueries.GetAllAsync(cancellationToken);
        return statuses.Select(ConditionerStatusDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ConditionerStatusDto>> CreateConditionerStatus(
        [FromBody] CreateConditionerStatusDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateConditionerStatusCommand
        {
            Name = request.Name
        };

        var newStatus = await sender.Send(command, cancellationToken);

        return ConditionerStatusDto.FromDomainModel(newStatus);
    }
}
