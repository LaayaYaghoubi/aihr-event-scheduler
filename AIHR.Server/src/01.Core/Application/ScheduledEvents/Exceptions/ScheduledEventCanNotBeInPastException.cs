using AIHR.EventScheduler.Contracts.BaseClasses;

namespace AIHR.EventScheduler.Application.ScheduledEvents.Exceptions;

public class ScheduledEventCanNotBeInPastException() : KnownException("A scheduled event cannot have a start time in the past.")
{
    
}