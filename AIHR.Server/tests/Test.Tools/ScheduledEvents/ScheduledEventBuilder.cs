using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;

namespace AIHR.EventScheduler.Test.Tools.ScheduledEvents
{
    public class ScheduledEventBuilder
    {
        private readonly ScheduledEvent _scheduledEvent;

        public ScheduledEventBuilder()
        {
            _scheduledEvent = new ScheduledEvent
            {
                Title = "dummy title",
                Description = "dummy description",
                DateRange = new DateRange(DateTime.Now, DateTime.Now.AddHours(2))
            };
        }

        public ScheduledEventBuilder WithTitle(string title)
        {
            _scheduledEvent.Title = title;
            return this;
        }

        public ScheduledEventBuilder WithDescription(string description)
        {
            _scheduledEvent.Description = description;
            return this;
        }

        public ScheduledEventBuilder WithDateRange(DateRange dateRange)
        {
            _scheduledEvent.DateRange = dateRange;
            return this;
        }

        public ScheduledEvent Build()
        {
            return _scheduledEvent;
        }
    }
}