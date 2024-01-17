using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AuthenticationEntity
{
    [Key]
    [ForeignKey(nameof(Person))]
    public int PersonId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string UserName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string Password { get; set; } = null!;

    public virtual PersonEntity? Person { get; set; }
}

