﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class PrescriptionEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string PrescriptionDetails { get; set; } = null!;

    [ForeignKey(nameof(PatientEntity))]
    public int PatientId { get; set; }

}