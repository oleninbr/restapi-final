using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IСonditionerTypesControlerService
    {
        Task<Option<ConditionerTypeDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
