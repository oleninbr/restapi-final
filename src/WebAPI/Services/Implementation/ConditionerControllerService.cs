using WebAPI.Services.Abstract;
using Application.Common.Interfaces.Queries;
using Domain.Conditioners;
using LanguageExt;
using WebAPI.Dtos;

namespace WebAPI.Services.Implementation
{
    public class ConditionerControllerService(IConditionerQueries conditionerQueries) :
        IConditionerControllerService
    {
        public async Task<Option<ConditionerDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await conditionerQueries.GetByIdAsync(new ConditionerId(id), cancellationToken);

            return entity.Match(
                r => ConditionerDto.FromDomainModel(r),
                () => Option<ConditionerDto>.None);
        }
    }
}
