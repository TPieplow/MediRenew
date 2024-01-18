using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities
{
    public class AppointmentEntity
    {
        [Key, Column(Order = 0)]
        public int PatientId { get; set; }

        [Key, Column(Order = 1)]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Comments { get; set; }

        public virtual PatientEntity Patient { get; set; } = null!;
        public virtual DoctorEntity Doctor { get; set; } = null!;

    }
}
