using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AuthenticationEntity
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    [ForeignKey(nameof(EmployeeEntity))]
    public int EmployeeId { get; set; }

}
