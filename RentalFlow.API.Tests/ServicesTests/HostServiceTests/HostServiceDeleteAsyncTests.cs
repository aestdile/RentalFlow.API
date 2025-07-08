using FluentAssertions;
using Moq;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.UnitTests.HostServiceTests;

public class DeleteAsyncTests
{
    private readonly Mock<IGenericRepository<Host>> _repo;

    public DeleteAsyncTests()
    {
        _repo = new Mock<IGenericRepository<Host>>();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnDeletedCount_WhenDeletionIsSuccessful()
    {
        long hostId = 1;

        var fakeHost = new Host
        {
            Id = hostId,
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Password = "123456",
            PhoneNumber = "+998901112233",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = RentalFlow.API.Domain.Enums.Gender.Male
        };

        _repo.Setup(r => r.GetByIdAsync(hostId)).ReturnsAsync(fakeHost); 
        _repo.Setup(r => r.DeleteAsync(hostId)).ReturnsAsync(1L);

        var service = new HostService(_repo.Object);

        var result = await service.DeleteAsync(hostId);

        result.Should().Be(1L);
        _repo.Verify(r => r.GetByIdAsync(hostId), Times.Once);
        _repo.Verify(r => r.DeleteAsync(hostId), Times.Once);
    }


    [Fact]
    public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenHostNotFound()
    {
        long hostId = 999;
        _repo.Setup(r => r.GetByIdAsync(hostId)).ReturnsAsync((Host?)null); 

        var service = new HostService(_repo.Object);

        var act = async () => await service.DeleteAsync(hostId);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Host with ID {hostId} not found.");
    }

}
