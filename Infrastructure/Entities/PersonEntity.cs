using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class PersonEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime? Created { get; set; }
    public bool Active { get; set; }
    public DateTime? Updated { get; set; }

    public virtual PersonProfileEntity PersonProfile { get; set; } = null!;

    public ICollection<AppointmentEntity> Appointments = new List<AppointmentEntity>();
}
