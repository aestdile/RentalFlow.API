using RentalFlow.API.Domain.Common;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.API.Application.DTOs.GuestDTOs;

public class GuestUpdateDto : BaseEntity<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
}
