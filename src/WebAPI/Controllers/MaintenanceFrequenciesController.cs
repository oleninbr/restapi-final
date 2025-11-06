using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.MaintenanceFrequencies.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("maintenance-frequencies")]
public class MaintenanceFrequenciesController(
    IMaintenanceFrequencyQueries queries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MaintenanceFrequencyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(MaintenanceFrequencyDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceFrequencyDto>> Create([FromBody] CreateMaintenanceFrequencyDto request, CancellationToken cancellationToken)
    {
        var input = new CreateMaintenanceFrequencyCommand { Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<MaintenanceFrequencyDto>>(
            f => MaintenanceFrequencyDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<MaintenanceFrequencyDto>> Update([FromBody] UpdateMaintenanceFrequencyDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateMaintenanceFrequencyCommand { MaintenanceFrequencyId = request.Id, Name = request.Name };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<MaintenanceFrequencyDto>>(
            f => MaintenanceFrequencyDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<MaintenanceFrequencyDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteMaintenanceFrequencyCommand { MaintenanceFrequencyId = id };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<MaintenanceFrequencyDto>>(
            f => MaintenanceFrequencyDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }
}
