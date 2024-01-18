using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

public class InvoiceEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string Description { get; set; } = null!;

    [Required]
    [Precision(10, 2)]
    public decimal Cost { get; set; }

    [Required]
    [Precision(10, 2)]
    public decimal TotalCost { get; set; }

    [Required]
    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }
    public virtual PatientEntity Patient { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Pharmacy))]
    public int PharmacyId { get; set; }
    public virtual PharmacyEntity Pharmacy { get; set; } = null!;
}
