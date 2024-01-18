using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class PrescriptionEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string PrescriptionDetails { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(PersonEntity))]
    public int DoctorId { get; set; }

    [Required]
    [ForeignKey(nameof(PersonEntity))]
    public int PatientId { get; set; }

    [DeleteBehavior(DeleteBehavior.ClientSetNull)]
    public virtual PersonEntity? Doctor { get; set; }

    [DeleteBehavior(DeleteBehavior.ClientSetNull)]
    public virtual PersonEntity? Patient { get; set; }
}
