namespace AdditionalProject.Models.DTOs;

public class EventCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime Date { get; set; }
    public required int Capacity { get; set; }
}