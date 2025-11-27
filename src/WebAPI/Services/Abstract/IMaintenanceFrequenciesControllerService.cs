using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IMaintenanceFrequenciesControllerService
    {
        Task<Option<MaintenanceFrequencyDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
