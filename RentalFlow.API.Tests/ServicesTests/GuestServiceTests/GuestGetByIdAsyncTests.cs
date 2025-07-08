using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.GuestServiceTests
{
    public class GuestGetByIdAsyncTests
    {
        private readonly Mock<IGenericRepository<Guest>> _guestRepositoryMock;

        public GuestGetByIdAsyncTests()
        {
            _guestRepositoryMock = new Mock<IGenericRepository<Guest>>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnGuestDto_WhenGuestExists()
        {
            // Arrange
            var guest = new Guest
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.smith@example.com",
                PhoneNumber = "998901234567",
                Gender = Gender.Male
            };

            _guestRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(guest);

            var service = new GuestService(_guestRepositoryMock.Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(guest.Id);
            result.FirstName.Should().Be(guest.FirstName);
            result.Email.Should().Be(guest.Email);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenGuestDoesNotExist()
        {
            _guestRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                                .ReturnsAsync((Guest)null);

            var service = new GuestService(_guestRepositoryMock.Object);

            var result = await service.GetByIdAsync(9999);

            result.Should().BeNull(); 
        }

    }
}
