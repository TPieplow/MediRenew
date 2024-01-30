using Business.DTOs;

namespace Business.Interfaces
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Get alla departments stored in the database.
        /// </summary>
        /// <returns>A list containing all departments</returns>
        Task<IEnumerable<DepartmentDTO>> GetAllDepartments();
    }
}