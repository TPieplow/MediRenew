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
    [ForeignKey(nameof(PatientEntity))]
    public int PatientId { get; set; }

}
