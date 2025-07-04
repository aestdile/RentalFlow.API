using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.GuestDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.GuestServiceTests;

public class GuestUpdateAsyncTests
{
    private readonly Mock<IGenericRepository<Guest>> _repo;

    public GuestUpdateAsyncTests() => _repo = new Mock<IGenericRepository<Guest>>();

    private GuestUpdateDto GetValidUpdateDto() => new GuestUpdateDto
    {
        FirstName = "John",
        LastName = "Doe",
        DateOfBirth = new DateTime(1990, 1, 1),
        PhoneNumber = "998901234567",
        Gender = Gender.Male
    };

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("john")]
    [InlineData("John123")]
    [InlineData("J@hn")]
    public async Task UpdateAsync_ShouldThrow_WhenFirstNameIsInvalid(string name)
    {
        var dto = GetValidUpdateDto();
        dto.FirstName = name;
        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Guest());

        var service = new GuestServiceWithValidation(_repo.Object);
        var act = async () => await service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*FirstName*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("doe")]
    [InlineData("Doe123")]
    [InlineData("Doe!")]
    public async Task UpdateAsync_ShouldThrow_WhenLastNameIsInvalid(string name)
    {
        var dto = GetValidUpdateDto();
        dto.LastName = name;
        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Guest());

        var service = new GuestServiceWithValidation(_repo.Object);
        var act = async () => await service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*LastName*");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(30)]
    public async Task UpdateAsync_ShouldThrow_WhenDateOfBirthIsInFuture(int days)
    {
        var dto = GetValidUpdateDto();
        dto.DateOfBirth = DateTime.Today.AddDays(days);
        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Guest());

        var service = new GuestServiceWithValidation(_repo.Object);
        var act = async () => await service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*DateOfBirth*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123abc")]
    [InlineData("99890")]
    [InlineData("12345678901234567")]
    public async Task UpdateAsync_ShouldThrow_WhenPhoneNumberIsInvalid(string phone)
    {
        var dto = GetValidUpdateDto();
        dto.PhoneNumber = phone;
        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Guest());

        var service = new GuestServiceWithValidation(_repo.Object);
        var act = async () => await service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*PhoneNumber*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public async Task UpdateAsync_ShouldThrow_WhenGenderIsInvalid(int gender)
    {
        var dto = GetValidUpdateDto();
        dto.Gender = (Gender)gender;
        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Guest());

        var service = new GuestServiceWithValidation(_repo.Object);
        var act = async () => await service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentOutOfRangeException>().WithMessage("*Gender*");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedDto_WhenAllValid()
    {
        var dto = GetValidUpdateDto();
        var existingGuest = new Guest
        {
            Id = 1,
            FirstName = "Old",
            LastName = "Guest",
            Email = "old@example.com"
        };

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(existingGuest);
        _repo.Setup(r => r.UpdateAsync(It.IsAny<long>(), It.IsAny<Guest>())).ReturnsAsync(existingGuest);

        var service = new GuestServiceWithValidation(_repo.Object);
        var result = await service.UpdateAsync(1, dto);

        result.Should().NotBeNull();
        result.FirstName.Should().Be(dto.FirstName);
        result.LastName.Should().Be(dto.LastName);
        result.PhoneNumber.Should().Be(dto.PhoneNumber);
    }
}
