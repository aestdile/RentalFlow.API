using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;
using RentalFlow.API.Application.DTOs.HomeDTOs;

namespace RentalFlow.UnitTests.HomeServiceTests;

public class GetAllAsyncTests
{
    private readonly Mock<IGenericRepository<Home>> _homeRepoMock;

    public GetAllAsyncTests()
    {
        _homeRepoMock = new Mock<IGenericRepository<Home>>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfHomeDtos_WhenHomesExist()
    {
        var homes = new List<Home>
        {
            new Home
            {
                Id = 1,
                HostId = 10,
                Address = "Tashkent",
                Description = "Nice view",
                IsAvailable = true,
                NoofBedRooms = 2,
                NoofBathRooms = 1,
                Area = 100,
                IsPetAllowed = true,
                HomeType = HomeType.Flat,
                Price = 1500
            },
            new Home
            {
                Id = 2,
                HostId = 11,
                Address = "Samarkand",
                Description = "Large house",
                IsAvailable = false,
                NoofBedRooms = 4,
                NoofBathRooms = 2,
                Area = 200,
                IsPetAllowed = false,
                HomeType = HomeType.Flat,
                Price = 2500
            }
        };

        _homeRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(homes);

        var service = new HomeService(_homeRepoMock.Object);

        var result = await service.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().Address.Should().Be("Tashkent");
        result.Last().HomeType.Should().Be(HomeType.Flat);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoHomesExist()
    {
        _homeRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Home>());

        var service = new HomeService(_homeRepoMock.Object);

        var result = await service.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
