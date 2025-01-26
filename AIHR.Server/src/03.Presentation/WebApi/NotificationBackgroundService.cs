using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventScheduler.Persistence.EF;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AIHR.EventScheduler.WebApi;

public class NotificationBackgroundService(
    ILogger<NotificationBackgroundService> logger,
    IHubContext<NotificationHub> hubContext,
    EfDataContext context)
    : IHostedService, IDisposable
{
    private Timer _timer;


    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Notification Background Service is starting.");
        _timer = new Timer(CheckForUpcomingEvents, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        return Task.CompletedTask;
    }
    
    private async void CheckForUpcomingEvents(object? state)
    {
        var events = await GetEventsWithinNext24HoursAsync();
        foreach (var @event in events)
        {
            var userId = @event.UserId;
            var message = $"Reminder: \"{@event.Title}\" is scheduled on {@event.DateRange.Start:MMMM d, yyyy h:mm tt}.";
            await hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message, @event.Id);
            logger.LogInformation("Notification sent");
        }
    }

    private Task<List<ScheduledEvent>> GetEventsWithinNext24HoursAsync()
    {
        var now = DateTime.UtcNow;
        var next24Hours = now.AddHours(24);

        return context.Set<ScheduledEvent>()
            .Where(e => e.DateRange.Start >= now && e.DateRange.Start <= next24Hours  && !e.IsNotified)
            .ToListAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Notification Background Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}