using Microsoft.EntityFrameworkCore;
using RentalFlow.API.Domain.Entities;

namespace RentalFlow.API.Infrastructure.Persistance;

public class RentalFlowDbContext : DbContext
{
    public RentalFlowDbContext(DbContextOptions<RentalFlowDbContext> options)
        : base(options){}

    public DbSet<Home> Homes { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Host> Hosts { get; set; }
    public DbSet<HomeRequest> HomeRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<HomeRequest>()
            .HasOne(hr => hr.Home)
            .WithMany(h => h.HomeRequests)
            .HasForeignKey(hr => hr.HomeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HomeRequest>()
            .HasOne(hr => hr.Guest)
            .WithMany(g => g.HomeRequests)
            .HasForeignKey(hr => hr.GuestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Home>(entity =>
        {
            entity.Property(e => e.Price)
                  .HasPrecision(18, 2);
        });

        base.OnModelCreating(modelBuilder);
    }

}
