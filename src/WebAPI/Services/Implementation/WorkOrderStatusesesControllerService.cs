using Application.Common.Interfaces.Queries;
using Domain.WorkOrderStatuses;
using LanguageExt;
using WebAPI.Dtos;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Implementation
{
    public class WorkOrderStatusesControllerService(IWorkOrderStatusQueries workOrderStatusQueries) : IWorkOrderStatusesControllerService
    {
        public async Task<Option<WorkOrderStatusDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await workOrderStatusQueries.GetByIdAsync(new WorkOrderStatusId(id), cancellationToken);

            return entity.Match(
                r => WorkOrderStatusDto.FromDomainModel(r),
                () => Option<WorkOrderStatusDto>.None);
        }
    }
}
 
