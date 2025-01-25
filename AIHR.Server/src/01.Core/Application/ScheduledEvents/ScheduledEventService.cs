using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Application.ScheduledEvents.Exceptions;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventSchedulerInfrastructure.Helper;

namespace AIHR.EventScheduler.Application.ScheduledEvents;

public class ScheduledEventService(
    IScheduledEventRepository repository,
    IUnitOfWork unitOfWork,
    IUserService userService,
    IDateTimeService dateTimeService)
    : IScheduledEventService
{
    public async Task<ScheduledEvent> AddAsync(AddScheduledEventDto dto)
    {
        ThrowIfStartIsBeforeNow(dto.Start);
        
        var userId = userService.GetUserId();
        var scheduledEvent = new ScheduledEvent
        {
            Title = dto.Title,
            Description = dto.Description,
            DateRange = new DateRange(dto.Start, dto.End),
            UserId = userId
        };

        repository.Add(scheduledEvent);
        await unitOfWork.Complete();
        return scheduledEvent;
    }
    
    public async Task UpdateAsync(int id, UpdateScheduledEventDto dto)
    {
        var scheduledEvent = await ThrowIfScheduledEventNotFound(id);
        ThrowIfStartIsBeforeNow(dto.Start);

        scheduledEvent!.Description = dto.Description;
        scheduledEvent.Title = dto.Title;
        scheduledEvent.DateRange = new DateRange(dto.Start, dto.End);

        await unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var scheduledEvent = await ThrowIfScheduledEventNotFound(id);

        repository.Delete(scheduledEvent!);
        await unitOfWork.Complete();
    }

    public async Task<GetScheduledEventDto?> GetByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<PagedList<GetScheduledEventDto>> GetAllAsync(SortOrder sortOrder, Pagination pagination)
    {
        var userId = userService.GetUserId();
        return await repository.GetAll(userId,sortOrder, pagination);
    }

    private async Task<ScheduledEvent?> ThrowIfScheduledEventNotFound(int id)
    {
        var scheduledEvent = await repository.FindById(id);
        if (scheduledEvent == null)
            throw new ScheduledEventNotFoundException();
        return scheduledEvent;
    }
    
    private void ThrowIfStartIsBeforeNow(DateTime start)
    {
        if (start <= dateTimeService.Now)
            throw new ScheduledEventCanNotBeInPastException();
    }
}