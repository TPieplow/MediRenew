using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities;

public class PatientEntity
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
    public string Address { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(24)")]
    public string City { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(7)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string Email { get; set; } = null!;

    [ForeignKey(nameof(PharmacyEntity))]
    public int? PharmacyId { get; set; }
    

    //Prescription? 
}
