using FluentAssertions;
using Tests.Common;
using WebAPI.Dtos;


namespace Api.Tests.Integrations.ConditionerTypes
{
    public class ConditionTypesControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly Domain.ConditionerTypes. ConditionerType firstTestConditionerType = 
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

            var dto = await response.ToResponseModel<ConditionerTypeDto>();
            var conditionerTypes = await response.ToResponseModel<List<ConditionerTypeDto>>();
            conditionerTypes.Should().ContainSingle(c => c.Name == firstTestConditionerType.Name);
        }


        public async Task InitializeAsync()
        {
            await Context.ConditionerTypes.AddAsync(firstTestConditionerType);
            //await Context.Conditioners.AddAsync(_testConditioner);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.ConditionerTypes.RemoveRange(Context.ConditionerTypes);
            //Context.Conditioners.RemoveRange(Context.Conditioners);
            await SaveChangesAsync();
        }
    }
}
