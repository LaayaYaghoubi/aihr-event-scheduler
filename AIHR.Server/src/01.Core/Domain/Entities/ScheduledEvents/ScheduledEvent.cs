using AIHR.EventScheduler.Contracts.BaseClasses;

namespace AIHR.EventScheduler.Domain.Entities.ScheduledEvents;

public class ScheduledEvent : BaseEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateRange DateRange { get; set; }
    public string UserId { get; set; }
    public bool IsNotified { get; set; }
}
  