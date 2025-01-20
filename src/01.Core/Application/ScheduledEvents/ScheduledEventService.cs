using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
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

    public async Task<ScheduledEvent> AddAsync(AddScheduledEventDto addScheduledEventDto)
    {
        var scheduledEvent = new ScheduledEvent
        {
            Title = addScheduledEventDto.Title,
            Description = addScheduledEventDto.Description,
            DateRange = new DateRange(addScheduledEventDto.Start,addScheduledEventDto.End)
        };
        
        _repository.Add(scheduledEvent);
        await _unitOfWork.Complete();
        return scheduledEvent;
    }
}