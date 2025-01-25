using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Exceptions;
using AIHR.EventScheduler.Contracts.Interfaces;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents.Exceptions;
using AIHR.EventScheduler.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using AIHR.EventScheduler.Test.Tools.ScheduledEvents;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace AIHR.EventScheduler.Applications.Unit.Tests.ScheduledEvents.Update;

public class UpdateScheduledEventTests : BusinessUnitTest
{
    private readonly IScheduledEventService _sut;
    private readonly IDateTimeService _dateTimeServiceMock;

    public UpdateScheduledEventTests()
    {
        _dateTimeServiceMock = Substitute.For<IDateTimeService>();
        _dateTimeServiceMock.Now.Returns(DateTime.Now.AddDays(-1));
        _dateTimeServiceMock.Today.Returns(DateTime.Today.AddDays(-1));
        _sut = new ScheduledEventServiceFactory().Create(SetupContext, dateTimeService: _dateTimeServiceMock);
    }

    [Fact]
    public async Task UpdateAsync_WithValidDto_UpdatesScheduledEventProperly()
    {
        var scheduledEvent = new ScheduledEventBuilder().Build();
        Save(scheduledEvent);

        var updateScheduledEventDto = new UpdateScheduledEventDtoFactory().Create();

        await _sut.UpdateAsync(scheduledEvent.Id, updateScheduledEventDto);

        var actualEvent =
            await ReadContext
                .Set<ScheduledEvent>()
                .SingleAsync(se => se.Id == scheduledEvent.Id);

        Assert.Equal(updateScheduledEventDto.Title, actualEvent.Title);
        Assert.Equal(updateScheduledEventDto.Description, actualEvent.Description);
        Assert.Equal(updateScheduledEventDto.Start, actualEvent.DateRange.Start);
        Assert.Equal(updateScheduledEventDto.End, actualEvent.DateRange.End);
    }

    [Theory]
    [InlineData(0)]
    public async Task UpdateAsync_WithNonExistentId_ShouldThrowException(int dummyId)
    {
        var updateScheduledEventDto = new UpdateScheduledEventDtoFactory().Create();

        await Assert.ThrowsAsync<ScheduledEventNotFoundException>(() =>
            _sut.UpdateAsync(dummyId, updateScheduledEventDto));
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidDateRange_ShouldThrowException()
    {
        var scheduledEvent = new ScheduledEventBuilder().Build();
        Save(scheduledEvent);
        var start = DateTime.Now.AddDays(2);
        var end = DateTime.Now.AddDays(1);
        var updateScheduledEventDto = new UpdateScheduledEventDtoFactory().Create(start, end);


        await Assert.ThrowsAsync<StartDateMustBeBeforeEndDateException>(() =>
            _sut.UpdateAsync(scheduledEvent.Id, updateScheduledEventDto));
    }

    [Fact]
    public async Task UpdateAsync_withStartInPast_ShouldThrowException()
    {
        var scheduledEvent = new ScheduledEventBuilder().Build();
        Save(scheduledEvent);
        var updateScheduledEventDto = new UpdateScheduledEventDtoFactory().Create();
        _dateTimeServiceMock.Now.Returns(updateScheduledEventDto.Start.AddDays(1));

        await Assert.ThrowsAsync<ScheduledEventCanNotBeInPastException>(() =>
            _sut.UpdateAsync(scheduledEvent.Id, updateScheduledEventDto));
    }
}