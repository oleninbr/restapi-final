using Domain.WorkOrderPriorities;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.WorkOrderPriorities
{
    public class WorkOrderPriorityControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly WorkOrderPriority _firstTestWorkOrderPriority =
            Test.Data.WorkOrderPriorities.WorkOrderPrioritiesData.FirstTestPriority();

        private const string BaseRoute = "work-order-priorities";

        public WorkOrderPriorityControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllWorkOrderPriorities()
        {
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var priorities = await response.ToResponseModel<List<WorkOrderPriorityDto>>();
            priorities.Should().ContainSingle(p => p.Name == _firstTestWorkOrderPriority.Name);
        }

        [Fact]
        public async Task ShouldGetWorkOrderPriorityById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_firstTestWorkOrderPriority.Id.Value}");
            response.EnsureSuccessStatusCode();

            var dto = await response.ToResponseModel<WorkOrderPriorityDto>();
            dto.Name.Should().Be(_firstTestWorkOrderPriority.Name);
        }

        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateWorkOrderPriority()
        {
            var request = new CreateWorkOrderPriorityDto("NewPriority");

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<WorkOrderPriorityDto>();
            created.Name.Should().Be("NewPriority");

            var dbEntity = Context.WorkOrderPriorities
                .First(x => x.Id == new WorkOrderPriorityId(created.Id));

            dbEntity.Name.Should().Be("NewPriority");
        }

        [Fact]
        public async Task ShouldUpdateWorkOrderPriority()
        {
            var request = new UpdateWorkOrderPriorityDto(
                Id: _firstTestWorkOrderPriority.Id.Value,
                Name: "UpdatedPriority"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.WorkOrderPriorities
                .First(x => x.Id.Equals(_firstTestWorkOrderPriority.Id));

            updated.Name.Should().Be("UpdatedPriority");
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUpdatingMissingPriority()
        {
            var request = new UpdateWorkOrderPriorityDto(
                Id: Guid.NewGuid(),
                Name: "DoesNotMatter"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDeleteWorkOrderPriority()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestWorkOrderPriority.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.WorkOrderPriorities.Any(x => x.Id == _firstTestWorkOrderPriority.Id)
                .Should().BeFalse();
        }

        public async Task InitializeAsync()
        {
            await Context.WorkOrderPriorities.AddAsync(_firstTestWorkOrderPriority);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.WorkOrderPriorities.RemoveRange(Context.WorkOrderPriorities);
            await SaveChangesAsync();
        }
    }
}
