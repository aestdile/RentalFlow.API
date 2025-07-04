using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.API.Tests.ServicesTests.HomeRequestServiceTests;

public class HomeRequestCreateAsyncTests
{
    private readonly Mock<IGenericRepository<HomeRequest>> _repoMock;

    public HomeRequestCreateAsyncTests()
    {
        _repoMock = new Mock<IGenericRepository<HomeRequest>>();
    }

    private HomeRequestCreateDto ValidDto => new()
    {
        GuestId = 1,
        HomeId = 2,
        RequestMessage = "Valid message",
        StartDate = DateTime.Today.AddDays(1),
        EndDate = DateTime.Today.AddDays(5)
    };

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CreateAsync_ShouldThrow_WhenGuestIdInvalid(int guestId)
    {
        var dto = ValidDto;
        dto.GuestId = guestId;
        var svc = new HomeRequestServiceWithValidation(_repoMock.Object);
        Func<Task> act = () => svc.CreateAsync(dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*GuestId*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task CreateAsync_ShouldThrow_WhenHomeIdInvalid(int homeId)
    {
        var dto = ValidDto;
        dto.HomeId = homeId;
        var svc = new HomeRequestServiceWithValidation(_repoMock.Object);
        Func<Task> act = () => svc.CreateAsync(dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*HomeId*");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task CreateAsync_ShouldThrow_WhenRequestMessageEmpty(string msg)
    {
        var dto = ValidDto;
        dto.RequestMessage = msg;
        var svc = new HomeRequestServiceWithValidation(_repoMock.Object);
        Func<Task> act = () => svc.CreateAsync(dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*RequestMessage*");
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenMessageTooLong()
    {
        var dto = ValidDto;
        dto.RequestMessage = new string('a', 501);
        var svc = new HomeRequestServiceWithValidation(_repoMock.Object);
        Func<Task> act = () => svc.CreateAsync(dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*RequestMessage*");
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenStartDateIsPast()
    {
        var dto = ValidDto;
        dto.StartDate = DateTime.Today.AddDays(-1);
        var svc = new HomeRequestServiceWithValidation(_repoMock.Object);
        Func<Task> act = () => svc.CreateAsync(dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*StartDate*");
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenEndDateBeforeStartDate()
    {
        var dto = ValidDto;
        dto.EndDate = dto.StartDate;
        var svc = new HomeRequestServiceWithValidation(_repoMock.Object);
        Func<Task> act = () => svc.CreateAsync(dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*EndDate*");
    }

    [Fact]
    public async Task CreateAsync_ShouldSucceed_WhenAllFieldsValid()
    {
        var dto = ValidDto;
        _repoMock.Setup(r => r.CreateAsync(It.IsAny<HomeRequest>()))
            .ReturnsAsync(new HomeRequest
            {
                Id = 1,
                GuestId = dto.GuestId,
                HomeId = dto.HomeId,
                RequestMessage = dto.RequestMessage,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

        var svc = new HomeRequestServiceWithValidation(_repoMock.Object);
        var result = await svc.CreateAsync(dto);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
    }
}

