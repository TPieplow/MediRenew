using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(RoleEntity))]
    public int RoleId { get; set; }

    public virtual AuthenticationEntity Authentication { get; set; } = null!;
    public virtual PersonProfileEntity PersonProfile { get; set; } = null!;
    public virtual RoleEntity Role { get; set; } = null!;
}
