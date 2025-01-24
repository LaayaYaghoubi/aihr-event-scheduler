using System.ComponentModel.DataAnnotations;

namespace AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;

public record UpdateScheduledEventDto(
    [Required] string Title ,
    [Required] string Description,
    [Required] DateTime Start,
    [Required] DateTime End);
