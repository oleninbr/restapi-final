using WebAPI.Dtos;
using WebAPI.Services.Abstract;
using Application.Common.Interfaces.Queries;
using Domain.ConditionerStatuses;
using LanguageExt;

namespace WebAPI.Services.Implementation
{
    public class СonditionerStatusesControllerService(IConditionerStatusQueries conditionerStatusQueries) : IСonditionerStatusesControllerService
    {
        public async Task<Option<ConditionerStatusDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await conditionerStatusQueries.GetByIdAsync(new ConditionerStatusId(id), cancellationToken);
            return entity.Match(
                r => ConditionerStatusDto.FromDomainModel(r),
                () => Option<ConditionerStatusDto>.None);
        }
    }
}
