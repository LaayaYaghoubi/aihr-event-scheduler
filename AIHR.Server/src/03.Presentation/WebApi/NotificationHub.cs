using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace AIHR.EventScheduler.WebApi;

[Authorize]
public class NotificationHub : Hub
{
    private readonly IScheduledEventService _scheduledEventService;

    public NotificationHub(IScheduledEventService scheduledEventService)
    {
        _scheduledEventService = scheduledEventService;
    }

    public async Task AcknowledgeNotification(int eventId)
    {
        await _scheduledEventService.Notified(eventId);
    }
}