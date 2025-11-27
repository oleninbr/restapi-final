using Domain.Conditioners;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.Manufacturers;
using Domain.WorkOrders;
using Domain.WorkOrderPriorities;
using Domain.WorkOrderStatuses;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.WorkOrders
{
    public class WorkOrderControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly ConditionerStatus _firstTestConditionerStatus = Test.Data.ConditionerStatuses.ConditionerStatusesData.FirstTestStatus();
        private readonly ConditionerType _firstTestConditionerType = Test.Data.ConditionerTypes.ConditionerTypesData.FirstTestType();
        private readonly Manufacturer _firstTestManufacturer = Test.Data.Manufacturers.ManufacturersData.FirstTestManufacturer();

        private Conditioner _firstTestConditioner;
        private WorkOrderPriority _firstTestPriority;
        private WorkOrderStatus _firstTestStatus;
        private WorkOrder _firstTestWorkOrder;

        private const string BaseRoute = "work-orders";

        public WorkOrderControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllWorkOrders()
        {
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var workOrders = await response.ToResponseModel<List<WorkOrderDto>>();
            workOrders.Should().ContainSingle(w => w.Id == _firstTestWorkOrder.Id.Value);
        }

        [Fact]
        public async Task ShouldGetWorkOrderById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_firstTestWorkOrder.Id.Value}");
            response.EnsureSuccessStatusCode();

            var dto = await response.ToResponseModel<WorkOrderDto>();
            dto.Id.Should().Be(_firstTestWorkOrder.Id.Value);
        }

        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateWorkOrder()
        {
            var request = new CreateWorkOrderDto(
                WorkOrderNumber: "WO-2001",
                Title: "New WorkOrder",
                Description: "Test Description",
                ScheduledDate: DateTime.UtcNow.AddDays(7),
                ConditionerId: _firstTestConditioner.Id.Value,
                PriorityId: _firstTestPriority.Id.Value,
                StatusId: _firstTestStatus.Id.Value
            );

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<WorkOrderDto>();
            created.Title.Should().Be("New WorkOrder");

            var dbEntity = Context.WorkOrders
                .First(x => x.Id == new WorkOrderId(created.Id));

            dbEntity.Title.Should().Be("New WorkOrder");
        }

        [Fact]
        public async Task ShouldUpdateWorkOrder()
        {
            var request = new UpdateWorkOrderDto(
                Id: _firstTestWorkOrder.Id.Value,
                Title: "Updated WorkOrder",
                Description: "Updated Description",
                PriorityId: _firstTestPriority.Id.Value,
                StatusId: _firstTestStatus.Id.Value,
                ScheduledDate: DateTime.UtcNow.AddDays(14)
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.WorkOrders
                .First(x => x.Id.Equals(_firstTestWorkOrder.Id));

            updated.Title.Should().Be("Updated WorkOrder");
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUpdatingMissingWorkOrder()
        {
            var request = new UpdateWorkOrderDto(
                Id: Guid.NewGuid(),
                Title: "DoesNotMatter",
                Description: "DoesNotMatter",
                PriorityId: _firstTestPriority.Id.Value,
                StatusId: _firstTestStatus.Id.Value,
                ScheduledDate: DateTime.UtcNow.AddDays(30)
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDeleteWorkOrder()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestWorkOrder.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.WorkOrders.Any(x => x.Id == _firstTestWorkOrder.Id)
                .Should().BeFalse();
        }

        public async Task InitializeAsync()
        {
            // Створюємо всі залежні сутності з узгодженими Id
            _firstTestPriority = Test.Data.WorkOrderPriorities.WorkOrderPrioritiesData.FirstTestPriority();
            _firstTestStatus = Test.Data.WorkOrderStatuses.WorkOrderStatusesData.FirstTestStatus();

            await Context.ConditionerStatuses.AddAsync(_firstTestConditionerStatus);
            await Context.ConditionerTypes.AddAsync(_firstTestConditionerType);
            await Context.Manufacturers.AddAsync(_firstTestManufacturer);

            _firstTestConditioner = Conditioner.New(
                ConditionerId.New(),
                "LG ArtCool",
                "AC-120",
                "SN12345",
                "Office A1",
                DateTime.UtcNow,
                _firstTestConditionerStatus.Id,
                _firstTestConditionerType.Id,
                _firstTestManufacturer.Id
            );

            await Context.Conditioners.AddAsync(_firstTestConditioner);
            await Context.WorkOrderPriorities.AddAsync(_firstTestPriority);
            await Context.WorkOrderStatuses.AddAsync(_firstTestStatus);

            _firstTestWorkOrder = WorkOrder.New(
                WorkOrderId.New(),
                "WO-1001",
                _firstTestConditioner.Id,
                "Fix Compressor",
                "Replace damaged compressor and test cooling.",
                _firstTestPriority.Id,
                _firstTestStatus.Id,
                DateTime.UtcNow.AddDays(3)
            );

            await Context.WorkOrders.AddAsync(_firstTestWorkOrder);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.WorkOrders.RemoveRange(Context.WorkOrders);
            await SaveChangesAsync();
        }
    }
}
