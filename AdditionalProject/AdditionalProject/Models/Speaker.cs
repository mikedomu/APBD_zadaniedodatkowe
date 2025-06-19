using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdditionalProject.Models;
[Table("Speaker")]
public class Speaker
{
    [Key]
    [Column("IdSpeaker")]
    public int Id { get; set; }
    
    [MaxLength(50)] 
    public string FirstName { get; set; } = null!;
    
    [MaxLength(50)]
    public string LastName { get; set; }= null!;

    [MaxLength(120)] 
    public string Email { get; set; } = null!;

    public virtual ICollection<EventSpeaker> EventSpeakers { get; set; } = null!;



}