using WebAPI.Dtos;
using WebAPI.Services.Abstract;
using Application.Common.Interfaces.Queries;
using Domain.MaintenanceFrequencies;
using LanguageExt;


namespace WebAPI.Services.Implementation
{
    public class MaintenanceFrequenciesControllerService(IMaintenanceFrequencyQueries maintenanceFrequencyQueries) : IMaintenanceFrequenciesControllerService
    {
        public async Task<Option<MaintenanceFrequencyDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var entity = await maintenanceFrequencyQueries.GetByIdAsync(new MaintenanceFrequencyId(id), cancellationToken);
            return entity.Match(
                r => MaintenanceFrequencyDto.FromDomainModel(r),
                () => Option<MaintenanceFrequencyDto>.None);
        }
    }
}
