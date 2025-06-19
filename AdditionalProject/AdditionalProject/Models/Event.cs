using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdditionalProject.Models;
[Table("Event")]
public class Event
{
    [Key]
    [Column("IdEvent")]
    public int Id { get; set; }
    
    [MaxLength(120)]
    public string Name { get; set; } = null!;
    
    [MaxLength(500)] 
    public string? Description { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Capacity { get; set; }
    
    public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = null!;
    public virtual ICollection<EventSpeaker> EventSpeakers { get; set; } = null!;

}