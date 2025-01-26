using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventSchedulerInfrastructure.Helper;

namespace AIHR.EventScheduler.Application.ScheduledEvents.Contracts;

public interface IScheduledEventService : IService
{
    Task<ScheduledEvent> AddAsync(AddScheduledEventDto dto);
    Task UpdateAsync(int id, UpdateScheduledEventDto dto);
    Task Delete(int id);
    Task<GetScheduledEventDto?> GetByIdAsync(int id);
    Task<PagedList<GetScheduledEventDto>> GetAllAsync(SortOrder sortOrder, Pagination pagination);
    Task Notified(int eventId);
}