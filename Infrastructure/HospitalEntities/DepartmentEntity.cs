using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

public class DepartmentEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string DepartmentName { get; set; } = null!;

    [ForeignKey(nameof(HospitalEntity))]
    public int HospitalId {  get; set; }
    
}
