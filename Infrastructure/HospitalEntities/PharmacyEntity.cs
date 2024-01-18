using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

public class PharmacyEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string MedicationName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(128)")]
    public string Dosage { get; set; } = null!;

    public int? PatientId { get; set; }
    public int? DoctorId { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual PatientEntity? Patient {  get; set; }

    [ForeignKey(nameof(DoctorId))]
    public virtual DoctorEntity? Doctor { get; set; }

}
