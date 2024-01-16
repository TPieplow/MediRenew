using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AuthenticationEntity
{
    [Column(TypeName = "nvarchar(64)")]
    public string UserName { get; set; } = null!;

    [Column(TypeName = "nvarchar(64)")]
    public string Password { get; set; } = null!;

    [ForeignKey(nameof(EmployeeEntity))]
    public int EmployeeId { get; set; }

}
