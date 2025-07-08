using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RentalFlow.API.Application.DTOs.HomeDTOs;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.API.IntegrationTests.Controllers;

public class HomeControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HomeControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private HomeCreateDto GenerateHome()
    {
        return new HomeCreateDto
        {
            HostId = 1,
            Address = "123 Elm Street",
            Description = "A cozy and modern home",
            IsAvailable = true,
            NoofBedRooms = 3,
            NoofBathRooms = 2,
            Area = 120,
            IsPetAllowed = true,
            HomeType = HomeType.Flat,
            Price = 150000
        };
    }

    [Fact]
    public async Task CreateHome_ShouldReturnCreated_WhenValid()
    {
        var home = GenerateHome();

        var response = await _client.PostAsJsonAsync("/api/home", home);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await response.Content.ReadFromJsonAsync<HomeDto>();
        created.Should().NotBeNull();
        created!.Address.Should().Be(home.Address);
    }

    [Fact]
    public async Task CreateHome_ShouldReturnBadRequest_WhenNullPayload()
    {
        var response = await _client.PostAsJsonAsync("/api/home", (HomeCreateDto)null!);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAllHomes_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/home");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var homes = await response.Content.ReadFromJsonAsync<List<HomeDto>>();
        homes.Should().NotBeNull();
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenExists()
    {
        var home = GenerateHome();
        var createResponse = await _client.PostAsJsonAsync("/api/home", home);
        var created = await createResponse.Content.ReadFromJsonAsync<HomeDto>();

        var response = await _client.GetAsync($"/api/home/{created!.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var fetched = await response.Content.ReadFromJsonAsync<HomeDto>();
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenNotExists()
    {
        var response = await _client.DeleteAsync("/api/home/999999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateHome_ShouldReturnOk_WhenValid()
    {
        var home = GenerateHome();
        var createResponse = await _client.PostAsJsonAsync("/api/home", home);
        var created = await createResponse.Content.ReadFromJsonAsync<HomeDto>();

        var updateDto = new HomeUpdateDto
        {
            Address = "Updated Street 456",
            Description = "Updated description",
            IsAvailable = false,
            NoofBedRooms = 2,
            NoofBathRooms = 1,
            Area = 80,
            IsPetAllowed = false,
            HomeType = HomeType.Flat,
            Price = 200000
        };

        var response = await _client.PutAsJsonAsync($"/api/home/{created!.Id}", updateDto);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await response.Content.ReadFromJsonAsync<HomeDto>();
        updated!.Address.Should().Be(updateDto.Address);
    }

    [Fact]
    public async Task UpdateHome_ShouldReturnNotFound_WhenNotExists()
    {
        var updateDto = new HomeUpdateDto
        {
            Address = "Nowhere",
            Description = "No description",
            IsAvailable = true,
            NoofBedRooms = 1,
            NoofBathRooms = 1,
            Area = 50,
            IsPetAllowed = false,
            HomeType = HomeType.Flat,
            Price = 100000
        };

        var response = await _client.PutAsJsonAsync("/api/home/999999", updateDto);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateHome_ShouldReturnBadRequest_WhenNullPayload()
    {
        var response = await _client.PutAsJsonAsync("/api/home/1", (HomeUpdateDto)null!);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteHome_ShouldReturnNoContent_WhenExists()
    {
        var home = GenerateHome();
        var createResponse = await _client.PostAsJsonAsync("/api/home", home);
        var created = await createResponse.Content.ReadFromJsonAsync<HomeDto>();

        var response = await _client.DeleteAsync($"/api/home/{created!.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteHome_ShouldReturnNotFound_WhenNotExists()
    {
        var response = await _client.DeleteAsync("/api/home/999999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
