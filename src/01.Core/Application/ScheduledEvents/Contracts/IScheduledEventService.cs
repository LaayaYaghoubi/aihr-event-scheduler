using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;

namespace AIHR.EventScheduler.Application.ScheduledEvents.Contracts;

public interface IScheduledEventService : IService
{
    Task<ScheduledEvent> AddAsync(AddScheduledEventDto dto);
    Task UpdateAsync(int id, UpdateScheduledEventDto dto);
    Task Delete(int id);
    Task<GetScheduledEventDto?> GetByIdAsync(int id);
}