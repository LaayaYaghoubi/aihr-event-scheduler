using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using Microsoft.EntityFrameworkCore;

namespace AIHR.EventScheduler.Persistence.EF.ScheduledEvents;

public class EfScheduledEventRepository : IScheduledEventRepository
{
    private readonly DbSet<ScheduledEvent> _scheduledEvents;

    public EfScheduledEventRepository(EfDataContext context)
    {
        _scheduledEvents = context.Set<ScheduledEvent>();
    }

    public void Add(ScheduledEvent scheduledEvent)
    {
        _scheduledEvents.Add(scheduledEvent);
    }

    public async Task<ScheduledEvent?> FindById(int id)
    {
        return await _scheduledEvents.FindAsync(id);
    }
}