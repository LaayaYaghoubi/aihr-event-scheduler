using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;

namespace AIHR.EventScheduler.Application.ScheduledEvents.Contracts;

public interface IScheduledEventRepository : IRepository
{
    void Add(ScheduledEvent scheduledEvent);
}