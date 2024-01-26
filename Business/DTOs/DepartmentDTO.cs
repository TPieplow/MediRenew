using Infrastructure.HospitalEntities;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Business.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = null!;
        public int HospitalId { get; set; }
        public virtual HospitalEntity Hospital { get; set; } = null!;
    }
}
