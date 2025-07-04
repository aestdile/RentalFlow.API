using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HomeDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.HomeServiceTests;

public class UpdateAsyncTests
{
    private readonly Mock<IGenericRepository<Home>> _repo;

    public UpdateAsyncTests()
    {
        _repo = new Mock<IGenericRepository<Home>>();
    }

    private HomeUpdateDto GetValidDto() => new HomeUpdateDto
    {
        Address = "Tashkent, Yunusobod",
        Description = "Modern home with 3 bedrooms and nice backyard.",
        IsAvailable = true,
        NoofBedRooms = 3,
        NoofBathRooms = 2,
        Area = 150.5,
        IsPetAllowed = true,
        HomeType = HomeType.Flat,
        Price = 2000.00m
    };

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task UpdateAsync_ShouldThrow_WhenAddressIsInvalid(string address)
    {
        var dto = GetValidDto();
        dto.Address = address;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Home());

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Address*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("short")]
    public async Task UpdateAsync_ShouldThrow_WhenDescriptionTooShort(string desc)
    {
        var dto = GetValidDto();
        dto.Description = desc;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Home());

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.UpdateAsync(1, dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Description*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    public async Task UpdateAsync_ShouldThrow_WhenRoomsAreNegative(int count)
    {
        var dto = GetValidDto();
        dto.NoofBedRooms = count;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Home());
        var service = new HomeServiceWithValidation(_repo.Object);

        var act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Room*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public async Task UpdateAsync_ShouldThrow_WhenAreaInvalid(double area)
    {
        var dto = GetValidDto();
        dto.Area = area;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Home());
        var service = new HomeServiceWithValidation(_repo.Object);

        var act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Area*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public async Task UpdateAsync_ShouldThrow_WhenPriceInvalid(decimal price)
    {
        var dto = GetValidDto();
        dto.Price = price;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Home());
        var service = new HomeServiceWithValidation(_repo.Object);

        var act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Price*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public async Task UpdateAsync_ShouldThrow_WhenHomeTypeInvalid(int type)
    {
        var dto = GetValidDto();
        dto.HomeType = (HomeType)type;

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new Home());
        var service = new HomeServiceWithValidation(_repo.Object);

        var act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentOutOfRangeException>().WithMessage("*HomeType*");
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenAllValid()
    {
        var dto = GetValidDto();
        var existingHome = new Home { Id = 1, HostId = 10 };

        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(existingHome);
        _repo.Setup(r => r.UpdateAsync(It.IsAny<long>(), It.IsAny<Home>())).ReturnsAsync(existingHome);

        var service = new HomeServiceWithValidation(_repo.Object);
        var result = await service.UpdateAsync(1, dto);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Address.Should().Be(dto.Address);
        result.Price.Should().Be(dto.Price);
    }
}
