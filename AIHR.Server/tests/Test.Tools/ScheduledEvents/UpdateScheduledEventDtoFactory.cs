using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;

namespace AIHR.EventScheduler.Test.Tools.ScheduledEvents;

public class UpdateScheduledEventDtoFactory
{
    public UpdateScheduledEventDto Create()
    {
        return new UpdateScheduledEventDto(
            "updated title", 
            "updatedDescription", 
            DateTime.Now,
            DateTime.Now.AddHours(2));
    }

    public UpdateScheduledEventDto Create(DateTime start, DateTime end)
    {
        return new UpdateScheduledEventDto(
            "updated title", 
            "updatedDescription", 
            start,
            end);
    }
}