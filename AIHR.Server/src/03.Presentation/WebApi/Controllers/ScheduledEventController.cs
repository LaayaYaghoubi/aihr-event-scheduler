using AIHR.EventScheduler.Application.ScheduledEvents.Contracts;
using AIHR.EventScheduler.Application.ScheduledEvents.Contracts.Dto;
using AIHR.EventScheduler.Domain.Entities.ScheduledEvents;
using AIHR.EventSchedulerInfrastructure.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AIHR.EventScheduler.WebApi.Controllers;

[ApiController]
[Route("api/v1/scheduled-event")]
public class ScheduledEventController(IScheduledEventService scheduledEventService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ScheduledEvent>> Add([FromBody] AddScheduledEventDto dto)
    {
        var response = await scheduledEventService.AddAsync(dto);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromRoute] int id, UpdateScheduledEventDto dto)
    {
        await scheduledEventService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await scheduledEventService.Delete(id);
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetScheduledEventDto>> GetById([FromRoute] int id)
    {
        var response = await scheduledEventService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<GetScheduledEventDto>>> GetAll(
        [FromQuery] Pagination pagination,
        [FromQuery] SortOrder sortOrder = SortOrder.Descending)
    {
        var response = await scheduledEventService.GetAllAsync(sortOrder, pagination);
        return Ok(response);
    }
}