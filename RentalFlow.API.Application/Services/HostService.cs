using RentalFlow.API.Application.DTOs.HostDTOs;
using RentalFlow.API.Application.Interfaces.IServices;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.API.Application.Services;

public class HostService : IHostService
{
    private readonly IGenericRepository<Host> _hostRepository;
    public HostService(IGenericRepository<Host> hostRepository)
    {
        _hostRepository = hostRepository;
    }
    public async Task<HostDto> CreateAsync(HostCreateDto hostCreateDto)
    {
        var host = new Host
        {
            FirstName = hostCreateDto.FirstName,
            LastName = hostCreateDto.LastName,
            DateOfBirth = hostCreateDto.DateOfBirth,
            Email = hostCreateDto.Email,
            Password = hostCreateDto.Password,
            PhoneNumber = hostCreateDto.PhoneNumber,
            Gender = hostCreateDto.Gender,
        };

        var createdHost = await _hostRepository.CreateAsync(host);
        var result = new HostDto
        {
            Id = createdHost.Id,
            FirstName = createdHost.FirstName,
            LastName = createdHost.LastName,
            DateOfBirth = createdHost.DateOfBirth,
            Email = createdHost.Email,
            PhoneNumber = createdHost.PhoneNumber,
            Gender = createdHost.Gender,
        };

        return result;
    }

    public Task<long> DeleteAsync(long id)
    {
        return _hostRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<HostDto>> GetAllAsync()
    {
        var hosts = await _hostRepository.GetAllAsync();
        return hosts.Select(host => new HostDto
        {
            Id = host.Id,
            FirstName = host.FirstName,
            LastName = host.LastName,
            DateOfBirth = host.DateOfBirth,
            Email = host.Email,
            PhoneNumber = host.PhoneNumber,
            Gender = host.Gender,
        }).ToList();
    }

    public async Task<HostDto> GetByIdAsync(long id)
    {
        var host = await _hostRepository.GetByIdAsync(id);
        if (host == null)
            throw new KeyNotFoundException($"Host with ID {id} not found.");

        var hostDto = new HostDto
        {
            Id = host.Id,
            FirstName = host.FirstName,
            LastName = host.LastName,
            DateOfBirth = host.DateOfBirth,
            Email = host.Email,
            PhoneNumber = host.PhoneNumber,
            Gender = host.Gender,
        };

        return hostDto;
    }

    public async Task<HostDto> UpdateAsync(long id, HostUpdateDto hostUpdateDto)
    {
        var host = await _hostRepository.GetByIdAsync(id);
        if (host == null)
        {
            throw new KeyNotFoundException($"Host with ID {id} not found.");
        }

        host.FirstName = hostUpdateDto.FirstName;
        host.LastName = hostUpdateDto.LastName;
        host.DateOfBirth = hostUpdateDto.DateOfBirth;
        host.PhoneNumber = hostUpdateDto.PhoneNumber;
        host.Gender = hostUpdateDto.Gender;

        await _hostRepository.UpdateAsync(id, host);

        return new HostDto
        {
            Id = host.Id,
            FirstName = host.FirstName,
            LastName = host.LastName,
            DateOfBirth = host.DateOfBirth,
            Email = host.Email,
            PhoneNumber = host.PhoneNumber,
            Gender = host.Gender,
        };
    }
}
