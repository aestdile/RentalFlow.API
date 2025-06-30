using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalFlow.API.Application.DTOs.HomeRequestDTOs;

public class HomeRequestDto
{
    public long Id { get; set; }
    public long GuestId { get; set; }
    public long HomeId { get; set; }
    public string RequestMessage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
