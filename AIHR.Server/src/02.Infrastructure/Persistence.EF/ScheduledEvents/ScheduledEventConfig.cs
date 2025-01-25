using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIHR.EventScheduler.Persistence.EF.ScheduledEvents;

public class ScheduledEventConfig : IEntityTypeConfiguration<ScheduledEvent>
{
    public void Configure(EntityTypeBuilder<ScheduledEvent> _)
    {
        _.HasKey(e => e.Id);
        _.Property(e => e.Id);
        _.Property(e => e.Title).HasMaxLength(70);
        _.Property(e => e.Description).HasMaxLength(250);
        _.ComplexProperty(e => e.DateRange).IsRequired();
        _.Property(e => e.UserId).IsRequired();
    }
}