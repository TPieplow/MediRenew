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
    public int PersonId { get; set; }

    public virtual PersonEntity Person { get; set; } = null!;

}
