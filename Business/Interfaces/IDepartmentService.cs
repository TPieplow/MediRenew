using Business.DTOs;

namespace Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartments();
    }
}