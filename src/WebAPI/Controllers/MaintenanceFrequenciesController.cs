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

[Route("maintenance-frequencies")]
[ApiController]
public class MaintenanceFrequenciesController(
    IMaintenanceFrequencyQueries maintenanceFrequencyQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MaintenanceFrequencyDto>>> GetMaintenanceFrequencies(CancellationToken cancellationToken)
    {
        var frequencies = await maintenanceFrequencyQueries.GetAllAsync(cancellationToken);
        return frequencies.Select(MaintenanceFrequencyDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceFrequencyDto>> CreateMaintenanceFrequency(
        [FromBody] CreateMaintenanceFrequencyDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateMaintenanceFrequencyCommand
        {
            Name = request.Name
        };

        var newFrequency = await sender.Send(command, cancellationToken);

        return MaintenanceFrequencyDto.FromDomainModel(newFrequency);
    }
}
