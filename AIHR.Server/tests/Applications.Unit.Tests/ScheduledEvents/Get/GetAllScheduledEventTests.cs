using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using AIHR.EventScheduler.Test.Tools.ScheduledEvents;
using AIHR.EventSchedulerInfrastructure.Helper;
using NSubstitute;

namespace AIHR.EventScheduler.Applications.Unit.Tests.ScheduledEvents.Get;

public class GetAllScheduledEventTests : BusinessUnitTest
{
    private readonly IScheduledEventService _sut;
    private readonly IUserService _userService;

    public GetAllScheduledEventTests()
    {
        _userService = Substitute.For<IUserService>();
        _sut = new ScheduledEventServiceFactory().Create(SetupContext, _userService);
    }

    [Fact]
    public async Task GetAll_WithDefaultSort_ReturnsEventsSortedByDate()
    {
        const string userId = "userId";
        _userService.GetUserId().Returns(userId);
        var event1 = new ScheduledEventBuilder()
            .WithDateRange(new DateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)))
            .WithUserId(userId)
            .Build();
        var event2 = new ScheduledEventBuilder()
            .WithDateRange(new DateRange(DateTime.Now.AddDays(3), DateTime.Now.AddDays(4)))
            .WithUserId(userId)
            .Build();
        Save(event1, event2);

        var result = await _sut.GetAllAsync(SortOrder.Ascending, new Pagination());

        Assert.Collection(
            result.Items,
            e => Assert.Equal(event1.DateRange.Start, e.Start),
            e => Assert.Equal(event2.DateRange.Start, e.Start));
    }

    [Fact]
    public async Task GetAll_WithNoEvents_ReturnsEmptyList()
    {
        var result = await _sut.GetAllAsync(SortOrder.Ascending, new Pagination());

        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task GetAll_WithMultipleEvents_ReturnsAllEvents()
    {
        const string userId = "userId";
        _userService.GetUserId().Returns(userId);
        var event1 = new ScheduledEventBuilder().WithUserId(userId).Build();
        var event2 = new ScheduledEventBuilder().WithUserId(userId).Build();
        Save(event1, event2);

        var result = await _sut.GetAllAsync(SortOrder.Ascending, new Pagination());

        Assert.Equal(2, result.TotalCount);
    }
}