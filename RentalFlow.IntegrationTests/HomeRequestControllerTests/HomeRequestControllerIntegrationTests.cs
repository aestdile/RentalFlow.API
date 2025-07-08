using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RentalFlow.API.Application.DTOs.GuestDTOs;
using RentalFlow.API.Application.DTOs.HomeDTOs;
using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Domain.Enums;
using Xunit;

namespace RentalFlow.IntegrationTests.HomeRequestControllerTests;

public class HomeRequestControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HomeRequestControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateHomeRequest_ShouldReturnCreated_WhenValid()
    {
        var guestDto = new GuestCreateDto
        {
            FirstName = "Test",
            LastName = "Guest",
            DateOfBirth = new DateTime(1995, 1, 1),
            Email = "guest@example.com",
            Password = "StrongPassword123!",
            PhoneNumber = "998901234567",
            Gender = Gender.Male
        };

        var guestResp = await _client.PostAsJsonAsync("/api/guest", guestDto);
        guestResp.EnsureSuccessStatusCode(); 
        var createdGuest = await guestResp.Content.ReadFromJsonAsync<GuestDto>();

        var homeDto = new HomeCreateDto
        {
            HostId = 1, 
            Address = "Tashkent, Uzbekistan",
            Description = "Test Home Description",
            IsAvailable = true,
            NoofBedRooms = 3,
            NoofBathRooms = 2,
            Area = 120.5,
            IsPetAllowed = true,
            HomeType = HomeType.Flat, 
            Price = 150_000M
        };

        var homeResp = await _client.PostAsJsonAsync("/api/home", homeDto);
        homeResp.EnsureSuccessStatusCode(); 
        var createdHome = await homeResp.Content.ReadFromJsonAsync<HomeDto>();

        var dto = new HomeRequestCreateDto
        {
            GuestId = createdGuest!.Id,
            HomeId = createdHome!.Id,
            RequestMessage = "Looking to stay",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(5)
        };

        var response = await _client.PostAsJsonAsync("/api/homerequest", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await response.Content.ReadFromJsonAsync<HomeRequestDto>();
        created!.RequestMessage.Should().Be(dto.RequestMessage);
    }


    [Fact]
    public async Task GetAllHomeRequests_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/homerequest");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetHomeRequestById_ShouldReturnNotFound_WhenInvalidId()
    {
        var response = await _client.GetAsync("/api/homerequest/99999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task UpdateHomeRequest_ShouldReturnOk_WhenValid()
    {
        var guestDto = new GuestCreateDto
        {
            FirstName = "Ali",
            LastName = "Valiyev",
            Email = "ali@example.com",
            Password = "123456",
            DateOfBirth = new DateTime(1995, 1, 1),
            PhoneNumber = "+998901234567",
            Gender = Gender.Male
        };

        var guestResp = await _client.PostAsJsonAsync("/api/guest", guestDto);
        guestResp.EnsureSuccessStatusCode();
        var guest = await guestResp.Content.ReadFromJsonAsync<GuestDto>();

        var hostDto = new HostCreateDto
        {
            FirstName = "Host",
            LastName = "Person",
            Email = "host@example.com",
            Password = "123456",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "+998909876543",
            Gender = Gender.Male
        };

        var hostResp = await _client.PostAsJsonAsync("/api/host", hostDto);
        hostResp.EnsureSuccessStatusCode();
        var host = await hostResp.Content.ReadFromJsonAsync<HostDto>();

        var homeDto = new HomeCreateDto
        {
            HostId = host!.Id,
            Address = "Tashkent",
            Description = "Nice place",
            IsAvailable = true,
            NoofBedRooms = 2,
            NoofBathRooms = 1,
            Area = 50,
            IsPetAllowed = false,
            HomeType = HomeType.Flat,
            Price = 200000
        };

        var homeResp = await _client.PostAsJsonAsync("/api/home", homeDto);
        homeResp.EnsureSuccessStatusCode();
        var home = await homeResp.Content.ReadFromJsonAsync<HomeDto>();

        var createDto = new HomeRequestCreateDto
        {
            GuestId = guest!.Id,
            HomeId = home!.Id,
            RequestMessage = "Initial",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2)
        };

        var createResp = await _client.PostAsJsonAsync("/api/homerequest", createDto);
        createResp.EnsureSuccessStatusCode();
        var created = await createResp.Content.ReadFromJsonAsync<HomeRequestDto>();

        var updateDto = new HomeRequestUpdateDto
        {
            RequestMessage = "Updated",
            StartDate = DateTime.UtcNow.AddDays(3),
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        var updateResp = await _client.PutAsJsonAsync($"/api/homerequest/{created!.Id}", updateDto);
        updateResp.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await updateResp.Content.ReadFromJsonAsync<HomeRequestDto>();
        updated!.RequestMessage.Should().Be("Updated");
    }




    [Fact]
    public async Task DeleteHomeRequest_ShouldReturnNotFound_WhenInvalidId()
    {
        var response = await _client.DeleteAsync("/api/homerequest/99999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
