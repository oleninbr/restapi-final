using System.Net;
using System.Net.Http.Json;
using WebAPI.Dtos;
using Domain.Conditioners;
using Domain.Manufacturers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;
using Test.Data.Conditioners;
using Test.Data.Manufacturers;

namespace Api.Tests.Integrations.Conditioners;

public class ConditionersControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private readonly Manufacturer _testManufacturer = ManufacturersData.FirstTestManufacturer();
    private readonly Conditioner _testConditioner = ConditionersData.FirstTestConditioner();

    private const string BaseRoute = "conditioners";
    private readonly string _getRoute;

    public ConditionersControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
        _getRoute = $"{BaseRoute}/{_testConditioner.Id.Value}";
    }

    [Fact]
    public async Task ShouldGetConditionerById()
    {
        var response = await Client.GetAsync(_getRoute);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dto = await response.ToResponseModel<ConditionerDto>();
        dto.Id.Should().Be(_testConditioner.Id.Value);
        dto.Name.Should().Be(_testConditioner.Name);
    }

    [Fact]
    public async Task ShouldCreateConditioner()
    {
        var request = new CreateConditionerDto(
            _testConditioner.Name,
            _testConditioner.Model,
            _testConditioner.SerialNumber,
            _testConditioner.Location,
            _testConditioner.InstallationDate,
            _testConditioner.StatusId.Value,
            _testConditioner.TypeId.Value,
            _testManufacturer.Id.Value);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    public async Task InitializeAsync()
    {
        await Context.Manufacturers.AddAsync(_testManufacturer);
        await Context.Conditioners.AddAsync(_testConditioner);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Manufacturers.RemoveRange(Context.Manufacturers);
        Context.Conditioners.RemoveRange(Context.Conditioners);
        await SaveChangesAsync();
    }
}
