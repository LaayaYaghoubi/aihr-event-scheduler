using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using AIHR.EventScheduler.Test.Tools.ScheduledEvents;

namespace AIHR.EventScheduler.Applications.Unit.Tests.ScheduledEvents.Get;

public class GetScheduledEventByIdTests : BusinessUnitTest
{
    private readonly IScheduledEventService _sut;

    public GetScheduledEventByIdTests()
    {
        _sut = new ScheduledEventServiceFactory().Create(SetupContext);
    }

    [Fact]
    public async Task GetById_WithExistentId_ReturnsScheduledEvent()
    {
        var expectedEvent = new ScheduledEventBuilder().Build();
        Save(expectedEvent);

        var actualEvent = await _sut.GetByIdAsync(expectedEvent.Id);

        Assert.NotNull(actualEvent);
        Assert.Equal(expectedEvent.Id, actualEvent.Id);
        Assert.Equal(expectedEvent.Title, actualEvent.Title);
        Assert.Equal(expectedEvent.Description, actualEvent.Description);
        Assert.Equal(expectedEvent.DateRange.Start, actualEvent.Start);
        Assert.Equal(expectedEvent.DateRange.End, actualEvent.End);
    }

    [Fact]
    public async Task GetById_WithNonExistentId_ShouldReturnsNull()
    {
        var nonExistentId = 999;

        var result = await _sut.GetByIdAsync(nonExistentId);

        Assert.Null(result);
    }
}