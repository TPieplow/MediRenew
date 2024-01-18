using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AppointmentEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(MAX)")]
    public string AppointmentDetails { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(PersonEntity))]
    public int DoctorId { get; set; }

    [Required]
    [ForeignKey(nameof(PersonEntity))]
    public int PatientId { get; set; }

}

