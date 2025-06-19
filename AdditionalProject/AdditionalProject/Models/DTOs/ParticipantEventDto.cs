namespace AdditionalProject.Models.DTOs;

public class ParticipantEventDto
{
    public int IdEvent { get; set; }
    public string EventName { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public List<string> Speakers { get; set; } = new();
    public DateTime RegisteredAt { get; set; } 
}