using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents.Exceptions;

namespace AIHR.EventScheduler.Domain.Unit.Tests.ScheduledEvents;

public class DateRangeTests
{
    [Fact]
    public void DateRange_StartBeforeEnd_ShouldCreateDateRange()
    {
        var start = new DateTime(2023, 1, 1);
        var end = new DateTime(2023, 1, 2);

        var dateRange = new DateRange(start, end);

        Assert.Equal(start, dateRange.Start);
        Assert.Equal(end, dateRange.End);
    }

    [Fact]
    public void DateRange_StartEqualToEnd_ShouldThrowException()
    {
        var start = new DateTime(2023, 1, 1);
        var end = new DateTime(2023, 1, 1);

        Assert.Throws<StartDateMustBeBeforeEndDateException>(() => new DateRange(start, end));
    }

    [Fact]
    public void DateRange_StartAfterEnd_ShouldThrowException()
    {
        var start = new DateTime(2023, 1, 2);
        var end = new DateTime(2023, 1, 1);

        Assert.Throws<StartDateMustBeBeforeEndDateException>(() => new DateRange(start, end));
    }
}