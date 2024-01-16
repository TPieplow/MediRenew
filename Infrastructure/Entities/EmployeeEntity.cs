﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(RoleEntity))]
    public int RoleId { get; set; }
}
