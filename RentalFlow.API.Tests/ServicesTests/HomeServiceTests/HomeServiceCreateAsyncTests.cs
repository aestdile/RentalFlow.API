using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HomeDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.HomeServiceTests;

public class CreateAsyncTests
{
    private readonly Mock<IGenericRepository<Home>> _repo;

    public CreateAsyncTests()
    {
        _repo = new Mock<IGenericRepository<Home>>();
    }

    private HomeCreateDto GetValidDto() => new HomeCreateDto
    {
        HostId = 1,
        Address = "Tashkent, Yunusobod",
        Description = "Spacious home with garden",
        IsAvailable = true,
        NoofBedRooms = 3,
        NoofBathRooms = 2,
        Area = 120.5,
        IsPetAllowed = true,
        HomeType = HomeType.Flat,
        Price = 1500.0m
    };

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task CreateAsync_ShouldThrow_WhenHostIdIsInvalid(long hostId)
    {
        var dto = GetValidDto();
        dto.HostId = hostId;

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*HostId*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CreateAsync_ShouldThrow_WhenAddressIsInvalid(string address)
    {
        var dto = GetValidDto();
        dto.Address = address;

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Address*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("Short")]
    public async Task CreateAsync_ShouldThrow_WhenDescriptionIsTooShort(string description)
    {
        var dto = GetValidDto();
        dto.Description = description;

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Description*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task CreateAsync_ShouldThrow_WhenRoomCountsAreNegative(int count)
    {
        var dto = GetValidDto();
        dto.NoofBedRooms = count;

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*rooms*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-25)]
    public async Task CreateAsync_ShouldThrow_WhenAreaIsInvalid(double area)
    {
        var dto = GetValidDto();
        dto.Area = area;

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Area*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public async Task CreateAsync_ShouldThrow_WhenPriceIsInvalid(decimal price)
    {
        var dto = GetValidDto();
        dto.Price = price;

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Price*");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(100)]
    public async Task CreateAsync_ShouldThrow_WhenHomeTypeIsInvalid(int type)
    {
        var dto = GetValidDto();
        dto.HomeType = (HomeType)type;

        var service = new HomeServiceWithValidation(_repo.Object);
        var act = () => service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentOutOfRangeException>().WithMessage("*HomeType*");
    }

    [Fact]
    public async Task CreateAsync_ShouldSucceed_WhenAllDataValid()
    {
        var dto = GetValidDto();

        _repo.Setup(r => r.CreateAsync(It.IsAny<Home>())).ReturnsAsync(new Home
        {
            Id = 1,
            HostId = dto.HostId,
            Address = dto.Address,
            Description = dto.Description,
            IsAvailable = dto.IsAvailable,
            NoofBedRooms = dto.NoofBedRooms,
            NoofBathRooms = dto.NoofBathRooms,
            Area = dto.Area,
            IsPetAllowed = dto.IsPetAllowed,
            HomeType = dto.HomeType,
            Price = dto.Price
        });

        var service = new HomeServiceWithValidation(_repo.Object);

        var result = await service.CreateAsync(dto);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Address.Should().Be(dto.Address);
        result.Price.Should().Be(dto.Price);
    }
}
