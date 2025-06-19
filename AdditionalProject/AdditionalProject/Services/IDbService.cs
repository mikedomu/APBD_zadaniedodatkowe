using System.Globalization;
using AdditionalProject.Data;
using AdditionalProject.Exceptions;
using AdditionalProject.Models;
using AdditionalProject.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AdditionalProject.Services;

public interface IDbService
{
    public Task CreateEventAsync(EventCreateDto eventData);
    public Task AssignSpeakerToEventAsync(int EventId, int IdSpeaker);
    public Task RegisterParticipantAsync(int EventId,int IdParticipant);
    public Task UnregisterParticipantAsync(int EventId, int IdParticipant);
    public Task<List<EventEverythingDto>> GetUpcomingEventsAsync();
    
    public Task<List<ParticipantEventDto>> GetParticipantEventsAsync(int participantId);
    
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task CreateEventAsync(EventCreateDto dto)
    {
        if (dto.Date < DateTime.Now)
        {
            throw new DataException("Data nie może być przeszła");
        }

        var newEvent = new Event
        {
            Name = dto.Name,
            Description = dto.Description,
            Date = dto.Date,
            Capacity = dto.Capacity,
        };
        data.Events.Add(newEvent);
        await data.SaveChangesAsync();
    }

    public async Task AssignSpeakerToEventAsync(int EventID, int IdSpeaker)
    {
        var eventCheck = await data.Events.FindAsync(EventID);
        var speaker = await data.Speakers.FindAsync(IdSpeaker);

        if (eventCheck == null || speaker == null)
            throw new NotFoundException("Prelegent bądź wydarzenie nie istnieje");

        var eventDate = eventCheck.Date;

        var dataConflict = await data.EventSpeakers
            .Where(x => x.IdSpeaker == IdSpeaker && x.IdEvent != EventID)
            .Select(x => x.Event)
            .AnyAsync(e => e.Date == eventDate);

        if (dataConflict)
            throw new ConflictException("Prelegent nie może uczestniczyć w wydarzeniach o tej samej porze");

        var exists = await data.EventSpeakers
            .AnyAsync(x => x.IdSpeaker == IdSpeaker && x.IdEvent == EventID);

        if (exists)
            throw new ConflictException("Prelegent jest już przypisany do tego wydarzenia");
        
        await using var transaction = await data.Database.BeginTransactionAsync();
        try
        {
            data.EventSpeakers.Add(new EventSpeaker
            {
                IdEvent = EventID,
                IdSpeaker = IdSpeaker
            });

            await data.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


    public async Task RegisterParticipantAsync(int eventId, int idParticipant)
    {
        var eventCheck = await data.Events.FindAsync(eventId);
        var participant = await data.Participants.FindAsync(idParticipant);

        if (eventCheck == null || participant == null)
            throw new NotFoundException("Uczestnik bądź wydarzenie nie istnieje");

        if (await data.EventRegistrations.AnyAsync(er => er.IdEvent == eventId && er.IdParticipant == idParticipant))
            throw new ConflictException("Uczestnik jest już zapisany");

        if (await data.EventRegistrations.CountAsync(er => er.IdEvent == eventId) >= eventCheck.Capacity)
            throw new ConflictException("Limit miejsc osiągnięty");

        await using var transaction = await data.Database.BeginTransactionAsync();

        try
        {
            data.EventRegistrations.Add(new EventRegistration
            {
                IdEvent = eventId,
                IdParticipant = idParticipant,
                RegisteredAt = DateTime.ParseExact(
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture
                )
            });

            await data.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


    public async Task UnregisterParticipantAsync(int eventId, int idParticipant)
    {
        var eventCheck = await data.Events.FindAsync(eventId);
        if (eventCheck == null)
        {
            throw new NotFoundException("Wydarzenie nie istnieje.");
        }
            
        var registration= await data.EventRegistrations
            .FirstOrDefaultAsync(r=>r.IdEvent == eventId && r.IdParticipant == idParticipant);
        if (registration == null)
        {
            throw new NotFoundException("Rejestracja nie istnieje.");
        }
        TimeSpan difference = eventCheck.Date- DateTime.Now;
        if (difference.TotalHours <= 24)
        {
            throw new ConflictException("Nie można anulować udziału na mniej niż 24h przed wydarzeniem.");
        }
        data.EventRegistrations.Remove(registration);
        await data.SaveChangesAsync();
    }
    public async Task<List<EventEverythingDto>> GetUpcomingEventsAsync()
    {
        var now = DateTime.Now;

        var result = await data.Events
            .Where(e => e.Date > now)
            .Select(e => new EventEverythingDto
            {
                IdEvent = e.Id,
                Name = e.Name,
                Description = e.Description ?? "",
                Date = e.Date,
                Speakers = e.EventSpeakers
                    .Select(es => es.Speaker.FirstName + " " + es.Speaker.LastName)
                    .ToList(),
                RegisteredParticipants = e.EventRegistrations.Count,
                AvailableSeats = e.Capacity - e.EventRegistrations.Count
            })
            .ToListAsync();

        if (!result.Any())
            throw new NotFoundException("Brak nadchodzących wydarzeń.");

        return result;
    }

    public async Task<List<ParticipantEventDto>> GetParticipantEventsAsync(int participantId)
    {
        var participantExists = await data.Participants.AnyAsync(p => p.Id == participantId);
        if (!participantExists)
            throw new NotFoundException("Uczestnik nie istnieje.");
        
        var events= await data.EventRegistrations
            .Where(r => r.IdParticipant == participantId && r.Event.Date < DateTime.Now)
            .Select(r=> new ParticipantEventDto
            {
                IdEvent = r.Event.Id,
                EventName = r.Event.Name,
                EventDate = r.Event.Date,
                RegisteredAt = r.RegisteredAt,
                Speakers= r.Event.EventSpeakers
                .Select(es => es.Speaker.LastName)
                .ToList(),
            })
            .ToListAsync();
        if (!events.Any())
            throw new NotFoundException("Uczestnik nie brał udziału w żadnych  wydarzeniach.");

        return events;
    }
}