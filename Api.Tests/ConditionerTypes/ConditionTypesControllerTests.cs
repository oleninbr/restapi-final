using Domain.ConditionerTypes;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.ConditionerTypes
{
    public class ConditionTypesControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly ConditionerType _firstTestConditionerType = 
            Test.Data.ConditionerTypes.ConditionerTypesData.FirstTestType();

        private const string BaseRoute = "conditioner-types";

        public ConditionTypesControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllConditionerTypes()
        {
            //arrange


            //act
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // We only expect a List<ConditionerTypeDto>, not a single Dto wrapper
            var conditionerTypes = await response.ToResponseModel<List<ConditionerTypeDto>>();
            conditionerTypes.Should().ContainSingle(c => c.Name == _firstTestConditionerType.Name);
        }


        [Fact]
        public async Task ShouldGetConditionerTypeById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_firstTestConditionerType.Id.Value}");
            response.EnsureSuccessStatusCode();

            var dto = await response.ToResponseModel<ConditionerTypeDto>();
            dto.Name.Should().Be(_firstTestConditionerType.Name);
        }


        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateConditionerType()
        {
            var request = new CreateConditionerTypeDto("NewTestType");

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<ConditionerTypeDto>();
            created.Name.Should().Be("NewTestType");

            var dbEntity = Context.ConditionerTypes
        .First(x => x.Id == new ConditionerTypeId(created.Id));

            dbEntity.Name.Should().Be("NewTestType");
        }


        [Fact]
        public async Task ShouldUpdateConditionerType()
        {
            var request = new UpdateConditionerTypeDto(
                Id: _firstTestConditionerType.Id.Value,
                Name: "UpdatedName"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.ConditionerTypes
                .First(x => x.Id.Equals(_firstTestConditionerType.Id));

            updated.Name.Should().Be("UpdatedName");
        }



        [Fact]
        public async Task ShouldReturnNotFoundWhenUpdatingMissingType()
        {
            var request = new UpdateConditionerTypeDto(
                Id: Guid.NewGuid(),
                Name: "DoesNotMatter"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task ShouldDeleteConditionerType()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestConditionerType.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.ConditionerTypes.Any(x => x.Id == _firstTestConditionerType.Id)
                .Should().BeFalse();
        }

        public async Task InitializeAsync()
        {
            // Just add the data needed for this test
            await Context.ConditionerTypes.AddAsync(_firstTestConditionerType);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            // Just clean up the data added by this test
            Context.ConditionerTypes.RemoveRange(Context.ConditionerTypes);
            await SaveChangesAsync();
        }
    }
}