using Domain.Conditioners;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.Manufacturers;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.Conditioners
{
    public class ConditionersControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly ConditionerStatus _testStatus = Test.Data.ConditionerStatuses.ConditionerStatusesData.FirstTestStatus();
        private readonly ConditionerType _testType = Test.Data.ConditionerTypes.ConditionerTypesData.FirstTestType();
        private readonly Manufacturer _testManufacturer = Test.Data.Manufacturers.ManufacturersData.FirstTestManufacturer();
        private Conditioner _testConditioner;

        private const string BaseRoute = "conditioners";

        public ConditionersControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllConditioners()
        {
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var conditioners = await response.ToResponseModel<List<ConditionerDto>>();
            conditioners.Should().ContainSingle(c => c.Id == _testConditioner.Id.Value);
        }

        [Fact]
        public async Task ShouldGetConditionerById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_testConditioner.Id.Value}");
            response.EnsureSuccessStatusCode();

            var dto = await response.ToResponseModel<ConditionerDto>();
            dto.Id.Should().Be(_testConditioner.Id.Value);
        }

        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateConditioner()
        {
            var request = new CreateConditionerDto(
                Name: "New Conditioner",
                Model: "Model X",
                SerialNumber: "SN99999",
                Location: "Office Z",
                InstallationDate: DateTime.UtcNow,
                StatusId: _testStatus.Id.Value,
                TypeId: _testType.Id.Value,
                ManufacturerId: _testManufacturer.Id.Value
            );

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<ConditionerDto>();
            created.Name.Should().Be("New Conditioner");

            var dbEntity = Context.Conditioners
                .First(x => x.Id == new ConditionerId(created.Id));

            dbEntity.Name.Should().Be("New Conditioner");
        }

        [Fact]
        public async Task ShouldUpdateConditioner()
        {
            var request = new UpdateConditionerDto(
                Id: _testConditioner.Id.Value,
                Name: "Updated Conditioner",
                Model: _testConditioner.Model,
                SerialNumber: _testConditioner.SerialNumber,
                Location: _testConditioner.Location,
                InstallationDate: _testConditioner.InstallationDate,
                StatusId: _testStatus.Id.Value,
                TypeId: _testType.Id.Value,
                ManufacturerId: _testManufacturer.Id.Value
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.Conditioners
                .First(x => x.Id.Equals(_testConditioner.Id));

            updated.Name.Should().Be("Updated Conditioner");
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUpdatingMissingConditioner()
        {
            var request = new UpdateConditionerDto(
                Id: Guid.NewGuid(),
                Name: "DoesNotMatter",
                Model: "DoesNotMatter",
                SerialNumber: "DoesNotMatter",
                Location: "DoesNotMatter",
                InstallationDate: DateTime.UtcNow,
                StatusId: _testStatus.Id.Value,
                TypeId: _testType.Id.Value,
                ManufacturerId: _testManufacturer.Id.Value
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDeleteConditioner()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_testConditioner.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.Conditioners.Any(x => x.Id == _testConditioner.Id)
                .Should().BeFalse();
        }

        public async Task InitializeAsync()
        {
            await Context.ConditionerStatuses.AddAsync(_testStatus);
            await Context.ConditionerTypes.AddAsync(_testType);
            await Context.Manufacturers.AddAsync(_testManufacturer);

            _testConditioner = Conditioner.New(
                ConditionerId.New(),
                "LG ArtCool",
                "AC-120",
                "SN12345",
                "Office A1",
                DateTime.UtcNow,
                _testStatus.Id,
                _testType.Id,
                _testManufacturer.Id
            );

            await Context.Conditioners.AddAsync(_testConditioner);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.Conditioners.RemoveRange(Context.Conditioners);
            Context.ConditionerStatuses.RemoveRange(Context.ConditionerStatuses);
            Context.ConditionerTypes.RemoveRange(Context.ConditionerTypes);
            Context.Manufacturers.RemoveRange(Context.Manufacturers);
            await SaveChangesAsync();
        }
    }
}
