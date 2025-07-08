using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.HomeRequestTests;

public class GetAllAsyncTests
{
    private readonly Mock<IGenericRepository<HomeRequest>> _repo;

    public GetAllAsyncTests()
    {
        _repo = new Mock<IGenericRepository<HomeRequest>>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedDtos_WhenDataExists()
    {
        var homeRequests = new List<HomeRequest>
        {
            new HomeRequest
            {
                Id = 1,
                GuestId = 101,
                HomeId = 201,
                RequestMessage = "Vacation request",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(5),
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow
            },
            new HomeRequest
            {
                Id = 2,
                GuestId = 102,
                HomeId = 202,
                RequestMessage = "Weekend stay",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow
            }
        };

        _repo.Setup(r => r.GetAllAsync()).ReturnsAsync(homeRequests);
        var service = new HomeRequestService(_repo.Object);

        var result = (await service.GetAllAsync()).ToList();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result[0].RequestMessage.Should().Be("Vacation request");
        result[1].GuestId.Should().Be(102);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoDataExists()
    {
        _repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<HomeRequest>());
        var service = new HomeRequestService(_repo.Object);

        var result = await service.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
