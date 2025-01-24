using AIHR.EventScheduler.Domain.Entities.ScheduledEvents.Exceptions;

namespace AIHR.EventScheduler.Domain.Entities.ScheduledEvents;

public class DateRange
{
    public DateRange(DateTime start, DateTime end)
    {
        if(start >= end) 
            throw new StartDateMustBeBeforeEndDateException();
        Start = start;
        End = end;
    }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
