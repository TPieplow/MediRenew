using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class PersonEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    
    public int ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public virtual PersonProfileEntity? Profile { get; set; } = null!;
}
