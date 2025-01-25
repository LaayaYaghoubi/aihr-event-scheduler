using AIHR.EventScheduler.Application.ScheduledEvents;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Persistence.EF;
using AIHR.EventScheduler.Persistence.EF.ScheduledEvents;
using NSubstitute;

namespace AIHR.EventScheduler.Test.Tools.ScheduledEvents;

public class ScheduledEventServiceFactory
{
    public IScheduledEventService Create(EfDataContext context, IUserService? userService = null)
    {
        return new ScheduledEventService(
            new EfScheduledEventRepository(context),
            new EfUnitOfWork(context),
            userService ?? Substitute.For<IUserService>());
    }
}