using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.HomeRequestTests;

public class HomeRequestUpdateAsyncTests
{
    private readonly Mock<IGenericRepository<HomeRequest>> _repo;

    public HomeRequestUpdateAsyncTests() => _repo = new Mock<IGenericRepository<HomeRequest>>();

    private HomeRequestUpdateDto GetValidDto() => new HomeRequestUpdateDto
    {
        RequestMessage = "I want to book this home for vacation.",
        StartDate = DateTime.Today.AddDays(1),
        EndDate = DateTime.Today.AddDays(5)
    };

    private HomeRequest GetExistingEntity() => new HomeRequest
    {
        Id = 1,
        GuestId = 1,
        HomeId = 1,
        RequestMessage = "Old",
        StartDate = DateTime.Today,
        EndDate = DateTime.Today.AddDays(2),
        CreatedAt = DateTime.UtcNow.AddDays(-3),
        UpdatedAt = DateTime.UtcNow.AddDays(-3)
    };

    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenNotFound()
    {
        _repo
            .Setup(repo => repo.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync((HomeRequest)null);

        var service = new HomeRequestService(_repo.Object);

        var updateDto = new HomeRequestUpdateDto
        {
            RequestMessage = "Updated",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(3)
        };

        Func<Task> act = async () => await service.UpdateAsync(9999, updateDto);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("HomeRequest with ID 9999 not found.");
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task UpdateAsync_ShouldThrow_WhenRequestMessageIsEmpty(string msg)
    {
        var dto = GetValidDto();
        dto.RequestMessage = msg;

        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(GetExistingEntity());
        var service = new HomeRequestServiceWithValidation(_repo.Object);

        Func<Task> act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*RequestMessage*");
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenRequestMessageIsTooLong()
    {
        var dto = GetValidDto();
        dto.RequestMessage = new string('A', 1001);

        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(GetExistingEntity());
        var service = new HomeRequestServiceWithValidation(_repo.Object);

        Func<Task> act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*RequestMessage*");
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenStartDateIsInPast()
    {
        var dto = GetValidDto();
        dto.StartDate = DateTime.Today.AddDays(-1);

        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(GetExistingEntity());
        var service = new HomeRequestServiceWithValidation(_repo.Object);

        Func<Task> act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*StartDate*");
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenEndDateBeforeStartDate()
    {
        var dto = GetValidDto();
        dto.EndDate = dto.StartDate.AddDays(-1);

        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(GetExistingEntity());
        var service = new HomeRequestServiceWithValidation(_repo.Object);

        Func<Task> act = () => service.UpdateAsync(1, dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*EndDate*");
    }

    [Fact]
    public async Task UpdateAsync_ShouldSucceed_WhenValid()
    {
        var dto = GetValidDto();

        _repo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(GetExistingEntity());
        _repo.Setup(x => x.UpdateAsync(It.IsAny<long>(), It.IsAny<HomeRequest>()))
             .ReturnsAsync((long id, HomeRequest hr) => hr);

        var service = new HomeRequestServiceWithValidation(_repo.Object);

        var result = await service.UpdateAsync(1, dto);

        result.Should().NotBeNull();
        result.RequestMessage.Should().Be(dto.RequestMessage);
        result.StartDate.Should().Be(dto.StartDate);
        result.EndDate.Should().Be(dto.EndDate);
    }
}
