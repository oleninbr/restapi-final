using Domain.MaintenanceFrequencies;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.MaintenanceFrequencies
{
    public class MaintenanceFrequencyControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly MaintenanceFrequency _firstTestMaintenanceFrequency =
            Test.Data.MaintenanceFrequencies.MaintenanceFrequenciesData.FirstTestFrequency();

        private const string BaseRoute = "maintenance-frequencies";

        public MaintenanceFrequencyControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllMaintenanceFrequencies()
        {
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var frequencies = await response.ToResponseModel<List<MaintenanceFrequencyDto>>();
            frequencies.Should().ContainSingle(f => f.Name == _firstTestMaintenanceFrequency.Name);
        }

        [Fact]
        public async Task ShouldGetMaintenanceFrequencyById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_firstTestMaintenanceFrequency.Id.Value}");
            response.EnsureSuccessStatusCode();

            var dto = await response.ToResponseModel<MaintenanceFrequencyDto>();
            dto.Name.Should().Be(_firstTestMaintenanceFrequency.Name);
        }

        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateMaintenanceFrequency()
        {
            var request = new CreateMaintenanceFrequencyDto("NewFrequency");

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<MaintenanceFrequencyDto>();
            created.Name.Should().Be("NewFrequency");

            var dbEntity = Context.MaintenanceFrequencies
                .First(x => x.Id == new MaintenanceFrequencyId(created.Id));

            dbEntity.Name.Should().Be("NewFrequency");
        }

        [Fact]
        public async Task ShouldUpdateMaintenanceFrequency()
        {
            var request = new UpdateMaintenanceFrequencyDto(
                Id: _firstTestMaintenanceFrequency.Id.Value,
                Name: "UpdatedFrequency"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.MaintenanceFrequencies
                .First(x => x.Id.Equals(_firstTestMaintenanceFrequency.Id));

            updated.Name.Should().Be("UpdatedFrequency");
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUpdatingMissingFrequency()
        {
            var request = new UpdateMaintenanceFrequencyDto(
                Id: Guid.NewGuid(),
                Name: "DoesNotMatter"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDeleteMaintenanceFrequency()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestMaintenanceFrequency.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.MaintenanceFrequencies.Any(x => x.Id == _firstTestMaintenanceFrequency.Id)
                .Should().BeFalse();
        }

        public async Task InitializeAsync()
        {
            await Context.MaintenanceFrequencies.AddAsync(_firstTestMaintenanceFrequency);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.MaintenanceFrequencies.RemoveRange(Context.MaintenanceFrequencies);
            await SaveChangesAsync();
        }
    }
}
