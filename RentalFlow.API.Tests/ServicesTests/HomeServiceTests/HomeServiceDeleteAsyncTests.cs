using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.HomeServiceTests;

public class DeleteAsyncTests
{
    private readonly Mock<IGenericRepository<Home>> _homeRepoMock;

    public DeleteAsyncTests()
    {
        _homeRepoMock = new Mock<IGenericRepository<Home>>();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnDeletedCount_WhenIdExists()
    {
        // Arrange
        long inputId = 5;
        _homeRepoMock
            .Setup(repo => repo.DeleteAsync(inputId))
            .ReturnsAsync(1L); 

        var service = new HomeService(_homeRepoMock.Object);

        // Act
        var result = await service.DeleteAsync(inputId);

        // Assert
        result.Should().Be(1L);
        _homeRepoMock.Verify(repo => repo.DeleteAsync(inputId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnZero_WhenIdDoesNotExist()
    {
        // Arrange
        long inputId = 999;
        _homeRepoMock
            .Setup(repo => repo.DeleteAsync(inputId))
            .ReturnsAsync(0L);

        var service = new HomeService(_homeRepoMock.Object);

        // Act
        var result = await service.DeleteAsync(inputId);

        // Assert
        result.Should().Be(0L);
        _homeRepoMock.Verify(repo => repo.DeleteAsync(inputId), Times.Once);
    }
}
