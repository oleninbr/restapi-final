using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.Manufacturers.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("manufacturers")]
public class ManufacturersController(
    IManufacturerQueries queries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ManufacturerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(ManufacturerDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<ManufacturerDto>> Create([FromBody] CreateManufacturerDto request, CancellationToken cancellationToken)
    {
        var input = new CreateManufacturerCommand { Name = request.Name, Country = request.Country };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ManufacturerDto>>(
            m => ManufacturerDto.FromDomainModel(m),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<ManufacturerDto>> Update([FromBody] UpdateManufacturerDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateManufacturerCommand { ManufacturerId = request.Id, Name = request.Name, Country = request.Country };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ManufacturerDto>>(
            m => ManufacturerDto.FromDomainModel(m),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ManufacturerDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteManufacturerCommand { ManufacturerId = id };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ManufacturerDto>>(
            m => ManufacturerDto.FromDomainModel(m),
            e => e.ToObjectResult());
    }
}
