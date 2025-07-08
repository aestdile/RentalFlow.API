using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.HomeRequestTests;

public class GetByIdAsyncTests
{
    private readonly Mock<IGenericRepository<HomeRequest>> _repo;

    public GetByIdAsyncTests()
    {
        _repo = new Mock<IGenericRepository<HomeRequest>>();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDto_WhenEntityExists()
    {
        var id = 1;
        var homeRequest = new HomeRequest
        {
            Id = id,
            GuestId = 10,
            HomeId = 20,
            RequestMessage = "Need home for vacation",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(3),
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow
        };

        _repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(homeRequest);
        var service = new HomeRequestService(_repo.Object);

        var result = await service.GetByIdAsync(id);

        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        result.RequestMessage.Should().Be("Need home for vacation");
        result.StartDate.Should().Be(homeRequest.StartDate);
        result.EndDate.Should().Be(homeRequest.EndDate);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrow_WhenEntityNotFound()
    {
        _repo
            .Setup(repo => repo.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync((HomeRequest)null);

        var service = new HomeRequestService(_repo.Object);

        Func<Task> act = async () => await service.GetByIdAsync(42);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("HomeRequest with ID 42 not found.");
    }
}
