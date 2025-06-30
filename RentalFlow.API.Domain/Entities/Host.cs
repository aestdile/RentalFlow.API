using RentalFlow.API.Domain.Common;
using RentalFlow.API.Domain.Enums;

namespace RentalFlow.API.Domain.Entities;

public class Host : AuditableEntity<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public ICollection<Home> Homes { get; set; } = new List<Home>();
}
