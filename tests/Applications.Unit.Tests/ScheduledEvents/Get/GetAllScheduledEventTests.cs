using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using AIHR.EventScheduler.Test.Tools.ScheduledEvents;
using AIHR.EventSchedulerInfrastructure.Helper;

namespace AIHR.EventScheduler.Applications.Unit.Tests.ScheduledEvents.Get;

public class GetAllScheduledEventTests : BusinessUnitTest
{
    private readonly IScheduledEventService _sut;

    public GetAllScheduledEventTests()
    {
        _sut = new ScheduledEventServiceFactory().Create(SetupContext);
    }

    [Fact]
    public async Task GetAll_WithDefaultSort_ReturnsEventsSortedByDate()
    {
        var event1 = new ScheduledEventBuilder().WithDateRange(new DateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2))).Build();
        var event2 = new ScheduledEventBuilder().WithDateRange(new DateRange(DateTime.Now.AddDays(3), DateTime.Now.AddDays(4))).Build();
        Save(event1, event2);

        var result = await _sut.GetAllAsync(SortOrder.Ascending,new Pagination());

        Assert.Collection(
            result.Items, 
            e => Assert.Equal(event1.DateRange.Start, e.Start),
            e => Assert.Equal(event2.DateRange.Start, e.Start));
    }

    [Fact]
    public async Task GetAll_WithNoEvents_ReturnsEmptyList()
    {
        var result = await _sut.GetAllAsync(SortOrder.Ascending,new Pagination());

        Assert.Empty(result.Items);
        Assert.Equal(0,result.TotalCount);
    }

    [Fact]
    public async Task GetAll_WithMultipleEvents_ReturnsAllEvents()
    {
        var event1 = new ScheduledEventBuilder().Build();
        var event2 = new ScheduledEventBuilder().Build();
        Save(event1, event2);

        var result = await _sut.GetAllAsync(SortOrder.Ascending,new Pagination());

        Assert.Equal(2, result.TotalCount);
    }
}
