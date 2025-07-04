using System.Text.RegularExpressions;
using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.GuestDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.GuestServiceTests
{
    public class GuestCreateAsyncTests
    {
        private readonly Mock<IGenericRepository<Guest>> _guestRepositoryMock;

        public GuestCreateAsyncTests()
        {
            _guestRepositoryMock = new Mock<IGenericRepository<Guest>>();
        }

        private GuestCreateDto GetValidGuestCreateDto() => new GuestCreateDto
        {
            FirstName = "John",
            LastName = "Smith",
            DateOfBirth = new DateTime(2000, 1, 1),
            Email = "john.smith@example.com",
            Password = "Strong@123",
            PhoneNumber = "998901234567",
            Gender = Gender.Male
        };

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("john")]
        [InlineData("JoHN")]
        [InlineData("Jasur2")]
        [InlineData("Otabek!")]
        [InlineData("Mu ham mad")]
        public async Task CreateAsync_ShouldThrow_WhenFirstNameIsInvalid(string firstName)
        {
            var dto = GetValidGuestCreateDto();
            dto.FirstName = firstName;
            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            Func<Task> act = () => svc.CreateAsync(dto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*FirstName*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("smith2")]
        [InlineData("Smith@")]
        [InlineData("Sm Ith")]
        [InlineData("smith")]
        public async Task CreateAsync_ShouldThrow_WhenLastNameIsInvalid(string lastName)
        {
            var dto = GetValidGuestCreateDto();
            dto.LastName = lastName;
            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            Func<Task> act = () => svc.CreateAsync(dto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*LastName*");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public async Task CreateAsync_ShouldThrow_WhenDateOfBirthIsInFuture(int daysToAdd)
        {
            var dto = GetValidGuestCreateDto();
            dto.DateOfBirth = DateTime.Today.AddDays(daysToAdd);
            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            Func<Task> act = () => svc.CreateAsync(dto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*DateOfBirth*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("plainaddress")]
        [InlineData("john.doe.com")]
        [InlineData("john@doe")]
        [InlineData("john@.com")]
        public async Task CreateAsync_ShouldThrow_WhenEmailIsInvalid(string email)
        {
            var dto = GetValidGuestCreateDto();
            dto.Email = email;
            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            Func<Task> act = () => svc.CreateAsync(dto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Email*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("password")]
        [InlineData("Password1")]
        [InlineData("password@1")]
        [InlineData("P@1")]
        public async Task CreateAsync_ShouldThrow_WhenPasswordIsWeak(string password)
        {
            var dto = GetValidGuestCreateDto();
            dto.Password = password;
            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            Func<Task> act = () => svc.CreateAsync(dto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Password*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("abc123")]
        [InlineData("12345")]
        [InlineData("1234567890123456")]
        [InlineData("+99890123@456")]
        public async Task CreateAsync_ShouldThrow_WhenPhoneNumberIsInvalid(string phone)
        {
            var dto = GetValidGuestCreateDto();
            dto.PhoneNumber = phone;
            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            Func<Task> act = () => svc.CreateAsync(dto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("*PhoneNumber*");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(99)]
        public async Task CreateAsync_ShouldThrow_WhenGenderIsInvalid(int genderValue)
        {
            var dto = GetValidGuestCreateDto();
            dto.Gender = (Gender)genderValue;
            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            Func<Task> act = () => svc.CreateAsync(dto);
            await act.Should().ThrowAsync<ArgumentOutOfRangeException>().WithMessage("*Gender*");
        }

        [Fact]
        public async Task CreateAsync_ShouldSucceed_WhenAllDataIsValid()
        {
            var dto = GetValidGuestCreateDto();

            _guestRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Guest>()))
                .ReturnsAsync(new Guest
                {
                    Id = 1,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth,
                    Email = dto.Email,
                    Password = dto.Password,
                    PhoneNumber = dto.PhoneNumber,
                    Gender = dto.Gender
                });

            var svc = new GuestServiceWithValidation(_guestRepositoryMock.Object);

            var result = await svc.CreateAsync(dto);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.FirstName.Should().Be(dto.FirstName);
        }
    }
}
