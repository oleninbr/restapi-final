using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IConditionerControllerService
    {
        Task<Option<ConditionerDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
