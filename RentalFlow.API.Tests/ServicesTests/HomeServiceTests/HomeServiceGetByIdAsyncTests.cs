using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;
using RentalFlow.API.Application.DTOs.HomeDTOs;

namespace RentalFlow.UnitTests.HomeServiceTests;

public class GetByIdAsyncTests
{
    private readonly Mock<IGenericRepository<Home>> _homeRepoMock;

    public GetByIdAsyncTests()
    {
        _homeRepoMock = new Mock<IGenericRepository<Home>>();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnHomeDto_WhenHomeExists()
    {
        // Arrange
        var homeId = 1L;
        var home = new Home
        {
            Id = homeId,
            HostId = 10,
            Address = "Tashkent",
            Description = "Cozy and clean house",
            IsAvailable = true,
            NoofBedRooms = 2,
            NoofBathRooms = 1,
            Area = 120.5,
            IsPetAllowed = true,
            HomeType = HomeType.Flat,
            Price = 1500.00m
        };

        _homeRepoMock.Setup(r => r.GetByIdAsync(homeId)).ReturnsAsync(home);

        var service = new HomeService(_homeRepoMock.Object);

        // Act
        var result = await service.GetByIdAsync(homeId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(homeId);
        result.Address.Should().Be(home.Address);
        result.Price.Should().Be(home.Price);
        result.HomeType.Should().Be(home.HomeType);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFound_WhenHomeNotFound()
    {
        // Arrange
        var homeId = 99L;
        _homeRepoMock.Setup(r => r.GetByIdAsync(homeId)).ReturnsAsync((Home?)null);

        var service = new HomeService(_homeRepoMock.Object);

        // Act
        Func<Task> act = () => service.GetByIdAsync(homeId);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"*{homeId}*");
    }
}
