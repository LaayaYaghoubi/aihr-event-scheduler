namespace AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;

public record GetScheduledEventDto(
    int Id,
    string Title,
    string Description,
    DateTime Start,
    DateTime End);