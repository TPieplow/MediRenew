using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class PersonEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(RoleEntity))]
    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;

}
