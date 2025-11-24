using FluentAssertions;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.ConditionerTypes
{
    public class ConditionTypesControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly Domain.ConditionerTypes.ConditionerType _firstTestConditionerType = 
            Test.Data.ConditionerTypes.ConditionerTypesData.FirstTestType();

        private const string Baseroute = "conditioner-types";

        public ConditionTypesControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllConditionerTypes()
        {
            //arrange

            //act
            var response = await Client.GetAsync(Baseroute);
            response.EnsureSuccessStatusCode();
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            // We only expect a List<ConditionerTypeDto>, not a single Dto wrapper
            var conditionerTypes = await response.ToResponseModel<List<ConditionerTypeDto>>();
            conditionerTypes.Should().ContainSingle(c => c.Name == _firstTestConditionerType.Name);
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