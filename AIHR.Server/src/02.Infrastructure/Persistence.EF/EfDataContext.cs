using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AIHR.EventScheduler.Persistence.EF;

public class EfDataContext : IdentityDbContext<ApplicationUser>
{
    public EfDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ScheduledEvent> ScheduledEvents { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly
            (typeof(EfDataContext).Assembly);
    }
}