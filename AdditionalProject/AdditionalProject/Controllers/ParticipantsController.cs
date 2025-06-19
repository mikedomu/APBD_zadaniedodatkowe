using AdditionalProject.Exceptions;
using AdditionalProject.Models;
using AdditionalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdditionalProject.Controllers;
[ApiController]
[Route("[controller]")]
public class ParticipantsController(IDbService service):ControllerBase
{
    [HttpGet("{participantId}/past-events")]
    public async Task<IActionResult> GetParticipantPastEvents([FromRoute] int participantId)
    {
        try
        {
            var events = await service.GetParticipantEventsAsync(participantId);
            return Ok(events);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    
    
}