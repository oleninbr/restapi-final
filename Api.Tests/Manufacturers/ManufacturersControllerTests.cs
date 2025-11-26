using Domain.ConditionerTypes;
using Domain.Manufacturers;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.Manufacturers
{
    public class ManufacturersControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly Manufacturer _firstTestManufacturer =
            Test.Data.Manufacturers.ManufacturersData.FirstTestManufacturer();
        private const string BaseRoute = "manufacturers";

        public ManufacturersControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task ShouldGetAllManufacturers()
        {
            // act
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();
            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var manufacturers = await response.ToResponseModel<List<ManufacturerDto>>();
            manufacturers.Should().ContainSingle(m => m.Name == _firstTestManufacturer.Name);
        }

        [Fact]
        public async Task ShouldGetManufacturerById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_firstTestManufacturer.Id.Value}");
            response.EnsureSuccessStatusCode();
            var dto = await response.ToResponseModel<ManufacturerDto>();
            dto.Name.Should().Be(_firstTestManufacturer.Name);
        }


        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateManufacturer()
        {
            var request = new CreateManufacturerDto("NewTestType", "NewTestCountry");

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<ManufacturerDto>();
            created.Name.Should().Be("NewTestType");

            var dbEntity = Context.Manufacturers
        .First(x => x.Id == new ManufacturerId(created.Id));

            dbEntity.Name.Should().Be("NewTestType");
        }

        [Fact]
        public async Task ShouldUpdateManufacturer()
        {
            var request = new UpdateManufacturerDto(
                Id: _firstTestManufacturer.Id.Value,
                Name: "UpdatedName",
                Country: "UpdatedCountry"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.Manufacturers
                .First(x => x.Id.Equals(_firstTestManufacturer.Id));

            updated.Name.Should().Be("UpdatedName");
        }

        [Fact]
        public async Task ShouldReturnBadRequestForDuplicateManufacturerName()
        {
            var request = new CreateManufacturerDto(_firstTestManufacturer.Name, "SomeCountry");
            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldReturnBadRequestForShortManufacturerName()
        {
            var request = new CreateManufacturerDto("A", "SomeCountry");
            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldDeleteManufacturer()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestManufacturer.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.Manufacturers.Any(x => x.Id == _firstTestManufacturer.Id)
                .Should().BeFalse();
        }


        public async Task InitializeAsync()
        {
            await Context.Manufacturers.AddAsync(_firstTestManufacturer);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.Manufacturers.RemoveRange(Context.Manufacturers);
            await SaveChangesAsync();
        }
    }
}
