using WebAPI.Dtos;
using WebAPI.Modules.Errors;
using Application.MaintenanceSchedules.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("maintenance-schedules")]
public class MaintenanceSchedulesController(
    IMaintenanceScheduleQueries queries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MaintenanceScheduleDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await queries.GetAllAsync(cancellationToken);
        return entities.Select(MaintenanceScheduleDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceScheduleDto>> Create([FromBody] CreateMaintenanceScheduleDto request, CancellationToken cancellationToken)
    {
        var input = new CreateMaintenanceScheduleCommand
        {
            TaskName = request.TaskName,
            Description = request.Description,
            NextDueDate = request.NextDueDate,
            IsActive = request.IsActive,
            ConditionerId = request.ConditionerId,
            FrequencyId = request.FrequencyId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<MaintenanceScheduleDto>>(
            s => MaintenanceScheduleDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<MaintenanceScheduleDto>> Update([FromBody] UpdateMaintenanceScheduleDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateMaintenanceScheduleCommand
        {
            MaintenanceScheduleId = request.Id,
            TaskName = request.TaskName,
            Description = request.Description,
            NextDueDate = request.NextDueDate,
            IsActive = request.IsActive,
            ConditionerId = request.ConditionerId,
            FrequencyId = request.FrequencyId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<MaintenanceScheduleDto>>(
            s => MaintenanceScheduleDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<MaintenanceScheduleDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteMaintenanceScheduleCommand { MaintenanceScheduleId = id };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<MaintenanceScheduleDto>>(
            s => MaintenanceScheduleDto.FromDomainModel(s),
            e => e.ToObjectResult());
    }
}
