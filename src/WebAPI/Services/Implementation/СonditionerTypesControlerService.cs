using WebAPI.Dtos;
using WebAPI.Services.Abstract;
using Application.Common.Interfaces.Queries;
using Domain.ConditionerTypes;
using LanguageExt;


namespace WebAPI.Services.Implementation
{
    public class СonditionerTypesControlerService(IConditionerTypeQueries conditionerTypeQueries) : IСonditionerTypesControlerService
    {
        public async Task<Option<ConditionerTypeDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await conditionerTypeQueries.GetByIdAsync(new ConditionerTypeId(id), cancellationToken);

            return entity.Match(
                r => ConditionerTypeDto.FromDomainModel(r),
                () => Option<ConditionerTypeDto>.None);
        }
    }
}
