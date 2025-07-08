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
        var homeId = 1L;

        var mockRepo = new Mock<IGenericRepository<Home>>();

        mockRepo.Setup(r => r.GetByIdAsync(homeId))
                .ReturnsAsync(new Home { Id = homeId });

        mockRepo.Setup(r => r.DeleteAsync(homeId))
                .ReturnsAsync(1L);

        var service = new HomeService(mockRepo.Object);

        var result = await service.DeleteAsync(homeId);

        result.Should().Be(1L);

        mockRepo.Verify(r => r.GetByIdAsync(homeId), Times.Once);
        mockRepo.Verify(r => r.DeleteAsync(homeId), Times.Once);
    }



    [Fact]
    public async Task DeleteAsync_ShouldReturnZero_WhenIdDoesNotExist()
    {
        var nonExistingId = 999L;
        var mockRepo = new Mock<IGenericRepository<Home>>();

        mockRepo.Setup(r => r.GetByIdAsync(nonExistingId))
                .ReturnsAsync((Home?)null); 

        mockRepo.Setup(r => r.DeleteAsync(nonExistingId))
                .ReturnsAsync(0L); 

        var service = new HomeService(mockRepo.Object);

        var result = await service.DeleteAsync(nonExistingId);

        result.Should().Be(0L);

        mockRepo.Verify(r => r.DeleteAsync(It.IsAny<long>()), Times.Never);
    }

}
