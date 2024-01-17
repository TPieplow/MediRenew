using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class RoleEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(24)")]
    public string Role { get; set; } = null!;

    public virtual ICollection<PersonProfileEntity> PersonProfiles { get; set; } = new List<PersonProfileEntity>();
}

