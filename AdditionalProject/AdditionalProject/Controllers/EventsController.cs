using AdditionalProject.Exceptions;
using AdditionalProject.Models.DTOs;
using AdditionalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdditionalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController(IDbService service):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto eventData)
    {
        try
        {
            await service.CreateEventAsync(eventData);
            return Ok($"Utworzono event o nazwie: {eventData.Name}");

        }
        catch (DataException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{idEvent}/speakers")]
    public async Task<IActionResult> AssignSpeakerToEvent([FromRoute] int idEvent,
        [FromBody] AssignSpeakerDto assignSpeakerDto)
    {
        try
        {
            await service.AssignSpeakerToEventAsync(idEvent, assignSpeakerDto.IdSpeaker);
            return Ok($"Przypisano prelegenta o id {assignSpeakerDto.IdSpeaker} do eventu o id {idEvent}");

        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        
        
    }
    [HttpPost("{idEvent}/register-participant")]
    public async Task<IActionResult> RegisterParticipant([FromRoute] int idEvent,[FromBody] EventRegistrationDTO dto)
    {
        try
        {
            await service.RegisterParticipantAsync(idEvent, dto.IdParticipant);
            return Ok($"Zarejestrowano uczestnika o id {dto.IdParticipant} do eventu o id {idEvent}");

        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{idEvent}/unregister-participant")]
    public async Task<IActionResult> UnregisterParticipant([FromRoute] int  idEvent, [FromBody] EventRegistrationDTO dto)
    {
        try
        {
            await service.UnregisterParticipantAsync(idEvent, dto.IdParticipant);
            return Ok("Udział został anulowany.");
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingEvents()
    {
        try
        {
            var events = await service.GetUpcomingEventsAsync();
            return Ok(events);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }


    
}