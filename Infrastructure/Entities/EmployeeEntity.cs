using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string EmployeeRole { get; set; } = null!;
}
