using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

public class StaffEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(24)")]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [Column(TypeName = "nvarchar(24)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(24)")]
    public string RoleName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; } = null!;

    [ForeignKey(nameof(DepartmentEntity))]
    public int DepartmentId { get; set; }
    

    //Lista med doctors och staff??

}
