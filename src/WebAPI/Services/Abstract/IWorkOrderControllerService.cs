using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IWorkOrderControllerService
    {
        Task<Option<WorkOrderDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
