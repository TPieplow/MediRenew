using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class PersonProfileEntity
{
    [Key]
    public int ProfileId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(64)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string Country { get; set; } = null!;

    [Column(TypeName = "varchar(7)")]
    public string PostalCode { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string StreetName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(13)")]
    public int SocialSecurityNo { get; set; }

    public bool ActivePrescriptions { get; set; }
    public DateTime? LastVisit { get; set; }

    [Required]
    [ForeignKey(nameof(PersonId))]
    public int PersonId { get; set; }

    public virtual PersonEntity? Person { get; set; }
}