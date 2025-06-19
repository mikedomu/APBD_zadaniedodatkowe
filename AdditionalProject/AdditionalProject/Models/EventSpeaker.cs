using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AdditionalProject.Models;

[Table("EventSpeaker")]
[PrimaryKey(nameof(IdEvent),nameof(IdSpeaker))]
public class EventSpeaker
{
    public int IdSpeaker { get; set; }
    public int IdEvent { get; set; }
    
    [ForeignKey(nameof(IdSpeaker))]
    public virtual Speaker Speaker { get; set; } = null!;
    
    [ForeignKey(nameof(IdEvent))]
    public virtual Event Event { get; set; } = null!;
}