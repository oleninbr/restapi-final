using WebAPI.Dtos;
using LanguageExt;

namespace WebAPI.Services.Abstract
{
    public interface IManufacturersControllerService
    {
        Task<Option<ManufacturerDto>> Get(Guid id, CancellationToken cancellationToken);
    }
}
