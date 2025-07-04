using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.HostServiceTests;

public class DeleteAsyncTests
{
    private readonly Mock<IGenericRepository<Host>> _repo;

    public DeleteAsyncTests()
    {
        _repo = new Mock<IGenericRepository<Host>>();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnDeletedCount_WhenDeletionIsSuccessful()
    {
        // Arrange
        long hostId = 1;
        _repo.Setup(r => r.DeleteAsync(hostId)).ReturnsAsync(1L);

        var service = new HostService(_repo.Object);

        // Act
        var result = await service.DeleteAsync(hostId);

        // Assert
        result.Should().Be(1L);
        _repo.Verify(r => r.DeleteAsync(hostId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnZero_WhenHostNotFound()
    {
        // Arrange
        long hostId = 999;
        _repo.Setup(r => r.DeleteAsync(hostId)).ReturnsAsync(0L);

        var service = new HostService(_repo.Object);

        // Act
        var result = await service.DeleteAsync(hostId);

        // Assert
        result.Should().Be(0L);
        _repo.Verify(r => r.DeleteAsync(hostId), Times.Once);
    }
}
