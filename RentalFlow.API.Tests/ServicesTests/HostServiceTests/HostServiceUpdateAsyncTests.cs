using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.HostServiceTests;

public class UpdateAsyncTests
{
    private readonly Mock<IGenericRepository<Host>> _repo;

    public UpdateAsyncTests() =>
        _repo = new Mock<IGenericRepository<Host>>();

    private HostUpdateDto GetValidUpdateDto() => new HostUpdateDto
    {
        FirstName = "Alice",
        LastName = "Smith",
        DateOfBirth = new DateTime(1995, 5, 5),
        PhoneNumber = "998901234567",
        Gender = Gender.Female
    };

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("alice")]
    [InlineData("Alice123")]
    [InlineData("A!ice")]
    public async Task UpdateAsync_ShouldThrow_WhenFirstNameInvalid(string name)
    {
        var dto = GetValidUpdateDto();
        dto.FirstName = name;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Host());

        var service = new HostServiceWithValidation(_repo.Object);
        var act = () => service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*FirstName*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("smith")]
    [InlineData("Smith123")]
    [InlineData("Sm!th")]
    public async Task UpdateAsync_ShouldThrow_WhenLastNameInvalid(string name)
    {
        var dto = GetValidUpdateDto();
        dto.LastName = name;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Host());

        var service = new HostServiceWithValidation(_repo.Object);
        var act = () => service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*LastName*");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public async Task UpdateAsync_ShouldThrow_WhenDateOfBirthInFuture(int days)
    {
        var dto = GetValidUpdateDto();
        dto.DateOfBirth = DateTime.Today.AddDays(days);

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Host());

        var service = new HostServiceWithValidation(_repo.Object);
        var act = () => service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*DateOfBirth*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123abc")]
    [InlineData("99890")]
    [InlineData("99890123456789123")]
    public async Task UpdateAsync_ShouldThrow_WhenPhoneNumberInvalid(string phone)
    {
        var dto = GetValidUpdateDto();
        dto.PhoneNumber = phone;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Host());

        var service = new HostServiceWithValidation(_repo.Object);
        var act = () => service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*PhoneNumber*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(99)]
    public async Task UpdateAsync_ShouldThrow_WhenGenderInvalid(int gender)
    {
        var dto = GetValidUpdateDto();
        dto.Gender = (Gender)gender;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Host());

        var service = new HostServiceWithValidation(_repo.Object);
        var act = () => service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentOutOfRangeException>().WithMessage("*Gender*");
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedDto_WhenAllValid()
    {
        var dto = GetValidUpdateDto();

        var existing = new Host
        {
            Id = 1,
            FirstName = "Old",
            LastName = "Value",
            Email = "old@example.com"
        };

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(existing);
        _repo.Setup(r => r.UpdateAsync(It.IsAny<long>(), It.IsAny<Host>())).ReturnsAsync(existing);

        var service = new HostServiceWithValidation(_repo.Object);

        var result = await service.UpdateAsync(1, dto);

        result.Should().NotBeNull();
        result.FirstName.Should().Be(dto.FirstName);
        result.PhoneNumber.Should().Be(dto.PhoneNumber);
    }
}
