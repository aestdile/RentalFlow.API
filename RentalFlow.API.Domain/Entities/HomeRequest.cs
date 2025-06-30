using RentalFlow.API.Domain.Common;

namespace RentalFlow.API.Domain.Entities;

public class HomeRequest : BaseEntity<long>
{
    public long GuestId { get; set; }
    public Guest Guest { get; set; }
    public long HomeId { get; set; }
    public Home Home { get; set; }
    public string RequestMessage { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
