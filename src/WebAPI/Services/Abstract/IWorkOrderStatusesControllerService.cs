using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IWorkOrderStatusesControllerService
    {
        Task<Option<WorkOrderStatusDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
