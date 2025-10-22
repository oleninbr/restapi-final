using WebAPI.Dtos;
using Application.Common.Interfaces.Queries;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("conditioners")]
[ApiController]
public class ConditionersController(
    IConditionerQueries conditionerQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConditionerDto>>> GetConditioners(CancellationToken cancellationToken)
    {
        var conditioners = await conditionerQueries.GetAllAsync(cancellationToken);
        return conditioners.Select(ConditionerDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ConditionerDto>> CreateConditioner(
        [FromBody] CreateConditionerDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateConditionerCommand
        {
            Name = request.Name,
            Model = request.Model,
            SerialNumber = request.SerialNumber,
            Location = request.Location,
            InstallationDate = request.InstallationDate,
            StatusId = request.StatusId,
            TypeId = request.TypeId,
            ManufacturerId = request.ManufacturerId
        };

        var newConditioner = await sender.Send(command, cancellationToken);

        return ConditionerDto.FromDomainModel(newConditioner);
    }
}
