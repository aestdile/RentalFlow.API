namespace RentalFlow.API.Domain.Common;

public abstract class AuditableEntity<Tid> : BaseEntity<Tid>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? UpdatedBy { get; set; }
}

