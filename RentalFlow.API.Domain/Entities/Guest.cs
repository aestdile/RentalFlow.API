using RentalFlow.API.Domain.Common;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.API.Domain.Entities;

public class Guest : AuditableEntity<long>
{
    public long Id { get; set; } = default!; // Explicitly defining Id for clarity, even though AuditableEntity already has it.
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public ICollection<HomeRequest> HomeRequests { get; set; } = new List<HomeRequest>();
}
