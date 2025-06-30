namespace RentalFlow.API.Application.DTOs.HomeRequestDTOs;

public class HomeRequestCreateDto
{
    public long GuestId { get; set; }
    public long HomeId { get; set; }
    public string RequestMessage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
