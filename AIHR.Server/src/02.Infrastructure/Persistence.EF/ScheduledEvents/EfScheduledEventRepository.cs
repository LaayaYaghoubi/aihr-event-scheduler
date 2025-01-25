using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventSchedulerInfrastructure.Helper;
using Microsoft.EntityFrameworkCore;

namespace AIHR.EventScheduler.Persistence.EF.ScheduledEvents;

public class EfScheduledEventRepository(EfDataContext context) : IScheduledEventRepository
{
    private readonly DbSet<ScheduledEvent> _scheduledEvents = context.Set<ScheduledEvent>();

    public void Add(ScheduledEvent scheduledEvent)
    {
        _scheduledEvents.Add(scheduledEvent);
    }

    public async Task<ScheduledEvent?> FindById(int id)
    {
        return await _scheduledEvents.FindAsync(id);
    }

    public void Delete(ScheduledEvent scheduledEvent)
    {
        _scheduledEvents.Remove(scheduledEvent);
    }

    public async Task<GetScheduledEventDto?> GetByIdAsync(int id)
    {
        return await _scheduledEvents
            .Where(scheduledEvent => scheduledEvent.Id == id)
            .Select(scheduledEvent => new GetScheduledEventDto(
                scheduledEvent.Id,
                scheduledEvent.Title,
                scheduledEvent.Description,
                scheduledEvent.DateRange.Start,
                scheduledEvent.DateRange.End))
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<GetScheduledEventDto>> GetAll(string? userId, SortOrder sortOrder,
        Pagination pagination)
    {
        var query = sortOrder == SortOrder.Descending
            ? _scheduledEvents.OrderByDescending(e => e.DateRange.Start).Where(e=>e.UserId == userId)
            : _scheduledEvents.OrderBy(e => e.DateRange.Start).Where(e=>e.UserId == userId);

        var dtoQuery = query.Select(e => new GetScheduledEventDto(
            e.Id,
            e.Title,
            e.Description,
            e.DateRange.Start,
            e.DateRange.End));

        return await PagedList<GetScheduledEventDto>.CreateAsync(dtoQuery, pagination);
    }
}