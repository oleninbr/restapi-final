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

[Route("conditioner-types")]
[ApiController]
public class ConditionerTypesController(
    IConditionerTypeQueries conditionerTypeQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConditionerTypeDto>>> GetConditionerTypes(CancellationToken cancellationToken)
    {
        var types = await conditionerTypeQueries.GetAllAsync(cancellationToken);
        return types.Select(ConditionerTypeDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ConditionerTypeDto>> CreateConditionerType(
        [FromBody] CreateConditionerTypeDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateConditionerTypeCommand
        {
            Name = request.Name
        };

        var newType = await sender.Send(command, cancellationToken);

        return ConditionerTypeDto.FromDomainModel(newType);
    }
}
