using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Conditioners.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers;

[ApiController]
[Route("conditioners")]
public class ConditionersController(
    IConditionerQueries conditionerQueries,
    IConditionerControllerService controllerService,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConditionerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var conditioners = await conditionerQueries.GetAllAsync(cancellationToken);
        return conditioners.Select(ConditionerDto.FromDomainModel).ToList();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ConditionerDto>> Get(
      [FromRoute] Guid id,
      CancellationToken cancellationToken)
    {
        var entity = await controllerService.Get(id, cancellationToken);

        return entity.Match<ActionResult<ConditionerDto>>(
            e => e,
            () => NotFound());
    }


    [HttpPost]
    public async Task<ActionResult<ConditionerDto>> Create([FromBody] CreateConditionerDto request, CancellationToken cancellationToken)
    {
        var input = new CreateConditionerCommand
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

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerDto>>(
            c => ConditionerDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<ConditionerDto>> Update([FromBody] UpdateConditionerDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateConditionerCommand
        {
            ConditionerId = request.Id,
            Name = request.Name,
            Model = request.Model,
            SerialNumber = request.SerialNumber,
            Location = request.Location,
            InstallationDate = request.InstallationDate,
            StatusId = request.StatusId,
            TypeId = request.TypeId,
            ManufacturerId = request.ManufacturerId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ConditionerDto>>(
            c => ConditionerDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ConditionerDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteConditionerCommand { ConditionerId = id };
        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ConditionerDto>>(
            c => ConditionerDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }
}
