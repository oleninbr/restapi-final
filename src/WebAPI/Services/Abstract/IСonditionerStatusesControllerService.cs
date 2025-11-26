using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IСonditionerStatusesControllerService
    {
        Task<Option<ConditionerStatusDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
