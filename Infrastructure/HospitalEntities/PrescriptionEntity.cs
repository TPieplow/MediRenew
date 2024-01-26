using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

public class PrescriptionEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Precision(10,2)]
    public decimal Cost { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string Dosage { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(PatientEntity))]
    public int PatientId { get; set; }

    [Required]
    [ForeignKey(nameof(DoctorEntity))]
    public int DoctorId {  get; set; }

    [Required]
    [ForeignKey(nameof(PharmacyEntity))]
    public int PharmacyId { get; set; }

    public virtual PatientEntity Patient { get; set; } = null!;
    public virtual DoctorEntity Doctor { get; set; } = null!;
    public virtual PharmacyEntity Pharmacy { get; set; } = null!;
}
