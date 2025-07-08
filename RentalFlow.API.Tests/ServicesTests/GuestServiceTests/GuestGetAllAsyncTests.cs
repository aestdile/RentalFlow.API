using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.GuestServiceTests
{
    public class GuestGetAllAsyncTests
    {
        private readonly Mock<IGenericRepository<Guest>> _guestRepositoryMock;

        public GuestGetAllAsyncTests()
        {
            _guestRepositoryMock = new Mock<IGenericRepository<Guest>>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfGuestDtos_WhenGuestsExist()
        {
            var guests = new List<Guest>
            {
                new Guest
                {
                    Id = 1,
                    FirstName = "Alice",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1995, 5, 10),
                    Email = "alice@example.com",
                    PhoneNumber = "998901234567",
                    Gender = Gender.Female
                },
                new Guest
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1988, 12, 25),
                    Email = "bob@example.com",
                    PhoneNumber = "998907654321",
                    Gender = Gender.Male
                }
            };

            _guestRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(guests);

            var service = new GuestService(_guestRepositoryMock.Object);

            var result = (await service.GetAllAsync()).ToList();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].FirstName.Should().Be("Alice");
            result[1].Email.Should().Be("bob@example.com");
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoGuestsExist()
        {
            _guestRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Guest>());

            var service = new GuestService(_guestRepositoryMock.Object);

            var result = await service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
