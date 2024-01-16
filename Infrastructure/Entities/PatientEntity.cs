using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

[Index(nameof(SocialSecurityNo), IsUnique = true)]

public class PatientEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SocialSecurityNo { get; set; }

    public int? ActivePrescriptions { get; set; }
    public DateTime? LastVisit { get; set; }

    [Required]
    [ForeignKey(nameof(EmployeeEntity))]
    public int EmployeeId { get; set; }

    [Required]
    [ForeignKey(nameof(PrescriptionEntity))]
    public int PrescriptionId { get; set; }

    public virtual PersonProfileEntity PersonProfile { get; set; } = null!;
}
