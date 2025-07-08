using FluentAssertions;
using Moq;
using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.UnitTests.HostServiceTests;

public class GetByIdAsyncTests
{
    private readonly Mock<IGenericRepository<Host>> _repo;

    public GetByIdAsyncTests() =>
        _repo = new Mock<IGenericRepository<Host>>();

    [Fact]
    public async Task GetByIdAsync_ShouldReturnHostDto_WhenHostExists()
    {
        var host = new Host
        {
            Id = 1,
            FirstName = "Ali",
            LastName = "Valiyev",
            DateOfBirth = new DateTime(1990, 5, 10),
            Email = "ali@example.com",
            PhoneNumber = "998901234567",
            Gender = Gender.Male
        };

        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(host);
        var service = new HostService(_repo.Object);

        var result = await service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.FirstName.Should().Be("Ali");
        result.LastName.Should().Be("Valiyev");
        result.Email.Should().Be("ali@example.com");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrow_WhenHostNotFound()
    {
        _repo.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((Host?)null);
        var service = new HostService(_repo.Object);

        var act = async () => await service.GetByIdAsync(999);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("Host with ID 999 not found.");
    }
}
