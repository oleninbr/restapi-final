using Application.Common.Interfaces.Queries;
using LanguageExt;
using WebAPI.Dtos;
using WebAPI.Services.Abstract;
using Domain.MaintenanceSchedules;

namespace WebAPI.Services.Implementation
{
    public class MaintenanceSchedulesControllerService(IMaintenanceScheduleQueries manufacturerQueries) : IMaintenanceSchedulesControllerService
    {
        public async Task<Option<MaintenanceScheduleDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await manufacturerQueries.GetByIdAsync(new MaintenanceScheduleId(id), cancellationToken);
            return entity.Match(
                r => MaintenanceScheduleDto.FromDomainModel(r),
                () => Option<MaintenanceScheduleDto>.None);
        }
    }
}
