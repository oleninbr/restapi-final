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

[Route("manufacturers")]
[ApiController]
public class ManufacturersController(
    IManufacturerQueries manufacturerQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ManufacturerDto>>> GetManufacturers(CancellationToken cancellationToken)
    {
        var manufacturers = await manufacturerQueries.GetAllAsync(cancellationToken);
        return manufacturers.Select(ManufacturerDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ManufacturerDto>> CreateManufacturer(
        [FromBody] CreateManufacturerDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateManufacturerCommand
        {
            Name = request.Name,
            Country = request.Country
        };

        var newManufacturer = await sender.Send(command, cancellationToken);

        return ManufacturerDto.FromDomainModel(newManufacturer);
    }
}
