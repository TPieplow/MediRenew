using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

public class PrescriptionEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string MedicationName { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Precision(10,2)]
    public decimal Cost { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string Instructions { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }
    public virtual PatientEntity Patient { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Doctor))]
    public int DoctorId {  get; set; }
    public virtual DoctorEntity Doctor { get; set; } = null!;

    [ForeignKey(nameof(Pharmacy))]
    public int PharmacyId { get; set; }
    public virtual PharmacyEntity? Pharmacy { get; set; }
}
