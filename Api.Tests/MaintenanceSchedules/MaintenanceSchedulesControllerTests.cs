using Domain.Conditioners;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.MaintenanceFrequencies;
using Domain.MaintenanceSchedules;
using Domain.Manufacturers;
using FluentAssertions;
using System.Net.Http.Json;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.MaintenanceSchedules
{
    public class MaintenanceSchedulesControllerTests : BaseIntegrationTest, IAsyncLifetime
    {
        private readonly ConditionerStatus _firstTestConditionerStatus = Test.Data.ConditionerStatuses.ConditionerStatusesData.FirstTestStatus();
        private readonly ConditionerType _firstTestConditionerType = Test.Data.ConditionerTypes.ConditionerTypesData.FirstTestType();
        private readonly Manufacturer _firstTestManufacturer = Test.Data.Manufacturers.ManufacturersData.FirstTestManufacturer();

        private Conditioner _firstTestConditioner;
        private readonly MaintenanceFrequency _firstTestFrequency = Test.Data.MaintenanceFrequencies.MaintenanceFrequenciesData.FirstTestFrequency();
        private MaintenanceSchedule _firstTestMaintenanceSchedule;

        private const string BaseRoute = "maintenance-schedules";

        public MaintenanceSchedulesControllerTests(IntegrationTestWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldGetAllMaintenanceSchedules()
        {
            var response = await Client.GetAsync(BaseRoute);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var schedules = await response.ToResponseModel<List<MaintenanceScheduleDto>>();
            schedules.Should().ContainSingle(s => s.TaskName == _firstTestMaintenanceSchedule.TaskName);
        }

        [Fact]
        public async Task ShouldGetMaintenanceScheduleById()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{_firstTestMaintenanceSchedule.Id.Value}");
            response.EnsureSuccessStatusCode();

            var dto = await response.ToResponseModel<MaintenanceScheduleDto>();
            dto.TaskName.Should().Be(_firstTestMaintenanceSchedule.TaskName);
        }

        [Fact]
        public async Task ShouldReturnNotFoundForWrongId()
        {
            var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldCreateMaintenanceSchedule()
        {
            var request = new CreateMaintenanceScheduleDto(
                "NewSchedule", // TaskName
                "Test Description", // Description
                DateTime.UtcNow.AddDays(30), // NextDueDate
                true, // IsActive
                _firstTestMaintenanceSchedule.ConditionerId.Value, // ConditionerId
                _firstTestMaintenanceSchedule.FrequencyId.Value // FrequencyId
            );

            var response = await Client.PostAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var created = await response.ToResponseModel<MaintenanceScheduleDto>();
            created.TaskName.Should().Be("NewSchedule");

            var dbEntity = Context.MaintenanceSchedules
                .First(x => x.Id == new MaintenanceScheduleId(created.Id));

            dbEntity.TaskName.Should().Be("NewSchedule");
        }

        [Fact]
        public async Task ShouldUpdateMaintenanceSchedule()
        {
            var request = new UpdateMaintenanceScheduleDto(
                Id: _firstTestMaintenanceSchedule.Id.Value,
                TaskName: "UpdatedSchedule",
                Description: "Updated Description",
                NextDueDate: DateTime.UtcNow.AddDays(60),
                IsActive: false,
                ConditionerId: _firstTestMaintenanceSchedule.ConditionerId.Value,
                FrequencyId: _firstTestMaintenanceSchedule.FrequencyId.Value
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);
            response.IsSuccessStatusCode.Should().BeTrue();

            var updated = Context.MaintenanceSchedules
                .First(x => x.Id.Equals(_firstTestMaintenanceSchedule.Id));

            updated.TaskName.Should().Be("UpdatedSchedule");
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUpdatingMissingSchedule()
        {
            var request = new UpdateMaintenanceScheduleDto(
                Id: Guid.NewGuid(),
                TaskName: "DoesNotMatter",
                Description: "DoesNotMatter",
                NextDueDate: DateTime.UtcNow.AddDays(90),
                IsActive: false,
                ConditionerId: _firstTestMaintenanceSchedule.ConditionerId.Value,
                FrequencyId: _firstTestMaintenanceSchedule.FrequencyId.Value
            );

            var response = await Client.PutAsJsonAsync(BaseRoute, request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDeleteMaintenanceSchedule()
        {
            var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestMaintenanceSchedule.Id.Value}");
            response.IsSuccessStatusCode.Should().BeTrue();

            Context.MaintenanceSchedules.Any(x => x.Id == _firstTestMaintenanceSchedule.Id)
                .Should().BeFalse();
        }

        public async Task InitializeAsync()
        {
            await Context.ConditionerStatuses.AddAsync(_firstTestConditionerStatus);
            await Context.ConditionerTypes.AddAsync(_firstTestConditionerType);
            await Context.Manufacturers.AddAsync(_firstTestManufacturer);

            // Створюємо кондиціонер з правильним StatusId, TypeId, ManufacturerId
            _firstTestConditioner = Conditioner.New(
                ConditionerId.New(),
                "Test Conditioner",
                "Model X",
                "SN123456",
                "Test Location",
                DateTime.UtcNow,
                _firstTestConditionerStatus.Id,
                _firstTestConditionerType.Id,
                _firstTestManufacturer.Id
            );

            await Context.Conditioners.AddAsync(_firstTestConditioner);
            await Context.MaintenanceFrequencies.AddAsync(_firstTestFrequency);

            // Створюємо розклад з правильним ConditionerId та FrequencyId
            _firstTestMaintenanceSchedule = MaintenanceSchedule.New(
                MaintenanceScheduleId.New(),
                _firstTestConditioner.Id,
                "Test Task",
                "Test Description",
                _firstTestFrequency.Id,
                DateTime.UtcNow.AddDays(30),
                true
            );

            await Context.MaintenanceSchedules.AddAsync(_firstTestMaintenanceSchedule);
            await SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            Context.MaintenanceSchedules.RemoveRange(Context.MaintenanceSchedules);
            await SaveChangesAsync();
        }
    }
}
