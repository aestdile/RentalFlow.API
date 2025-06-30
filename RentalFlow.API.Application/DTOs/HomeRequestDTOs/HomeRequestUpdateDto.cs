namespace RentalFlow.API.Application.DTOs.HomeRequestDTOs;

public class HomeRequestUpdateDto
{
    public string RequestMessage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
