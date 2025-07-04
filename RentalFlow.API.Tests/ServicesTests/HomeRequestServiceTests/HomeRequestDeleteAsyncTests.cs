using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.HomeRequestTests;

public class DeleteAsyncTests
{
    private readonly Mock<IGenericRepository<HomeRequest>> _repo;

    public DeleteAsyncTests()
    {
        _repo = new Mock<IGenericRepository<HomeRequest>>();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnOne_WhenDeletionIsSuccessful()
    {
        // Arrange
        long requestId = 1;
        _repo.Setup(r => r.DeleteAsync(requestId)).ReturnsAsync(1L);
        var service = new HomeRequestService(_repo.Object);

        // Act
        var result = await service.DeleteAsync(requestId);

        // Assert
        result.Should().Be(1L);
        _repo.Verify(r => r.DeleteAsync(requestId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnZero_WhenNoRecordFound()
    {
        // Arrange
        long invalidId = 999;
        _repo.Setup(r => r.DeleteAsync(invalidId)).ReturnsAsync(0L);
        var service = new HomeRequestService(_repo.Object);

        // Act
        var result = await service.DeleteAsync(invalidId);

        // Assert
        result.Should().Be(0L);
        _repo.Verify(r => r.DeleteAsync(invalidId), Times.Once);
    }
}
