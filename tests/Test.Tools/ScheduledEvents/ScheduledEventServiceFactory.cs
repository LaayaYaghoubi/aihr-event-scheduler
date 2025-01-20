using AIHR.EventScheduler.Application.ScheduledEvents;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Persistence.EF;
using AIHR.EventScheduler.Persistence.EF.ScheduledEvents;

namespace AIHR.EventScheduler.Test.Tools.ScheduledEvents;

public class ScheduledEventServiceFactory
{
    public IScheduledEventService Create(EfDataContext context)
    {
        return new ScheduledEventService(
            new EfScheduledEventRepository(context),
            new EfUnitOfWork(context));
    }
}