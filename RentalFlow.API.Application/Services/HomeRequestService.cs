using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.Interfaces.IServices;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.API.Application.Services;

public class HomeRequestService : IHomeRequestService
{
    private readonly IGenericRepository<HomeRequest> _homeRequestRepository;
    public HomeRequestService(IGenericRepository<HomeRequest> homeRequestRepository)
    {
        _homeRequestRepository = homeRequestRepository;
    }
    public async Task<HomeRequestDto> CreateAsync(HomeRequestCreateDto homeRequestCreateDto)
    {
        var homeRequestEntity = new HomeRequest
        {
            GuestId = homeRequestCreateDto.GuestId,
            HomeId = homeRequestCreateDto.HomeId,
            RequestMessage = homeRequestCreateDto.RequestMessage,
            StartDate = homeRequestCreateDto.StartDate,
            EndDate = homeRequestCreateDto.EndDate
        };

        var createdHomeRequest = await _homeRequestRepository.CreateAsync(homeRequestEntity);

        return new HomeRequestDto
        {
            Id = createdHomeRequest.Id,
            GuestId = createdHomeRequest.GuestId,
            HomeId = createdHomeRequest.HomeId,
            RequestMessage = createdHomeRequest.RequestMessage,
            StartDate = createdHomeRequest.StartDate,
            EndDate = createdHomeRequest.EndDate,
            CreatedAt = createdHomeRequest.CreatedAt,
            UpdatedAt = createdHomeRequest.UpdatedAt
        };
    }

    public Task<long> DeleteAsync(long id)
    {
        return _homeRequestRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<HomeRequestDto>> GetAllAsync()
    {
        var homeRequests = await _homeRequestRepository.GetAllAsync();
        return homeRequests.Select(homeRequest => new HomeRequestDto
        {
            Id = homeRequest.Id,
            GuestId = homeRequest.GuestId,
            HomeId = homeRequest.HomeId,
            RequestMessage = homeRequest.RequestMessage,
            StartDate = homeRequest.StartDate,
            EndDate = homeRequest.EndDate,
            CreatedAt = homeRequest.CreatedAt,
            UpdatedAt = homeRequest.UpdatedAt
        }).ToList();
    }

    public async Task<HomeRequestDto> GetByIdAsync(long id)
    {
        var request = await _homeRequestRepository.GetByIdAsync(id);
        if (request == null)
        {
            throw new KeyNotFoundException($"HomeRequest with ID {id} not found.");
        }

        return new HomeRequestDto
        {
            Id = request.Id,
            GuestId = request.GuestId,
            HomeId = request.HomeId,
            RequestMessage = request.RequestMessage,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
    }


    public async Task<HomeRequestDto> UpdateAsync(long id, HomeRequestUpdateDto homeRequestUpdateDto)
    {
        var homeRequest = await _homeRequestRepository.GetByIdAsync(id);
        if (homeRequest == null)
        {
            throw new KeyNotFoundException($"HomeRequest with ID {id} not found.");
        }

        homeRequest.RequestMessage = homeRequestUpdateDto.RequestMessage;
        homeRequest.StartDate = homeRequestUpdateDto.StartDate;
        homeRequest.EndDate = homeRequestUpdateDto.EndDate;
        homeRequest.UpdatedAt = DateTime.UtcNow;

        var updatedHomeRequest = await _homeRequestRepository.UpdateAsync(id, homeRequest);

        return new HomeRequestDto
        {
            Id = updatedHomeRequest.Id,
            GuestId = updatedHomeRequest.GuestId,
            HomeId = updatedHomeRequest.HomeId,
            RequestMessage = updatedHomeRequest.RequestMessage,
            StartDate = updatedHomeRequest.StartDate,
            EndDate = updatedHomeRequest.EndDate,
            CreatedAt = updatedHomeRequest.CreatedAt,
            UpdatedAt = updatedHomeRequest.UpdatedAt
        };
    }
}
