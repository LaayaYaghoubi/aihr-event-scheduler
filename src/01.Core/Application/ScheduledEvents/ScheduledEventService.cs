using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Application.ScheduledEvents.Exceptions;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;

namespace AIHR.EventScheduler.Application.ScheduledEvents;

public class ScheduledEventService : IScheduledEventService
{
    private readonly IScheduledEventRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduledEventService(
        IScheduledEventRepository repository,
        IUnitOfWork unitOfWork
        )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ScheduledEvent> AddAsync(AddScheduledEventDto dto)
    {
        var scheduledEvent = new ScheduledEvent
        {
            Title = dto.Title,
            Description = dto.Description,
            DateRange = new DateRange(dto.Start,dto.End)
        };
        
        _repository.Add(scheduledEvent);
        await _unitOfWork.Complete();
        return scheduledEvent;
    }

    public async Task UpdateAsync(int scheduledEventId, UpdateScheduledEventDto dto)
    {
        var scheduledEvent = await ThrowIfScheduledEventNotFound(scheduledEventId);

        scheduledEvent!.Description = dto.Description;
        scheduledEvent.Title = dto.Title;
        scheduledEvent.DateRange = new DateRange(dto.Start, dto.End);
        
        await _unitOfWork.Complete();
    }

    private async Task<ScheduledEvent?> ThrowIfScheduledEventNotFound(int scheduledEventId)
    {
        var scheduledEvent = await _repository.FindById(scheduledEventId);
        if (scheduledEvent == null)
            throw new ScheduledEventNotFoundException();
        return scheduledEvent;
    }
}