using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IMaintenanceSchedulesControllerService
    {
        Task<Option<MaintenanceScheduleDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
