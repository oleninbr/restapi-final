using Application.Common.Interfaces.Queries;
using Domain.WorkOrderPriorities;
using LanguageExt;
using WebAPI.Dtos;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Implementation
{
    public class WorkOrderPrioritiesControllerService
        (IWorkOrderPriorityQueries workOrderPriorityQueries)  : 
        IWorkOrderPrioritiesControllerService
    {

        public async Task<Option<WorkOrderPriorityDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await workOrderPriorityQueries.GetByIdAsync(new WorkOrderPriorityId(id), cancellationToken);

            return entity.Match(
                r => WorkOrderPriorityDto.FromDomainModel(r),
                () => Option<WorkOrderPriorityDto>.None);
        }
    }
}
