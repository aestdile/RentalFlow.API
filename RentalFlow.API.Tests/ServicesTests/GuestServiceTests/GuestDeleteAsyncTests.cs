using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.GuestServiceTests
{
    public class GuestDeleteAsyncTests
    {
        private readonly Mock<IGenericRepository<Guest>> _guestRepositoryMock;

        public GuestDeleteAsyncTests()
        {
            _guestRepositoryMock = new Mock<IGenericRepository<Guest>>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnDeletedCount_WhenDeletionIsSuccessful()
        {
            // Arrange
            _guestRepositoryMock
                .Setup(repo => repo.DeleteAsync(5))
                .ReturnsAsync(1L);

            var service = new GuestService(_guestRepositoryMock.Object);

            // Act
            var result = await service.DeleteAsync(5);

            // Assert
            result.Should().Be(1L);
            _guestRepositoryMock.Verify(repo => repo.DeleteAsync(5), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnZero_WhenNothingDeleted()
        {
            // Arrange: repository returns 0 (id not found)
            _guestRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<long>()))
                .ReturnsAsync(0L);

            var service = new GuestService(_guestRepositoryMock.Object);

            // Act
            var result = await service.DeleteAsync(123);

            // Assert
            result.Should().Be(0L);
            _guestRepositoryMock.Verify(repo => repo.DeleteAsync(123), Times.Once);
        }
    }
}
