using Infrastructure.HospitalEntities;

namespace Business.DTOs;

public class DoctorDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string DepartmentName { get; set; } = null!;
    public int DepartmentId { get; set; }

    public virtual DepartmentEntity Department { get; set; } = null!;

}
