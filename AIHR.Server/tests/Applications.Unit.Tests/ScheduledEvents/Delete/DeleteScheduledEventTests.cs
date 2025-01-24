using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Exceptions;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using AIHR.EventScheduler.Test.Tools.ScheduledEvents;
using Microsoft.EntityFrameworkCore;

namespace AIHR.EventScheduler.Applications.Unit.Tests.ScheduledEvents.Delete;

public class DeleteScheduledEventTests : BusinessUnitTest
{
    private readonly IScheduledEventService _sut;

    public DeleteScheduledEventTests()
    {
        _sut = new ScheduledEventServiceFactory().Create(SetupContext);
    }
    
    [Fact]
    public async Task Delete_WithExistentId_DeletesScheduledEvent()
    {
        var scheduledEvent = new ScheduledEventBuilder().Build();
        Save(scheduledEvent);

        await _sut.Delete(scheduledEvent.Id);
        
        var actualEvent = await ReadContext
            .Set<ScheduledEvent>()
            .SingleOrDefaultAsync(se => se.Id == scheduledEvent.Id);
        Assert.Null(actualEvent);
    }

    [Fact]
    public async Task Delete_WithNonExistentId_ShouldThrowException()
    {
        var nonExistentId = 999;

        await Assert.ThrowsAsync<ScheduledEventNotFoundException>(() => _sut.Delete(nonExistentId));
    }
}