using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities
{
    public class RoomEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsOccupied { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [ForeignKey(nameof(PatientEntity))]
        public int? PatientId { get; set; }

        [ForeignKey(nameof(StaffEntity))]
        public int? StaffId { get; set; }
    }
}
