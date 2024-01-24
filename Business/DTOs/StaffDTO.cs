using Infrastructure.HospitalEntities;

namespace Business.DTOs;

public class StaffDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string RoleName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int DepartmentId { get; set; }
    public DepartmentEntity Department { get; set; } = null!;
}

