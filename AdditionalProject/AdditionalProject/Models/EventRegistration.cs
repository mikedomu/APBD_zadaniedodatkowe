using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdditionalProject.Models;

[Table("EventRegistration")]
[PrimaryKey(nameof(IdEvent),nameof(IdParticipant))]
public class EventRegistration
{
    public int IdEvent { get; set; }
    public int IdParticipant { get; set; }
    public DateTime RegisteredAt { get; set; }
    
    [ForeignKey(nameof(IdEvent))]
    public virtual Event Event { get; set; } = null!;
    
    [ForeignKey(nameof(IdParticipant))]
    public virtual Participant Participant { get; set; } = null!;
    
}