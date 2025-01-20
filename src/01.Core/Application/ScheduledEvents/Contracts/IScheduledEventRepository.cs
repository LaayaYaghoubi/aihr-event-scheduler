using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;

namespace AIHR.EventScheduler.Application.ScheduledEvents.Contracts;

public interface IScheduledEventRepository : IRepository
{
    void Add(ScheduledEvent scheduledEvent);
    Task<ScheduledEvent?> FindById(int id);
    void Delete(ScheduledEvent scheduledEvent);
    Task<GetScheduledEventDto?> GetByIdAsync(int id);
}