using WebAPI.Dtos;
using Application.Common.Interfaces.Queries;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("maintenance-schedules")]
[ApiController]
public class MaintenanceSchedulesController(
    IMaintenanceScheduleQueries maintenanceScheduleQueries,
    ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MaintenanceScheduleDto>>> GetMaintenanceSchedules(CancellationToken cancellationToken)
    {
        var schedules = await maintenanceScheduleQueries.GetAllAsync(cancellationToken);
        return schedules.Select(MaintenanceScheduleDto.FromDomainModel).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceScheduleDto>> CreateMaintenanceSchedule(
        [FromBody] CreateMaintenanceScheduleDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateMaintenanceScheduleCommand
        {
            TaskName = request.TaskName,
            Description = request.Description,
            NextDueDate = request.NextDueDate,
            IsActive = request.IsActive,
            ConditionerId = request.ConditionerId,
            FrequencyId = request.FrequencyId
        };

        var newSchedule = await sender.Send(command, cancellationToken);

        return MaintenanceScheduleDto.FromDomainModel(newSchedule);
    }
}
