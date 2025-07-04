using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.HostServiceTests;

public class GetAllAsyncTests
{
    private readonly Mock<IGenericRepository<Host>> _repo;

    public GetAllAsyncTests()
    {
        _repo = new Mock<IGenericRepository<Host>>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfHostDtos_WhenHostsExist()
    {
        // Arrange
        var hosts = new List<Host>
        {
            new Host
            {
                Id = 1,
                FirstName = "Ali",
                LastName = "Valiyev",
                DateOfBirth = new DateTime(1990, 5, 10),
                Email = "ali@example.com",
                PhoneNumber = "998901234567",
                Gender = Gender.Male
            },
            new Host
            {
                Id = 2,
                FirstName = "Laylo",
                LastName = "Karimova",
                DateOfBirth = new DateTime(1992, 3, 15),
                Email = "laylo@example.com",
                PhoneNumber = "998909876543",
                Gender = Gender.Female
            }
        };

        _repo.Setup(r => r.GetAllAsync()).ReturnsAsync(hosts);
        var service = new HostService(_repo.Object);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().FirstName.Should().Be("Ali");
        result.Last().Email.Should().Be("laylo@example.com");
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoHostsExist()
    {
        // Arrange
        _repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Host>());
        var service = new HostService(_repo.Object);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
