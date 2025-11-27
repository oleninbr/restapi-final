using Application.Common.Interfaces.Queries;
using Domain.WorkOrders;  
using LanguageExt;
using WebAPI.Dtos;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Implementation
{
    public class WorkOrderControllerService(IWorkOrderQueries workOrderQueries) : IWorkOrderControllerService
    {
        public async Task<Option<WorkOrderDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await workOrderQueries.GetByIdAsync(new WorkOrderId(id), cancellationToken);

            return entity.Match(
                r => WorkOrderDto.FromDomainModel(r),
                () => Option<WorkOrderDto>.None);
        }
    }
}
