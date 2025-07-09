using RentalFlow.API.Domain.Common;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.API.Application.DTOs.HomeDTOs;

public class HomeDto : BaseEntity<long>
{
    public long HostId { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    public int NoofBedRooms { get; set; }
    public int NoofBathRooms { get; set; }
    public double Area { get; set; }
    public bool IsPetAllowed { get; set; }
    public HomeType HomeType { get; set; }
    public decimal Price { get; set; }
}
