namespace AdditionalProject.Models.DTOs;

public class EventEverythingDto
{
    public int IdEvent { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public List<string> Speakers { get; set; } = new();
    public int RegisteredParticipants { get; set; }
    public int AvailableSeats { get; set; }
}