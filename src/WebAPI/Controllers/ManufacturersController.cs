using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.Manufacturers.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers;

[ApiController]
[Route("manufacturers")]
public class ManufacturersController(
    IManufacturerQueries queries,
    IManufacturersControllerService controllerService,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ManufacturerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(ManufacturerDto.FromDomainModel).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ManufacturerDto>> Get(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var entity = await controllerService.Get(id, cancellationToken);

        return entity.Match<ActionResult<ManufacturerDto>>(
            e => e,
            () => NotFound());
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
