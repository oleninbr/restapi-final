using Application.Common.Interfaces.Queries;
using LanguageExt;
using WebAPI.Dtos;
using WebAPI.Services.Abstract;
using Domain.Manufacturers;

namespace WebAPI.Services.Implementation
{
    public class ManufacturersControllerService(IManufacturerQueries manufacturerQueries) : IManufacturersControllerService
    {

        public async Task<Option<ManufacturerDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await manufacturerQueries.GetByIdAsync(new ManufacturerId(id), cancellationToken);
            return entity.Match(
                r => ManufacturerDto.FromDomainModel(r),
                () => Option<ManufacturerDto>.None);
        }
    }
}
