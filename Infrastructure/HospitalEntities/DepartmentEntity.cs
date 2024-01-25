using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

[Index(nameof(DepartmentName), IsUnique = true)]
public class DepartmentEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string DepartmentName { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(HospitalEntity))]
    public int HospitalId {  get; set; }

    public virtual HospitalEntity Hospital { get; set; } = null!;

    public ICollection<DoctorEntity> Doctors = new List<DoctorEntity>();

    public ICollection<StaffEntity> Staff = new List<StaffEntity>();

}
