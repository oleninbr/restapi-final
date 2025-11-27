using Domain.WorkOrderStatuses;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.WorkOrderStatuses
{
    public class WorkOrderStatusControllerTest : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly WorkOrderStatus _firstTestWorkOrderStatus =
            Test.Data.WorkOrderStatuses.WorkOrderStatusesData.FirstTestStatus();

        private const string BaseRoute = "work-order-statuses";

        public WorkOrderStatusControllerTest(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllWorkOrderStatuses()
        {
            // act
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();

            // assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var statuses = await response.ToResponseModel<List<WorkOrderStatusDto>>();
            statuses.Should().ContainSingle(s => s.Name == _firstTestWorkOrderStatus.Name);
        }

        [Fact]
        public async Task ShouldGetWorkOrderStatusById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_firstTestWorkOrderStatus.Id.Value}");
            response.EnsureSuccessStatusCode();

            var dto = await response.ToResponseModel<WorkOrderStatusDto>();
            dto.Name.Should().Be(_firstTestWorkOrderStatus.Name);
        }

        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateWorkOrderStatus()
        {
            var request = new CreateWorkOrderStatusDto("NewStatus");

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<WorkOrderStatusDto>();
            created.Name.Should().Be("NewStatus");

            var dbEntity = Context.WorkOrderStatuses
                .First(x => x.Id == new WorkOrderStatusId(created.Id));

            dbEntity.Name.Should().Be("NewStatus");
        }

        [Fact]
        public async Task ShouldUpdateWorkOrderStatus()
        {
            var request = new UpdateWorkOrderStatusDto(
                Id: _firstTestWorkOrderStatus.Id.Value,
                Name: "UpdatedStatus"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.WorkOrderStatuses
                .First(x => x.Id.Equals(_firstTestWorkOrderStatus.Id));

            updated.Name.Should().Be("UpdatedStatus");
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUpdatingMissingStatus()
        {
            var request = new UpdateWorkOrderStatusDto(
                Id: Guid.NewGuid(),
                Name: "DoesNotMatter"
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDeleteWorkOrderStatus()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestWorkOrderStatus.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.WorkOrderStatuses.Any(x => x.Id == _firstTestWorkOrderStatus.Id)
                .Should().BeFalse();
        }

        public async Task InitializeAsync()
        {
            await Context.WorkOrderStatuses.AddAsync(_firstTestWorkOrderStatus);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.WorkOrderStatuses.RemoveRange(Context.WorkOrderStatuses);
            await SaveChangesAsync();
        }
    }
}
