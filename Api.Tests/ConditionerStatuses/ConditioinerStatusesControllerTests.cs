using System.Net.Http.Json;
using Domain.ConditionerStatuses;
using FluentAssertions;
using Tests.Common;
using WebAPI.Dtos;

namespace Api.Tests.Integrations.ConditionerStatuses;

public class ConditionerStatusesControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly ConditionerStatus _firstTestConditionerStatus =
        Test.Data.ConditionerStatuses.ConditionerStatusesData.FirstTestStatus();

    private readonly ConditionerStatus _secondTestConditionerStatus =
        Test.Data.ConditionerStatuses.ConditionerStatusesData.SecondTestStatus();

    private const string BaseRoute = "conditioner-statuses";

    public ConditionerStatusesControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldGetAllConditionerStatuses()
    {
        // act
        var response = await Client.GetAsync(BaseRoute);
        response.EnsureSuccessStatusCode();
        /*var body = await response.Content.ReadAsStringAsync();
        throw new Exception($"STATUS: {response.StatusCode}\nBODY: {body}");*/


        // assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var statuses = await response.ToResponseModel<List<ConditionerStatusDto>>();
        statuses.Should().ContainSingle(s => s.Name == _firstTestConditionerStatus.Name);
    }

    [Fact]
    public async Task ShouldGetConditionerStatusById()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_firstTestConditionerStatus.Id.Value}");
        response.EnsureSuccessStatusCode();

        var dto = await response.ToResponseModel<ConditionerStatusDto>();
        dto.Name.Should().Be(_firstTestConditionerStatus.Name);
    }

    [Fact]
    public async Task ShouldReturnNotFoundForWrongId()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldCreateConditionerStatus()
    {
        var request = new CreateConditionerStatusDto("NewStatus");

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.IsSuccessStatusCode.Should().BeTrue();

        var created = await response.ToResponseModel<ConditionerStatusDto>();
        created.Name.Should().Be("NewStatus");

        var dbEntity = Context.ConditionerStatuses
            .First(x => x.Id == new ConditionerStatusId(created.Id));

        dbEntity.Name.Should().Be("NewStatus");
    }

    [Fact]
    public async Task ShouldUpdateConditionerStatus()
    {
        var request = new UpdateConditionerStatusDto(
            Id: _firstTestConditionerStatus.Id.Value,
            Name: "UpdatedStatus"
        );

        var response = await Client.PutAsJsonAsync(BaseRoute, request);
        response.IsSuccessStatusCode.Should().BeTrue();

        var updated = Context.ConditionerStatuses
            .First(x => x.Id.Equals(_firstTestConditionerStatus.Id));

        updated.Name.Should().Be("UpdatedStatus");
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingMissingStatus()
    {
        var request = new UpdateConditionerStatusDto(
            Id: Guid.NewGuid(),
            Name: "DoesNotMatter"
        );

        var response = await Client.PutAsJsonAsync(BaseRoute, request);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeleteConditionerStatus()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_firstTestConditionerStatus.Id.Value}");
        response.IsSuccessStatusCode.Should().BeTrue();

        Context.ConditionerStatuses.Any(x => x.Id == _firstTestConditionerStatus.Id)
            .Should().BeFalse();
    }

    public async Task InitializeAsync()
    {
        await Context.ConditionerStatuses.AddAsync(_firstTestConditionerStatus);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.ConditionerStatuses.RemoveRange(Context.ConditionerStatuses);
        await SaveChangesAsync();
    }
}
