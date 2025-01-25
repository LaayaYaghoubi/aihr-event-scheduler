using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Exceptions;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents.Exceptions;
using AIHR.EventScheduler.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using AIHR.EventScheduler.Test.Tools.ScheduledEvents;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace AIHR.EventScheduler.Applications.Unit.Tests.ScheduledEvents.Create;

public class AddScheduledEventTests : BusinessUnitTest
{
    private readonly IScheduledEventService _sut;
    private readonly IDateTimeService _dateTimeServiceMock;

    public AddScheduledEventTests()
    {
        _dateTimeServiceMock = Substitute.For<IDateTimeService>();
        _dateTimeServiceMock.Now.Returns(DateTime.Now.AddDays(-1));
        _dateTimeServiceMock.Today.Returns(DateTime.Today.AddDays(-1));
        _sut = new ScheduledEventServiceFactory().Create(SetupContext, dateTimeService: _dateTimeServiceMock);
    }

    [Fact]
    public async Task AddAsync_WithValidDto_AddedScheduledEventProperly()
    {
        var createScheduledEventDto = new AddScheduledEventDtoFactory().Create();

        var scheduledEvent = await _sut.AddAsync(createScheduledEventDto);

        var actualEvent =
            await ReadContext
                .Set<ScheduledEvent>()
                .SingleAsync(se => se.Id == scheduledEvent.Id);

        Assert.Equal(createScheduledEventDto.Title, actualEvent.Title);
        Assert.Equal(createScheduledEventDto.Description, actualEvent.Description);
        Assert.Equal(createScheduledEventDto.Start, actualEvent.DateRange.Start);
        Assert.Equal(createScheduledEventDto.End, actualEvent.DateRange.End);
    }

    [Fact]
    public async Task AddAsync_WithInvalidDateRange_ShouldThrowException()
    {
        var start = DateTime.Now.AddDays(2);
        var end = DateTime.Now.AddDays(1);
        var createScheduledEventDto = new AddScheduledEventDtoFactory().Create(start, end);

        await Assert.ThrowsAsync<StartDateMustBeBeforeEndDateException>(() =>
            _sut.AddAsync(createScheduledEventDto));
    }

    [Fact]
    public async Task AddAsync_withStartInPast_ShouldThrowException()
    {
        var createScheduledEventDto = new AddScheduledEventDtoFactory().Create();
        _dateTimeServiceMock.Now.Returns(createScheduledEventDto.Start.AddDays(1));
        
        await Assert.ThrowsAsync<ScheduledEventCanNotBeInPastException>(() =>
            _sut.AddAsync(createScheduledEventDto));
    }
}