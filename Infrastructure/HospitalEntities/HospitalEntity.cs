using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.HospitalEntities
{
    public class HospitalEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(64)")]
        public string HospitalName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(64)")]
        public string HospitalAddress { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string HospitalPhoneNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(7)")]
        public string HospitalPostalCode { get; set; } = null!;

        public ICollection<DepartmentEntity> Departments = new List<DepartmentEntity>();
    }
}
