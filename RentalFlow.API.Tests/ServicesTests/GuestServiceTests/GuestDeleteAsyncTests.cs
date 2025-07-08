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
            _guestRepositoryMock
                .Setup(repo => repo.DeleteAsync(5))
                .ReturnsAsync(1L);

            var service = new GuestService(_guestRepositoryMock.Object);

            var result = await service.DeleteAsync(5);

            result.Should().Be(1L);
            _guestRepositoryMock.Verify(repo => repo.DeleteAsync(5), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnZero_WhenNothingDeleted()
        {
            _guestRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<long>()))
                .ReturnsAsync(0L);

            var service = new GuestService(_guestRepositoryMock.Object);

            var result = await service.DeleteAsync(123);

            result.Should().Be(0L);
            _guestRepositoryMock.Verify(repo => repo.DeleteAsync(123), Times.Once);
        }
    }
}
