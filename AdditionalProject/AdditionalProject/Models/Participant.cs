using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdditionalProject.Models;
[Table("Participant")]
public class Participant
{
    [Key]
    [Column("IdParticipant")]
    public int Id { get; set; }
    
    [MaxLength(50)] 
    public string FirstName { get; set; } = null!;
    
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
    
    [MaxLength(100)]
    public string Email { get; set; } = null!;
    
    public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = null!;
    
    
}