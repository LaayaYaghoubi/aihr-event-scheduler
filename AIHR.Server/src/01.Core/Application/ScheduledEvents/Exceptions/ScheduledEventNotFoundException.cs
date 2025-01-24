using AIHR.EventScheduler.Contracts.BaseClasses;

namespace AIHR.EventScheduler.Application.ScheduledEvents.Exceptions;

public class ScheduledEventNotFoundException() : KnownException("The scheduled event was not found.")
{
}