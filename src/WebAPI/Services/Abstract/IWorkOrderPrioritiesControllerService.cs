using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IWorkOrderPrioritiesControllerService
    {
        Task<Option<WorkOrderPriorityDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
