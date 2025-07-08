using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RentalFlow.API.Application.DTOs.HostDTOs;

namespace RentalFlow.IntegrationTests.HostControllerTests;

public class HostControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HostControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private HostCreateDto GenerateHostCreateDto() => new HostCreateDto
    {
        FirstName = "Test",
        LastName = "Host",
        Email = $"test{Guid.NewGuid()}@mail.com",
        Password = "Secure123!",
        PhoneNumber = "+998901234567",
        Gender = RentalFlow.API.Domain.Enums.Gender.Male,
        DateOfBirth = new DateTime(1990, 1, 1)
    };

    [Fact]
    public async Task CreateHost_ShouldReturnCreated_WhenValid()
    {
        var dto = GenerateHostCreateDto();

        var response = await _client.PostAsJsonAsync("/api/host", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<HostDto>();
        result.Should().NotBeNull();
        result!.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task GetAllHosts_ShouldReturnOk_WithList()
    {
        var response = await _client.GetAsync("/api/host");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var list = await response.Content.ReadFromJsonAsync<IEnumerable<HostDto>>();
        list.Should().NotBeNull();
    }

    [Fact]
    public async Task GetHostById_ShouldReturnOk_WhenExists()
    {
        var dto = GenerateHostCreateDto();
        var createResp = await _client.PostAsJsonAsync("/api/host", dto);
        var created = await createResp.Content.ReadFromJsonAsync<HostDto>();

        var response = await _client.GetAsync($"/api/host/{created!.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<HostDto>();
        result!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task UpdateHost_ShouldReturnOk_WhenValid()
    {
        var createDto = GenerateHostCreateDto();
        var createResp = await _client.PostAsJsonAsync("/api/host", createDto);
        var created = await createResp.Content.ReadFromJsonAsync<HostDto>();

        var updateDto = new HostUpdateDto
        {
            FirstName = "Updated",
            LastName = "User",
            PhoneNumber = "+998901111111",
            Gender = RentalFlow.API.Domain.Enums.Gender.Female,
            DateOfBirth = new DateTime(1995, 1, 1)
        };

        var response = await _client.PutAsJsonAsync($"/api/host/{created!.Id}", updateDto);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await response.Content.ReadFromJsonAsync<HostDto>();
        updated!.FirstName.Should().Be("Updated");
    }

    [Fact]
    public async Task DeleteHost_ShouldReturnNoContent_WhenValid()
    {
        var createDto = GenerateHostCreateDto();
        var createResp = await _client.PostAsJsonAsync("/api/host", createDto);
        var created = await createResp.Content.ReadFromJsonAsync<HostDto>();

        var response = await _client.DeleteAsync($"/api/host/{created!.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetHostById_ShouldReturnNotFound_WhenInvalidId()
    {
        var response = await _client.GetAsync("/api/host/999999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateHost_ShouldReturnNotFound_WhenInvalidId()
    {
        var updateDto = new HostUpdateDto
        {
            FirstName = "Fake",
            LastName = "Person",
            PhoneNumber = "000",
            Gender = RentalFlow.API.Domain.Enums.Gender.Male,
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        var response = await _client.PutAsJsonAsync("/api/host/999999", updateDto);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task DeleteHost_ShouldReturnNotFound_WhenInvalidId()
    {
        var response = await _client.DeleteAsync("/api/host/999999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
