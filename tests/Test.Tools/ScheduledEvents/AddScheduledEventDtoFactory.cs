using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;

namespace AIHR.EventScheduler.Test.Tools.ScheduledEvents;

public class AddScheduledEventDtoFactory
{
    public AddScheduledEventDto Create()
    {
        return new AddScheduledEventDto(
            "Test Event",
            "This is a test event",
            DateTime.Now.AddDays(1),
            DateTime.Now.AddDays(1).AddHours(2));
    }

    public AddScheduledEventDto Create(DateTime start, DateTime end)
    {
        return new AddScheduledEventDto(
            "Test Event",
            "This is a test event",
            start,
            end);
    }
}