using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AuthenticationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [ForeignKey(nameof(PersonEntity))]
    public int PersonId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string UserName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string Password { get; set; } = null!;

}

