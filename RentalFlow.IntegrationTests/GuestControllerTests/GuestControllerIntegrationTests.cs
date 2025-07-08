using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RentalFlow.API.Application.DTOs.GuestDTOs;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.IntegrationTests.GuestControllerTests;

public class GuestControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GuestControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private GuestCreateDto GenerateGuest()
    {
        return new GuestCreateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = $"{Guid.NewGuid()}@test.com",
            Password = "Secure123!",
            DateOfBirth = DateTime.UtcNow.AddYears(-25),
            PhoneNumber = "998901112233",
            Gender = Gender.Male
        };
    }

    [Fact]
    public async Task CreateGuest_ShouldReturnCreated_WhenValid()
    {
        var guestDto = GenerateGuest();

        var response = await _client.PostAsJsonAsync("/api/guest", guestDto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdGuest = await response.Content.ReadFromJsonAsync<GuestDto>();
        createdGuest.Should().NotBeNull();
        createdGuest!.Email.Should().Be(guestDto.Email);
    }

    [Fact]
    public async Task CreateGuest_ShouldReturnBadRequest_WhenNullPayload()
    {
        var response = await _client.PostAsJsonAsync("/api/guest", (GuestCreateDto)null!);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAllGuests_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/guest");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var guests = await response.Content.ReadFromJsonAsync<List<GuestDto>>();
        guests.Should().NotBeNull();
    }

    [Fact]
    public async Task GetGuestById_ShouldReturnGuest_WhenExists()
    {
        var guest = GenerateGuest();
        var createResponse = await _client.PostAsJsonAsync("/api/guest", guest);
        var createdGuest = await createResponse.Content.ReadFromJsonAsync<GuestDto>();

        var response = await _client.GetAsync($"/api/guest/{createdGuest!.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var returnedGuest = await response.Content.ReadFromJsonAsync<GuestDto>();
        returnedGuest!.Id.Should().Be(createdGuest.Id);
    }

    [Fact]
    public async Task GetGuestById_ShouldReturnNotFound_WhenNotExists()
    {
        var response = await _client.GetAsync("/api/guest/999999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateGuest_ShouldReturnOk_WhenValid()
    {
        var guest = GenerateGuest();
        var createResponse = await _client.PostAsJsonAsync("/api/guest", guest);
        var createdGuest = await createResponse.Content.ReadFromJsonAsync<GuestDto>();

        var updateDto = new GuestUpdateDto
        {
            FirstName = "Updated",
            LastName = "Name",
            PhoneNumber = "998909999999"
        };

        var response = await _client.PutAsJsonAsync($"/api/guest/{createdGuest!.Id}", updateDto);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await response.Content.ReadFromJsonAsync<GuestDto>();
        updated!.FirstName.Should().Be("Updated");
    }

    [Fact]
    public async Task UpdateGuest_ShouldReturnBadRequest_WhenNullPayload()
    {
        var response = await _client.PutAsJsonAsync("/api/guest/1", (GuestUpdateDto)null!);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateGuest_ShouldReturnNotFound_WhenInvalidId()
    {
        var updateDto = new GuestUpdateDto
        {
            FirstName = "Test",
            LastName = "NotFound",
            PhoneNumber = "998909999000"
        };

        var response = await _client.PutAsJsonAsync("/api/guest/999999", updateDto);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteGuest_ShouldReturnOk_WhenExists()
    {
        var guest = GenerateGuest();
        var createResponse = await _client.PostAsJsonAsync("/api/guest", guest);
        var createdGuest = await createResponse.Content.ReadFromJsonAsync<GuestDto>();

        var response = await _client.DeleteAsync($"/api/guest/{createdGuest!.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteGuest_ShouldReturnNotFound_WhenNotExists()
    {
        var response = await _client.DeleteAsync("/api/guest/999999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
