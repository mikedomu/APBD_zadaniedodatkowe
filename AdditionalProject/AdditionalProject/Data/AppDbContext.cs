using System.Globalization;
using AdditionalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AdditionalProject.Data;

public class AppDbContext: DbContext
{
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<EventSpeaker> EventSpeakers { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       modelBuilder.Entity<Event>()
            .Property(e => e.Date)
            .HasColumnType("datetime2(0)");

        modelBuilder.Entity<EventRegistration>()
            .Property(r => r.RegisteredAt)
            .HasColumnType("datetime2(0)");
        
        DateTime ToDate(string input) =>
           DateTime.ParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture); 
        /* Parsowanie na dzien,miesiac,rok + godzina i minuty znalazłem tutaj :
         https://stackoverflow.com/questions/5366285/how-to-parse-strings-to-datetime-in-c-sharp-properly */

        var speakers = new List<Speaker>
        {
            new()
            {
                Id = 1, 
                FirstName = "Anna", 
                LastName = "Nowak", 
                Email = "anna.nowak@gmail.com"
            },
            
            new() 
            { 
                Id = 2, 
                FirstName = "Jan", 
                LastName = "Kowalski", 
                Email = "jan.kowalski@wp.com" 
            },
            
            new() 
            { 
                Id = 3, 
                FirstName = "Ewa", 
                LastName = "Wiśniewska", 
                Email = "ewa.w@yahoo.com" 
            }
        };

        var events = new List<Event>
        {
            new() 
            { 
                Id = 1, 
                Name = "Konferencja Ogórek", 
                Description = "Event poświęcony ogórkom kiszonym i jak je przygotowywać",
                Date = ToDate("15/07/2025 10:00"), 
                Capacity = 100 
            },
            
            new() 
            { 
                Id = 2, 
                Name = "AI Workshop",
                Description = "Warsztaty o AI",
                Date = ToDate("16/07/2025 13:00"), 
                Capacity = 50 
            },
            
            new() 
            { 
                Id = 3, 
                Name = "Kochamy Pomidory", 
                Date = ToDate("20/07/2025 09:30"), 
                Capacity = 75 
            }
        };

        var participants = new List<Participant>
        {
            new() 
                { 
                    Id = 1, 
                    FirstName = "Katarzyna", 
                    LastName = "Wójcik", 
                    Email = "kasia.w@xd.com" 
                },
            
            new() 
            { 
                Id = 2, 
                FirstName = "Piotr", 
                LastName = "Lewandowski", 
                Email = "piotr.l@xdxd.com" 
            },
            
            new() 
            { 
                Id = 3, 
                FirstName = "Zofia", 
                LastName = "Kaczmarek", 
                Email = "zofia.k@xdd.com" 
            }
        };

        var eventSpeakers = new List<EventSpeaker>
        {
            new()
            {
                IdEvent = 1, 
                IdSpeaker = 1
            },
            new()
            {
                IdEvent = 1, 
                IdSpeaker = 2
            },
            
            new()
            {
                IdEvent = 2, 
                IdSpeaker = 3
            },
            
            new()
            {
                IdEvent = 3, 
                IdSpeaker = 1
            }
        };

        var registrations = new List<EventRegistration>
        {
            new()
            {
                IdEvent = 1, 
                IdParticipant = 1, 
                RegisteredAt = ToDate("01/06/2025 10:00")
            },
            
            new()
            {
                IdEvent = 1, 
                IdParticipant = 2, 
                RegisteredAt = ToDate("02/06/2025 12:15")
            },
            
            new()
            {
                IdEvent = 2, 
                IdParticipant = 3, 
                RegisteredAt = ToDate("05/06/2025 14:30")
            },
            
            new()
            {
                IdEvent = 3, 
                IdParticipant = 1, 
                RegisteredAt = ToDate("08/06/2025 09:45")
            }
        };

        modelBuilder.Entity<Speaker>().HasData(speakers);
        modelBuilder.Entity<Event>().HasData(events);
        modelBuilder.Entity<Participant>().HasData(participants);
        modelBuilder.Entity<EventSpeaker>().HasData(eventSpeakers);
        modelBuilder.Entity<EventRegistration>().HasData(registrations);
    }



}