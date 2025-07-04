using RentalFlow.API.Application.DTOs.HomeRequestDTOs;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;


public class HomeRequestServiceWithValidation : HomeRequestService
{
    public HomeRequestServiceWithValidation(IGenericRepository<HomeRequest> repo)
        : base(repo) { }

    public new async Task<HomeRequestDto> CreateAsync(HomeRequestCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (dto.GuestId <= 0)
            throw new ArgumentException("GuestId must be positive.", nameof(dto.GuestId));

        if (dto.HomeId <= 0)
            throw new ArgumentException("HomeId must be positive.", nameof(dto.HomeId));

        if (string.IsNullOrWhiteSpace(dto.RequestMessage))
            throw new ArgumentException("RequestMessage cannot be empty.", nameof(dto.RequestMessage));

        if (dto.RequestMessage.Length > 500)
            throw new ArgumentException("RequestMessage too long.", nameof(dto.RequestMessage));

        if (dto.StartDate.Date < DateTime.Today)
            throw new ArgumentException("StartDate cannot be in the past.", nameof(dto.StartDate));

        if (dto.EndDate <= dto.StartDate)
            throw new ArgumentException("EndDate must be after StartDate.", nameof(dto.EndDate));

        return await base.CreateAsync(dto);
    }

    public new async Task<HomeRequestDto> UpdateAsync(long id, HomeRequestUpdateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.RequestMessage))
            throw new ArgumentException("RequestMessage cannot be empty.", nameof(dto.RequestMessage));

        if (dto.RequestMessage.Length > 1000)
            throw new ArgumentException("RequestMessage is too long (max 1000).", nameof(dto.RequestMessage));

        if (dto.StartDate < DateTime.Today)
            throw new ArgumentException("StartDate cannot be in the past.", nameof(dto.StartDate));

        if (dto.EndDate <= dto.StartDate)
            throw new ArgumentException("EndDate must be after StartDate.", nameof(dto.EndDate));

        return await base.UpdateAsync(id, dto);
    }

}

