using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities
{
    public class RoomEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [ForeignKey(nameof(Patient))]
        public int? PatientId { get; set; }

        [ForeignKey(nameof(Staff))]
        public int? StaffId { get; set; }

        public virtual PatientEntity? Patient { get; set; }

        public virtual StaffEntity? Staff { get; set; }
    }
}
