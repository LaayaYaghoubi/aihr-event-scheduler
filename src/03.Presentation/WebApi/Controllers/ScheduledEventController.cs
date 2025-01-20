using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.EventScheduler.WebApi.Controllers;

[ApiController]
[Route("api/v1/scheduled-event")]
public class ScheduledEventController : ControllerBase
{
    private readonly IScheduledEventService _scheduledEventService;

    public ScheduledEventController(IScheduledEventService scheduledEventService)
    {
        _scheduledEventService = scheduledEventService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ScheduledEvent>> Add([FromBody] AddScheduledEventDto dto)
    {
        var response = await _scheduledEventService.AddAsync(dto);
        return Ok(response);
    }
}