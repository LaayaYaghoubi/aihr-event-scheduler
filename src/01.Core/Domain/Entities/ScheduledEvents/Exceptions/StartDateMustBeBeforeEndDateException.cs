using AIHR.EventScheduler.Contracts.BaseClasses;

namespace AIHR.EventScheduler.Domain.Entities.ScheduledEvents.Exceptions;

public class StartDateMustBeBeforeEndDateException() : KnownException("Start date should be before end date.")
{
    
}