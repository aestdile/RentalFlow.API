namespace RentalFlow.API.Domain.Common;

public abstract class BaseEntity<Tid>
{
    public Tid Id { get; set; } = default!;
}
