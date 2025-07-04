using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.HostServiceTests;

public class CreateAsyncTests
{
    private readonly Mock<IGenericRepository<Host>> _hostRepo;

    public CreateAsyncTests() =>
        _hostRepo = new Mock<IGenericRepository<Host>>();

    private HostCreateDto GetValidHostDto() => new HostCreateDto
    {
        FirstName = "John",
        LastName = "Doe",
        DateOfBirth = new DateTime(1990, 1, 1),
        Email = "john.doe@example.com",
        Password = "Strong@123",
        PhoneNumber = "998901234567",
        Gender = Gender.Male
    };

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("john")]
    [InlineData("Jo123")]
    [InlineData("Jo@hn")]
    public async Task CreateAsync_ShouldThrow_WhenFirstNameInvalid(string firstName)
    {
        var dto = GetValidHostDto();
        dto.FirstName = firstName;

        var service = new HostServiceWithValidation(_hostRepo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*FirstName*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("doe")]
    [InlineData("Doe2")]
    [InlineData("Doe@")]
    public async Task CreateAsync_ShouldThrow_WhenLastNameInvalid(string lastName)
    {
        var dto = GetValidHostDto();
        dto.LastName = lastName;

        var service = new HostServiceWithValidation(_hostRepo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*LastName*");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    public async Task CreateAsync_ShouldThrow_WhenDateOfBirthIsFuture(int days)
    {
        var dto = GetValidHostDto();
        dto.DateOfBirth = DateTime.Today.AddDays(days);

        var service = new HostServiceWithValidation(_hostRepo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*DateOfBirth*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("plainaddress")]
    [InlineData("email.com")]
    [InlineData("john@.com")]
    public async Task CreateAsync_ShouldThrow_WhenEmailInvalid(string email)
    {
        var dto = GetValidHostDto();
        dto.Email = email;

        var service = new HostServiceWithValidation(_hostRepo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Email*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("password")]
    [InlineData("Password1")]
    [InlineData("password@1")]
    [InlineData("P@1")]
    public async Task CreateAsync_ShouldThrow_WhenPasswordWeak(string password)
    {
        var dto = GetValidHostDto();
        dto.Password = password;

        var service = new HostServiceWithValidation(_hostRepo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Password*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123abc")]
    [InlineData("99890")]
    [InlineData("99890123456789123")]
    public async Task CreateAsync_ShouldThrow_WhenPhoneNumberInvalid(string phone)
    {
        var dto = GetValidHostDto();
        dto.PhoneNumber = phone;

        var service = new HostServiceWithValidation(_hostRepo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*PhoneNumber*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(99)]
    public async Task CreateAsync_ShouldThrow_WhenGenderInvalid(int gender)
    {
        var dto = GetValidHostDto();
        dto.Gender = (Gender)gender;

        var service = new HostServiceWithValidation(_hostRepo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentOutOfRangeException>().WithMessage("*Gender*");
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnHostDto_WhenAllIsValid()
    {
        var dto = GetValidHostDto();

        _hostRepo.Setup(r => r.CreateAsync(It.IsAny<Host>())).ReturnsAsync(new Host
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

        var service = new HostServiceWithValidation(_hostRepo.Object);

        var result = await service.CreateAsync(dto);

        result.Should().NotBeNull();
        result.FirstName.Should().Be(dto.FirstName);
        result.Email.Should().Be(dto.Email);
    }
}
